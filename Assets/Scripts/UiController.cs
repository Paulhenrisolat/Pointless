using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class UiController : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu, gameOver;
    public bool pauseIsOn { get; private set; }

    private MetersController metersController;
    private PlayerController playerController;
    private FaithController faithController;
    private PlatformControllerV2 platformControllerV2;
    private SaveController saveController;

    [SerializeField]
    private TMP_Text Mood, MetersTxt, PlayerHpTxt, score, stage, scoreboard;

    // Start is called before the first frame update
    void Start()
    {
        //pause
        pauseMenu = GameObject.Find("PauseMenu");
        pauseIsOn = false;
        pauseMenu.SetActive(false);
        //gameOver
        gameOver = GameObject.Find("GameOver");
        gameOver.SetActive(false);
        //other
        metersController = this.gameObject.GetComponent<MetersController>();
        faithController = this.gameObject.GetComponent<FaithController>();
        platformControllerV2 = this.gameObject.GetComponent<PlatformControllerV2>();
        saveController = this.gameObject.GetComponent<SaveController>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Pause();
        GameOver();
        MetersTxt.text = "Meters : " + metersController.Meters;
        PlayerHpTxt.text = playerController.playerHp.ToString();
        Mood.text = "Mood: " + faithController.entityStatus;
        stage.text = platformControllerV2.stage;
    }

    private void GameOver()
    {
        if (playerController.playerHp <= 0)
        {
            gameOver.SetActive(true);
            score.text = "Score : " + metersController.Meters;
            int podiumPlace = 1;
            if (saveController.scoreList != null)
            {
                foreach (var playerScore in saveController.scoreList)
                {
                    if (!playerScore.isShown)
                    {
                        scoreboard.text += playerScore.name + " : " + playerScore.meters + Podium(podiumPlace) + "<br>";
                        playerScore.isShown = true;
                        podiumPlace += 1;
                    }
                }
            }

        }
    }

    private string Podium(int place)
    {
        if(place == 1)
        {
            return "<color=#FFCE26> - 1er -</color>";
        }
        if (place == 2)
        {
            return "<color=#C1C1C1> - 2eme -</color>";
        }
        if (place == 3)
        {
            return "<color=#BD8350> - 3eme -</color>";
        }
        else
        {
            return "";
        }
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
