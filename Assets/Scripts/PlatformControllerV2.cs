using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControllerV2 : MonoBehaviour
{
    public static PlatformControllerV2 Instance;

    [SerializeField]
    private GameObject[] platformsPrefabs;

    [SerializeField]
    private List<GameObject> platformsOnScene = new();

    [SerializeField]
    private int maxPlatform;
    public float playerPosZ { get; private set; }
    public float distDestroy { get; private set; }

    private float nextPosPlatform;
    private GameObject player;

    private PlayerController playerController;
    private CollectibleControler collectibleControler;
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
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        collectibleControler = GetComponent<CollectibleControler>();
        playerPosZ = playerController.positionZ;

        maxPlatform = 5;
        distDestroy = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        playerPosZ = playerController.positionZ;
        PlacePlatform();
        PlatformManager();
    }

    private void PlacePlatform()
    {
        if (platformsOnScene.Count < maxPlatform)
        {
            int randPlatform = Random.Range(0, platformsPrefabs.Length);
            GameObject newPlatform = platformsPrefabs[randPlatform];
            newPlatform = Instantiate(newPlatform);

            float platformSize = newPlatform.GetComponentInChildren<Transform>().Find("Plane").GetComponent<Collider>().bounds.size.z;
            newPlatform.transform.position = new Vector3(0,0.2f,nextPosPlatform + platformSize/2);
            nextPosPlatform += platformSize;
            
            collectibleControler.PlaceCollectible(newPlatform);

            platformsOnScene.Add(newPlatform);
        }
    }

    private void PlatformManager()
    {
        for (int i = platformsOnScene.Count - 1; i >= 0; i--)
        {
            GameObject platform = platformsOnScene[i];
            float platformPosZ = platform.transform.position.z;
            if(platformPosZ < playerPosZ - distDestroy)
            {
                Destroy(platform);
                platformsOnScene.Remove(platform);
            }
        }
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
