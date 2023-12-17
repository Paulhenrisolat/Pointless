using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField]
    public int playerHp { get; private set; }

    [SerializeField]
    private float startSpeed, currentSpeed, speedMultiplier, jumpSpeed, difficultyMultiplier;

    [SerializeField]
    private int jumpLeft;

    [SerializeField]
    private bool isOnGround;

    private MetersControler metersControler;
    private Rigidbody playerRigibody;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

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
        transform.position = new Vector3(0, minY, 0);
        metersControler = GameObject.Find("GameplayManager").GetComponent<MetersControler>();
        playerRigibody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        SpeedManager();
        float upMove = Input.GetAxis("Jump") * jumpSpeed * Time.deltaTime;
        //transform.position += transform.up * upMove;

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

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRigibody.velocity = new Vector3(0,0,0);
            playerRigibody.AddForce(Vector3.up * jumpSpeed);
            isOnGround = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && !isOnGround && jumpLeft>0)
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

    private void SpeedManager()
    {
        speedMultiplier = (metersControler.Meters / 100) + difficultyMultiplier;
        
        //move
        float horizontalMove = Input.GetAxis("Horizontal") * (startSpeed+30) * Time.deltaTime;
        transform.position += transform.right * horizontalMove;
        float verticalMove = (startSpeed+speedMultiplier) * Time.deltaTime; //Input.GetAxis("Vertical") *
        transform.position += transform.forward * verticalMove;

        currentSpeed = verticalMove;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision !");
        playerHp -= 1;
        if (playerHp <= 0)
        {
            Debug.Log("GameOver !");
        }
        if (other.gameObject.layer == 3)
        {
            Debug.Log("ground !");
            isOnGround = true;
        }
    }
}
