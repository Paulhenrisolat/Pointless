using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetersController : MonoBehaviour
{
    public float Meters { get; private set; }

    private GameObject Player;
    private PlayerController PlayerController;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        PlayerController = Player.GetComponent<PlayerController>();
        Meters = Player.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Meters = Player.transform.position.z;
        Meters = (float)Math.Round(Meters,2);
    }
}
