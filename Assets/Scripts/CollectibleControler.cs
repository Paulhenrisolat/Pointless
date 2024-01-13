using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleControler : MonoBehaviour
{
    [SerializeField]
    private GameObject[] CollectiblesPrefabs;
    public List<GameObject> CollectiblesInScene = new();

    private GameObject gameplayManager;
    private PlatformControllerV2 platformControllerV2;

    private float playerZ, distDestroy;
    // Start is called before the first frame update
    void Start()
    {
        gameplayManager = GameObject.Find("GameplayManager");
        platformControllerV2 = gameplayManager.GetComponent<PlatformControllerV2>();
    }

    // Update is called once per frame
    void Update()
    {
        playerZ = platformControllerV2.playerPosZ;
        distDestroy = platformControllerV2.distDestroy;
        CollectibleManager();
    }

    public void PlaceCollectible(GameObject platform)
    {
        Bounds platformBonds = platform.GetComponentInChildren<Transform>().Find("Plane").GetComponent<Collider>().bounds;
        float maxZ = platform.transform.position.z + platformBonds.size.z / 2;
        float minZ = platform.transform.position.z - platformBonds.size.z / 2;
        float maxX = platform.transform.position.x + platformBonds.size.x / 2;
        float minX = platform.transform.position.x - platformBonds.size.x / 2;
        float randPositionZ = Random.Range(minZ, maxZ);
        float randPositionX = Random.Range(minX, maxX);

        int r = Random.Range(0, CollectiblesPrefabs.Length);
        GameObject newCollectible = CollectiblesPrefabs[r];
        newCollectible = Instantiate(newCollectible);
        float heightCollectible = newCollectible.GetComponent<Collider>().bounds.size.y / 2;
        newCollectible.transform.position = new Vector3(randPositionX, 0.2f + heightCollectible, randPositionZ);
        CollectiblesInScene.Add(newCollectible);
    }

    private void CollectibleManager()
    {
        for (int i = CollectiblesInScene.Count - 1; i >= 0; i--)
        {
            GameObject collectible = CollectiblesInScene[i];

            if (collectible.transform.position.z < playerZ - distDestroy)
            {
                Destroy(collectible);
                CollectiblesInScene.Remove(collectible);
            }
        }
    }
}
