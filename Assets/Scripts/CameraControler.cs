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
    private Transform anchorPos, cameraPos;

    // Start is called before the first frame update
    void Start()
    {
        cameraPos = Camera.main.transform;
        anchorPos = transform;
        duration = 0;
    }
    // Update is called once per frame
    void Update()
    {
        CameraShaking();
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
}
