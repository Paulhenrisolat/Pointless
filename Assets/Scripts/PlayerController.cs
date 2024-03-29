using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerHp { get; private set; }
    public float startingPosition { get; set; }
    public float positionZ { get { return transform.position.z; } set { transform.position = new Vector3(transform.position.x, transform.position.y, value); } }
    public bool isDead { get; private set; }
    [SerializeField]
    private float startSpeed, currentSpeed, speedMultiplier, jumpSpeed, difficultyMultiplier;

    [SerializeField]
    private int jumpLeft;

    [SerializeField]
    private bool isOnGround, canTakeDamage;

    [SerializeField]
    private double invincibilityTimerCooldown;
    
    private TimeService invincibilityTimer;
    private GameObject gameplayManager;
    private ScriptLocator scriptLocator;
    private GameObject cameraPlayer;
    private MetersController metersController;
    private UiController uiController;
    private FaithController faithController;
    private CameraControler cameraControler;
    private SoundController soundController;

    private Rigidbody playerRigibody;
    private MeshRenderer playerMeshRenderer;
    private TrailRenderer playerTrailRenderer;

    private float minX, maxX, minY, maxY;

    [SerializeField]
    private ParticleSystem Trail;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        startSpeed = 10f;
        jumpSpeed = 500f;
        playerHp = 5;
        minX = -10f;
        maxX = 10f;
        minY = 0.5f;
        maxY = 20f;
        difficultyMultiplier = 1f;
        startingPosition = 1f;
        canTakeDamage = true;
        transform.position = new Vector3(0, minY, startingPosition);
        gameplayManager = GameObject.Find("GameplayManager");
        scriptLocator = gameplayManager.GetComponent<ScriptLocator>();
        metersController = gameplayManager.GetComponent<MetersController>();
        uiController = gameplayManager.GetComponent<UiController>();
        faithController = gameplayManager.GetComponent<FaithController>();
        soundController = gameplayManager.GetComponent<SoundController>();
        cameraPlayer = GameObject.Find("CameraHandler");
        cameraControler = cameraPlayer.GetComponent<CameraControler>();
        playerRigibody = this.GetComponent<Rigidbody>();
        playerMeshRenderer = this.GetComponentInChildren<MeshRenderer>();
        playerTrailRenderer = this.GetComponentInChildren<TrailRenderer>();
        //timer
        invincibilityTimer = new TimeService(3f);
        invincibilityTimerCooldown = invincibilityTimer.timerActualTime;
    }

    // Update is called once per frame
    void Update()
    {
        LifeManager();
        if (!uiController.pauseIsOn && !isDead)
        {
            SpeedManager();
            JumpManager();
            playerRigibody.constraints = RigidbodyConstraints.None;
            playerRigibody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
        if (uiController.pauseIsOn)
        {
            playerRigibody.constraints = RigidbodyConstraints.FreezeAll;
        }

        //limit
        if (transform.position.y > maxY)
        {
            //transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
        }
        if (transform.position.y < minY)
        {
            transform.position = new Vector3(transform.position.x, minY, transform.position.z);
        }
        if (transform.position.x > maxX)
        {
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
        }
        if (transform.position.x < minX)
        {
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);
        }

        //Timer
        invincibilityTimerCooldown = invincibilityTimer.timerActualTime;
        invincibilityTimer.Timer();
        if (invincibilityTimer.timerIsEnded)
        {
            canTakeDamage = true;
            invincibilityTimer.StopTimer();
            invincibilityTimer.RestartTimer();
            playerMeshRenderer.enabled = true;
            playerTrailRenderer.enabled = true;
        }

        ResetPlayer();
    }

    private void SpeedManager()
    {
        speedMultiplier = (metersController.Meters / 100) + difficultyMultiplier;
        
        //move
        float horizontalMove = Input.GetAxis("Horizontal") * (startSpeed+30) * Time.deltaTime;
        transform.position += transform.right * horizontalMove;
        float verticalMove = (startSpeed+speedMultiplier) * Time.deltaTime; //Input.GetAxis("Vertical") *
        transform.position += transform.forward * verticalMove;

        currentSpeed = verticalMove;
    }

    private void JumpManager()
    {
        if (isOnGround)
        {
            jumpLeft = 2;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerRigibody.velocity = new Vector3(0, 0, 0);
                playerRigibody.AddForce(Vector3.up * jumpSpeed);
                //soundController.PlaySound("Jump");
                isOnGround = false;
            }
        }
        if (!isOnGround)
        {
            if (Input.GetKeyDown(KeyCode.Space) && jumpLeft > 0)
            {
                playerRigibody.velocity = new Vector3(0, 0, 0);
                playerRigibody.AddForce(Vector3.up * jumpSpeed);
                //soundController.PlaySound("Jump");
                jumpLeft--;
            }
            if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.S))
            {
                playerRigibody.velocity = new Vector3(0, 0, 0);
                playerRigibody.AddForce(Vector3.down * jumpSpeed * 5);
                jumpLeft--;
            }
        }
    }

    private void InvulnerabiltyManager()
    {
        canTakeDamage = false;
        invincibilityTimer.StartTimer();
    }

    private void LifeManager()
    {
        if (playerHp <= 0)
        {
            isDead = true;
            playerRigibody.constraints = RigidbodyConstraints.FreezeAll;
            if(playerMeshRenderer.enabled == true)
            {
                playerMeshRenderer.enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DamageCollision")
        {
            if(canTakeDamage)
            {
                playerHp -= 1;
                int rnb = Random.Range(1,3);
                soundController.PlaySound("glass"+rnb);
                //ParticleSystem playerParticle = GetComponentInChildren<ParticleSystem>();
                //playerParticle.Play();
                Debug.Log("! Damage from : " + other.gameObject.name + other.gameObject.layer + "" + other.gameObject.tag);
                InvulnerabiltyManager();
                StartCoroutine(InvincibilityTic());
                cameraControler.Shake();
            }
        }
        if (other.gameObject.layer == 3)
        {
            //Debug.Log("ground !");
            isOnGround = true;
        }
        if(other.gameObject.tag == "FaithBonus")
        {
            faithController.AddFaith();
            ParticleSystem faithParticle = other.gameObject.GetComponentInChildren<ParticleSystem>();
            faithParticle.Play();
            soundController.PlaySound("FaithUp");
        }
        if (other.gameObject.tag == "FaithMalus")
        {
            faithController.LooseFaith();
            ParticleSystem faithParticle = other.gameObject.GetComponentInChildren<ParticleSystem>();
            soundController.PlaySound("FaithDown");
            faithParticle.Play();
        }
    }

    IEnumerator InvincibilityTic()
    {
        playerMeshRenderer.enabled = false;
        playerTrailRenderer.enabled = false;
        yield return new WaitForSeconds(0.3f);
        playerMeshRenderer.enabled = true;
        playerTrailRenderer.enabled = true;
        yield return new WaitForSeconds(0.3f);
        playerMeshRenderer.enabled = false;
        playerTrailRenderer.enabled = false;
        yield return new WaitForSeconds(0.3f);
        playerMeshRenderer.enabled = true;
        playerTrailRenderer.enabled = true;
        yield return new WaitForSeconds(0.3f);
        playerMeshRenderer.enabled = false;
        playerTrailRenderer.enabled = false;
        yield return new WaitForSeconds(0.3f);
        playerMeshRenderer.enabled = false;
        playerTrailRenderer.enabled = false;
        yield return new WaitForSeconds(0.3f);
        playerMeshRenderer.enabled = true;
        playerTrailRenderer.enabled = true;
        yield return new WaitForSeconds(0.3f);
        playerMeshRenderer.enabled = false;
        playerTrailRenderer.enabled = false;
        yield return new WaitForSeconds(0.3f);
        playerMeshRenderer.enabled = true;
        playerTrailRenderer.enabled = true;
        yield return new WaitForSeconds(0.3f);
        playerMeshRenderer.enabled = false;
        playerTrailRenderer.enabled = false;
        yield return new WaitForSeconds(0.3f);
    }

    private void ResetPlayer()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            playerHp = 0;
        }
    }
}
