using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    [SerializeField]
    private float powerShake, initialDuration, slowDown;
    [SerializeField]
    private bool isShaking;

    private float duration;
    [SerializeField]
    private Transform anchorPos, cameraPos, startPos, planePos;

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        cameraPos = Camera.main.transform;
        anchorPos = transform;
        duration = 0;
        player = GameObject.Find("Player");
    }
    // Update is called once per frame
    void Update()
    {
        CameraShaking();
        CameraAction();
    }

    public void Shake()
    {
        if (!isShaking)
        {
            duration = initialDuration;
        }
    }

    private void CameraShaking()
    {
        if (duration > 0)
        {
            isShaking = true;
            cameraPos.position = anchorPos.position + Random.insideUnitSphere * powerShake;
            duration -= Time.deltaTime * slowDown;
        }
        else
        {
            isShaking = false;
            cameraPos.position = anchorPos.position;
        }
    }
    private void CameraAction()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeCameraPosition(20, player.transform.position.y + 10, player.transform.position.z);
            ChangeCameraRotation(0.4f, -88.8f, -18.2f);
            Debug.Log("CameraChanged");
        }
    }
    private void ChangeCameraPosition(float posX,float posY,float posZ)
    {
        transform.position = new Vector3(posX,posY,posZ);
    }
    private void ChangeCameraRotation(float rotX, float rotY, float rotZ)
    {
        transform.eulerAngles = new Vector3(rotX, rotY, rotZ);
    }
}
