using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControllerV2 : MonoBehaviour
{
    public static PlatformControllerV2 Instance;

    [SerializeField]
    private GameObject[] GroundsPrefabs, GroundsOnScene;

    [SerializeField]
    private float nextPosPlatform;

    [SerializeField]
    private int nextIdPlatform;

    [SerializeField]
    private float distDestroy;

    [SerializeField]
    private int maxPlatform;

    private GameObject Player;

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

        maxPlatform = 3;
        distDestroy = 10f;
        nextIdPlatform = 0;
        nextPosPlatform = 0;

        if (GroundsOnScene.Length < maxPlatform)
        {
            int r = Random.Range(0, GroundsPrefabs.Length);
            GameObject newPlatform;
            newPlatform = Instantiate(GroundsPrefabs[r]);
            newPlatform.transform.position = new Vector3(0, 0.2f, nextPosPlatform);
            GroundsOnScene[nextIdPlatform] = newPlatform;
            nextIdPlatform++;
            nextPosPlatform += newPlatform.transform.position.z + newPlatform.GetComponentInChildren<Transform>().Find("Plane").GetComponent<Collider>().bounds.size.z;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if(GroundsOnScene.Length < maxPlatform)
        //{
        //    int r = Random.Range(0, GroundsPrefabs.Length);
        //    GameObject newPlatform;
        //    newPlatform = Instantiate(GroundsPrefabs[r]);
        //    newPlatform.transform.position = new Vector3(0,0.2f,nextPosPlatform);
        //    GroundsOnScene[nextIdPlatform] = newPlatform;
        //    nextIdPlatform++;
        //    nextPosPlatform = nextPosPlatform + newPlatform.transform.position.z;
        //}

        for (int i = GroundsOnScene.Length - 1; i >= 0; i--)
        {
            GameObject platform = GroundsOnScene[i];

            if (platform.transform.position.z + platform.GetComponentInChildren<Transform>().Find("Plane").GetComponent<Collider>().bounds.size.z / 2 < Player.transform.position.z - distDestroy)
            {
                Destroy(platform);
            }
        }

    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
