using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public static PlatformController Instance;

    [SerializeField]
    private GameObject[] GroundsPrefabs, GroundsOnScene, CollectiblesPrefabs;
    private List<GameObject> CollectiblesInScene = new();
    [SerializeField]
    public float GroundSize;
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private float distDestroy;

    [SerializeField]
    private float nextPosPlatform;

    private PlayerController playerController;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        playerController = Player.GetComponent<PlayerController>();

        distDestroy = 15f;
        //nb object in list
        GroundsOnScene = new GameObject[GroundsPrefabs.Length];
        for (int i = 0; i < GroundsPrefabs.Length; i++)
        {
            int r = Random.Range(0, GroundsPrefabs.Length);
            GroundsOnScene[i] = Instantiate(GroundsPrefabs[r]);
            GroundSize = GroundsOnScene[i].GetComponentInChildren<Transform>().Find("Plane").GetComponent<Collider>().bounds.size.z;
        }

        float posZ = Player.transform.position.z + GroundSize / 2 - 1.5f;
        foreach (var ground in GroundsOnScene)
        {
            ground.transform.position = new Vector3(0, 0.2f, posZ);
            posZ += GroundSize;
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlatformManager();
        CollectibleManager();
    }

    private void PlatformManager()
    {
        for (int i = GroundsOnScene.Length - 1; i >= 0; i--)
        {
            GameObject ground = GroundsOnScene[i];
            float groundWidth = ground.GetComponentInChildren<Transform>().Find("Plane").GetComponent<Collider>().bounds.size.z;

            if (ground.transform.position.z + groundWidth / 2 < Player.transform.position.z - distDestroy)
            {
                float z = ground.transform.position.z;
                Destroy(ground);
                int r = Random.Range(0, GroundsPrefabs.Length);
                ground = Instantiate(GroundsPrefabs[r]);
                ground.transform.position = new Vector3(0, 0.2f, z + groundWidth * GroundsOnScene.Length);
                PlaceCollectible(ground);
                GroundsOnScene[i] = ground;
            }
        }
    }
    private void PlaceCollectible(GameObject platform)
    {
        Bounds platformBonds = platform.GetComponentInChildren<Transform>().Find("Plane").GetComponent<Collider>().bounds;
        float maxZ = platform.transform.position.z + platformBonds.size.z/2;
        float minZ = platform.transform.position.z - platformBonds.size.z/2;
        float maxX = platform.transform.position.x + platformBonds.size.x/2;
        float minX = platform.transform.position.x - platformBonds.size.x/2;
        float randPositionZ = Random.Range(minZ, maxZ);
        float randPositionX = Random.Range(minX, maxX);

        int r = Random.Range(0, CollectiblesPrefabs.Length);
        GameObject newCollectible = CollectiblesPrefabs[r];
        newCollectible = Instantiate(newCollectible);
        float heightCollectible = newCollectible.GetComponent<Collider>().bounds.size.y / 2;
        newCollectible.transform.position = new Vector3(randPositionX,0.2f+heightCollectible, randPositionZ);
        CollectiblesInScene.Add(newCollectible);
    }
    private void CollectibleManager()
    {
        for (int i = CollectiblesInScene.Count - 1; i>=0; i--)
        {
            GameObject collectible = CollectiblesInScene[i];

            if (collectible.transform.position.z < Player.transform.position.z - distDestroy)
            {
                Destroy(collectible);
                CollectiblesInScene.Remove(collectible);
            }
        }
    }
    private void OnDestroy()
    {
        Instance = null;
    }
}
