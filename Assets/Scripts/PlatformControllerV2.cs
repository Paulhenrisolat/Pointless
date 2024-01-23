using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControllerV2 : MonoBehaviour
{
    public static PlatformControllerV2 Instance;

    [SerializeField]
    private GameObject[] platformsPrefabs,platformsRuin,platformsForest,platformsHell,platformsHeaven,platformsTrap,actualStage;

    [SerializeField]
    private List<GameObject> platformsOnScene = new();

    [SerializeField]
    private int maxPlatform, trapChance;

    [SerializeField]
    private Material materialRuin, materialForest, materialHell, materialHeaven;

    public string stage { get; private set; }
    public float playerPosZ { get; private set; }
    public float distDestroy { get; private set; }

    private float nextPosPlatform;
    private GameObject player;

    private PlayerController playerController;
    private CollectibleControler collectibleControler;
    private FaithController faithController;

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
        faithController = GetComponent<FaithController>();

        playerPosZ = playerController.positionZ;

        maxPlatform = 12;
        distDestroy = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        playerPosZ = playerController.positionZ;
        StageManager();
        PlacePlatform();
        PlatformManager();
    }

    private void PlacePlatform()
    {
        if (platformsOnScene.Count < maxPlatform)
        {
            GameObject newPlatform;

            if (CanPlaceTrap())
            {
                int randPlatform = Random.Range(0, platformsTrap.Length);
                newPlatform = platformsTrap[randPlatform];
            }
            else
            {
                int randPlatform = Random.Range(0, actualStage.Length);
                newPlatform = actualStage[randPlatform];
            }

            newPlatform = Instantiate(newPlatform);

            float platformSize = newPlatform.GetComponentInChildren<Transform>().Find("Plane").GetComponent<Collider>().bounds.size.z;
            newPlatform.transform.position = new Vector3(0,0.2f,nextPosPlatform + platformSize/2);
            newPlatform.GetComponentInChildren<Transform>().Find("Plane").GetComponent<Renderer>().material = MaterialManager();
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
            float platformSizeZ = platform.GetComponentInChildren<Transform>().Find("Plane").GetComponent<Collider>().bounds.size.z;
            if (platformPosZ < playerPosZ - (distDestroy + platformSizeZ))
            {
                Destroy(platform);
                platformsOnScene.Remove(platform);
            }
        }
    }
    private bool CanPlaceTrap()
    {
        int r = Random.Range(0,faithController.faith);
        if (r==0)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
    private void StageManager()
    {
        switch (playerPosZ,playerPosZ)
        {
            case ( > 0, < 1000):
                stage = "ruin";
                actualStage = new GameObject[platformsRuin.Length];
                platformsRuin.CopyTo(actualStage,0);
                break;
            case ( > 1000, < 3000):
                stage = "forest";
                actualStage = new GameObject[platformsForest.Length];
                platformsForest.CopyTo(actualStage, 0);
                break;
            case ( > 3000, < 6000):
                stage = "hell";
                actualStage = new GameObject[platformsHell.Length];
                platformsHell.CopyTo(actualStage, 0);
                break;
            case ( > 6000, < 40000):
                stage = "heaven";
                actualStage = new GameObject[platformsHeaven.Length];
                platformsHeaven.CopyTo(actualStage, 0);
                break;
        }
    }
    private Material MaterialManager()
    {
        switch (stage)
        {
            case "ruin":
                return materialRuin;
            case "forest":
                return materialForest;
            case "hell":
                return materialHell;
            case "heaven":
                return materialHeaven;
            default: return materialRuin;
        }
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
