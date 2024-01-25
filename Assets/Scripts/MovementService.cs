using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementService : MonoBehaviour
{
    
    [SerializeField]
    private float speedX, speedY, speedZ;
    private float posX, posY, posZ;

    [SerializeField]
    private float rotationX, rotationY, rotationZ;
    private float startRotX, startRotY, startRotZ;

    [SerializeField]
    private bool canMove, randomSpeedX, randomSpeedY, randomSpeedZ;
    [SerializeField]
    private float randSpeed;

    private UiController uiController;
    private string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        if (sceneName != "Menu")
        {
            uiController = GameObject.Find("GameplayManager").GetComponent<UiController>();
        }

        posX = transform.position.x;
        posY = transform.position.y;
        posZ = transform.position.z;

        startRotX = transform.eulerAngles.x;
        startRotY = transform.eulerAngles.y;
        startRotZ = transform.eulerAngles.z;

        if (randomSpeedX)
        {
            speedX = Random.Range(-randSpeed, randSpeed);
        }
        if (randomSpeedY)
        {
            speedY = Random.Range(-randSpeed, randSpeed);
        }
        if (randomSpeedZ)
        {
            speedZ = Random.Range(-randSpeed, randSpeed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((canMove && sceneName == "Menu") || (canMove && !uiController.pauseIsOn))
        {
            posX += speedX * Time.deltaTime;
            posY += speedY * Time.deltaTime;
            posZ += speedZ * Time.deltaTime;

            startRotX += rotationX * Time.deltaTime;
            startRotY += rotationY * Time.deltaTime;
            startRotZ += rotationZ * Time.deltaTime;

            this.transform.position = new Vector3(posX,posY,posZ);
            //this.transform.rotation = new Quaternion(rotX,rotY,rotZ, rotW);
            transform.eulerAngles = new Vector3(startRotX, startRotY, startRotZ);
        }
    }
}
