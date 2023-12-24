using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementService : MonoBehaviour
{
    [SerializeField]
    private float distanceX, distanceY, distanceZ, speed;
    private float resultX, resultY, resultZ;

    [SerializeField]
    private bool repeatable;

    // Start is called before the first frame update
    void Start()
    {
        resultX = transform.position.x + distanceX;
        resultY = transform.position.y + distanceY;
        resultZ = transform.position.z + distanceZ;
    }

    // Update is called once per frame
    void Update()
    {
        if (speed < 0)
        {
            if (transform.position.z < resultZ)
            {
                transform.position += transform.forward * (speed * Time.deltaTime);
            }
        }
        if (speed > 0)
        {
            if (transform.position.x < resultX)
            {
                transform.position += transform.forward * (speed * Time.deltaTime);
            }
            if (transform.position.y < resultY)
            {
                transform.position += transform.forward * (speed * Time.deltaTime);
            }
            if (transform.position.z < resultZ)
            {
                transform.position += transform.forward * (speed * Time.deltaTime);
            }
        }
    }
}
