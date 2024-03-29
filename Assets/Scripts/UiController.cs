using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

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
    private SpriteRenderer lifeBarRenderer;
    private OptionController optionController;

    [SerializeField]
    private RawImage faceMood;
    [SerializeField]
    private TMP_Text Mood, MetersTxt, PlayerHpTxt, score, stage, scoreboard;
    [SerializeField]
    private Sprite lifebar1, lifebar2, lifebar3, lifebar4, lifebar5,faceAngry,faceFrustrated,faceNeutral,faceHappy;

    // Start is called before the first frame update
    void Start()
    {
        //Screen.SetResolution(Screen.width, Screen.height, true);
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
        optionController = this.gameObject.GetComponent<OptionController>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        lifeBarRenderer = GameObject.Find("LifeBar").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Pause();
        GameOver();
        LifeRenderManager();
        FaceManager();
        MetersTxt.text = "Meters : " + metersController.Meters;
        //Mood.text = "Mood: " + faithController.entityStatus;
        stage.text = platformControllerV2.stage;
        if(PlayerHpTxt != null)
        {
            PlayerHpTxt.text = playerController.playerHp.ToString();
        }
    }

    private void GameOver()
    {
        if (playerController.isDead)
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
        if (Input.GetKeyDown(KeyCode.Escape) && !playerController.isDead)
        {
            if (optionController.optionIsOpen)
            {
                optionController.CloseOption();
            }
            else
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

    private void LifeRenderManager()
    {
        switch (playerController.playerHp)
        {
            case 0:
                lifeBarRenderer.sprite = null;
                break;
            case 1:
                lifeBarRenderer.sprite = lifebar5;
                break;
            case 2:
                lifeBarRenderer.sprite = lifebar4;
                break;
            case 3:
                lifeBarRenderer.sprite = lifebar3;
                break;
            case 4:
                lifeBarRenderer.sprite = lifebar2;
                break;
            case 5:
                lifeBarRenderer.sprite = lifebar1;
                break;
        }
        
    }

    private void FaceManager()
    {
        switch (faithController.entityStatus)
        {
            case "Angry":
                faceMood.texture = faceAngry.texture;
                Mood.text = "Mood: <color=#FF0000>" + faithController.entityStatus + "</color>";
                break;
            case "Frustrated":
                faceMood.texture = faceFrustrated.texture;
                Mood.text = "Mood: <color=#FF8B00>" + faithController.entityStatus + "</color>";
                break;
            case "Neutral":
                faceMood.texture = faceNeutral.texture;
                Mood.text = "Mood: <color=#00FFE4>" + faithController.entityStatus + "</color>";
                break;
            case "Happy":
                faceMood.texture = faceHappy.texture;
                Mood.text = "Mood: <color=#00FF1F>" + faithController.entityStatus + "</color>";
                break;
        }
    }
}
