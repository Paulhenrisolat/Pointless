using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    [SerializeField]
    private float powerShake, duration, slowDown;
    [SerializeField]
    private bool isShaking;

    private float initialDuration;
    [SerializeField]
    private Transform anchorPos, cameraPos;

    // Start is called before the first frame update
    void Start()
    {
        cameraPos = Camera.main.transform;
        anchorPos = transform;
        initialDuration = duration;
    }
    // Update is called once per frame
    void Update()
    {
        CameraShaking();
    }

    public void Shake()
    {
        duration = initialDuration;
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
}
