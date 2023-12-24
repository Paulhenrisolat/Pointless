using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiController : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;
    public bool pauseIsOn { get; private set; }

    private MetersController metersController;
    private PlayerController playerController;
    private FaithController faithController;

    [SerializeField]
    private TMP_Text Mood, MetersTxt, PlayerHpTxt;

    // Start is called before the first frame update
    void Start()
    {
        //pause
        pauseMenu = GameObject.Find("PauseMenu");
        pauseIsOn = false;
        pauseMenu.SetActive(false);
        //other
        metersController = this.gameObject.GetComponent<MetersController>();
        faithController = this.gameObject.GetComponent<FaithController>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Pause();
        MetersTxt.text = "Meters : " + metersController.Meters;
        PlayerHpTxt.text = playerController.playerHp.ToString();
        Mood.text = "Mood: " + faithController.entityStatus;
    }

    private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseIsOn)
            {
                pauseIsOn = true;
            }
            else
            {
                pauseIsOn = false;
            }
        }

        if (pauseIsOn)
        {
            pauseMenu.SetActive(true);
        }
        if (!pauseIsOn)
        {
            pauseMenu.SetActive(false);
        }
    }

    public void EnablePause()
    {
        pauseIsOn = true;
    }
    public void DisablePause()
    {
        pauseIsOn = false;
    }
}
