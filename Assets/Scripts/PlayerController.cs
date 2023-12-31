using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerHp { get; private set; }
    public float startingPosition { get; set; }
    public float positionZ { get { return transform.position.z; } set { transform.position = new Vector3(transform.position.x, transform.position.y, value); } }

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
    private GameObject cameraPlayer;
    private MetersController metersController;
    private UiController uiController;
    private FaithController faithController;
    private CameraControler cameraControler;

    private Rigidbody playerRigibody;
    private MeshRenderer playerMeshRenderer;

    private float minX, maxX, minY, maxY;

    [SerializeField]
    private ParticleSystem Trail;

    // Start is called before the first frame update
    void Start()
    {
        startSpeed = 10f;
        jumpSpeed = 500f;
        playerHp = 100;
        minX = -10f;
        maxX = 10f;
        minY = 0.5f;
        maxY = 20f;
        difficultyMultiplier = 1f;
        startingPosition = 1f;
        canTakeDamage = true;
        transform.position = new Vector3(0, minY, startingPosition);
        gameplayManager = GameObject.Find("GameplayManager");
        metersController = gameplayManager.GetComponent<MetersController>();
        uiController = gameplayManager.GetComponent<UiController>();
        faithController = gameplayManager.GetComponent<FaithController>();
        cameraPlayer = GameObject.Find("CameraHandler");
        cameraControler = cameraPlayer.GetComponent<CameraControler>();
        playerRigibody = this.GetComponent<Rigidbody>();
        playerMeshRenderer = this.GetComponentInChildren<MeshRenderer>();
        //timer
        invincibilityTimer = new TimeService(3f);
        invincibilityTimerCooldown = invincibilityTimer.timerActualTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!uiController.pauseIsOn)
        {
            SpeedManager();
            JumpManager();
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
        }
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
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRigibody.velocity = new Vector3(0, 0, 0);
            playerRigibody.AddForce(Vector3.up * jumpSpeed);
            isOnGround = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && !isOnGround && jumpLeft > 0)
        {
            playerRigibody.velocity = new Vector3(0, 0, 0);
            playerRigibody.AddForce(Vector3.up * jumpSpeed);
            jumpLeft--;
        }
        if (isOnGround)
        {
            jumpLeft = 2;
        }
    }

    private void InvulnerabiltyManager()
    {
        canTakeDamage = false;
        invincibilityTimer.StartTimer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DamageCollision")
        {
            if (playerHp <= 0)
            {
                Debug.Log("GameOver !");
            }
            else if(canTakeDamage)
            {
                playerHp -= 1;
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
        }
        if (other.gameObject.tag == "FaithMalus")
        {
            faithController.LooseFaith();
        }
    }

    IEnumerator InvincibilityTic()
    {
        playerMeshRenderer.enabled = false;
        yield return new WaitForSeconds(0.3f);
        playerMeshRenderer.enabled = true;
        yield return new WaitForSeconds(0.3f);
        playerMeshRenderer.enabled = false;
        yield return new WaitForSeconds(0.3f);
        playerMeshRenderer.enabled = true;
        yield return new WaitForSeconds(0.3f);
        playerMeshRenderer.enabled = false;
        yield return new WaitForSeconds(0.3f);
        playerMeshRenderer.enabled = false;
        yield return new WaitForSeconds(0.3f);
        playerMeshRenderer.enabled = true;
        yield return new WaitForSeconds(0.3f);
        playerMeshRenderer.enabled = false;
        yield return new WaitForSeconds(0.3f);
        playerMeshRenderer.enabled = true;
        yield return new WaitForSeconds(0.3f);
        playerMeshRenderer.enabled = false;
        yield return new WaitForSeconds(0.3f);
    }
}
