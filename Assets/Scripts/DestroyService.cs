using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyService : MonoBehaviour
{
    [SerializeField]
    private List<MeshRenderer> rendererList;

    private void Start()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            foreach(MeshRenderer meshRenderer in rendererList)
            {
                meshRenderer.enabled = false;
            }
        }
    }
}
