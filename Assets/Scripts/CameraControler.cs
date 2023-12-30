using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    private Vector3 startingPos;
    [SerializeField]
    private int powerShake, nbRandPos;
    private float startX,startY,startZ;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        startX = transform.position.x;
        startY = transform.position.y;
        startZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CameraShaking()
    {
        for (int i = nbRandPos; i > 0; i--)
        {
            float randX = startX + Random.Range(0, powerShake);
            float randY = startY + Random.Range(0, powerShake);
            float randZ = startZ + Random.Range(0, powerShake);
            Vector3 randPos = new Vector3(randX, randY, randZ);
            transform.position = randPos;
        }
    }
}
