using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefabsService : MonoBehaviour
{
    [SerializeField]
    private GameObject platform, prefab, parent;
    private float platformMaxZ, platformMinZ, platformMaxX, platformMinX, platformY;

    [SerializeField]
    private int nbPrefabs;
    private int nbPrefabsOnScene;

    // Start is called before the first frame update
    void Start()
    {
        nbPrefabsOnScene = 0;
        platformMaxZ = platform.transform.position.z + platform.GetComponent<Collider>().bounds.size.z/2;
        platformMinZ = platform.transform.position.z - platform.GetComponent<Collider>().bounds.size.z/2;
        platformMaxX = platform.transform.position.x + platform.GetComponent<Collider>().bounds.size.x/2;
        platformMinX = platform.transform.position.x - platform.GetComponent<Collider>().bounds.size.x/2;
        platformY = platform.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        PlacePrefabs();
    }

    private void PlacePrefabs()
    {
        if (nbPrefabsOnScene < nbPrefabs)
        {
            GameObject newPrefabs = Instantiate(prefab, parent.transform);
            float heightPrefabs = newPrefabs.GetComponent<Collider>().bounds.size.y/2;
            float randPositionX = Random.Range(platformMinX, platformMaxX);
            float randPositionZ = Random.Range(platformMinZ, platformMaxZ);
            newPrefabs.transform.position = new Vector3(randPositionX, platformY + heightPrefabs, randPositionZ);
            nbPrefabsOnScene++;
        }
    }
}
