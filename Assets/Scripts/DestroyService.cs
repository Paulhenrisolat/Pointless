using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyService : MonoBehaviour
{
    CollectibleControler collectibleControler;

    private void Start()
    {
        collectibleControler = GameObject.Find("GameplayManager").GetComponent<CollectibleControler>();    
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(this.gameObject);
            collectibleControler.CollectiblesInScene.Remove(this.gameObject);
        }
    }
}
