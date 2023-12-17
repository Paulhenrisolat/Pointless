using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MetersControler : MonoBehaviour
{
    private GameObject Player;
    private PlayerControler PlayerControler;
    public float Meters { get; private set; }
    
    [SerializeField]
    private TMP_Text MetersTxt, PlayerHpTxt;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        PlayerControler = Player.GetComponent<PlayerControler>();
        Meters = Player.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Meters = Player.transform.position.z;
        MetersTxt.text = "Meters : "+ Meters;
        PlayerHpTxt.text = PlayerControler.playerHp.ToString();
    }
}
