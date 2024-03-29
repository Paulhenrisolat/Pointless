using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void ChargeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GameModeSelector(int gameMode)
    {
        PlayerPrefs.SetInt("GameModeSelected", gameMode);
        PlayerPrefs.Save();
    }
}
