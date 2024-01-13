using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeityController : MonoBehaviour
{
    [SerializeField]
    private Transform positionNeutral, positionFrustrated, positionAngry;
    [SerializeField]
    private float speedMovement;

    private FaithController faithController;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = positionNeutral.position;
        speedMovement = Time.deltaTime * speedMovement;
        faithController = GameObject.Find("GameplayManager").GetComponent<FaithController>();
    }

    // Update is called once per frame
    void Update()
    {
        changePosition();
    }

    private void changePosition()
    {
        switch (faithController.entityStatus)
        {
            case "Neutral":
                transform.position = Vector3.Lerp(transform.position, positionNeutral.position, speedMovement);
                break;
            case "Frustrated":
                transform.position = Vector3.Lerp(transform.position, positionFrustrated.position, speedMovement);
                break;
            case "Angry":
                transform.position = Vector3.Lerp(transform.position, positionAngry.position, speedMovement);
                break;
        }
    }
}
