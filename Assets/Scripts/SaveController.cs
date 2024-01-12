using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveController : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField scoreNameInput;
    [SerializeField]
    private PlayerData playerData = new PlayerData();
    [SerializeField]
    public LeaderBoard leaderBoard { get; private set; }

    private MetersController metersController;
    private PlayerController playerController;
    private string savePathPlayer, savePathLeaderBoard;

    private void Start()
    {
        metersController = this.gameObject.GetComponent<MetersController>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        savePathPlayer = Application.persistentDataPath + "/PlayerData.json";
        savePathLeaderBoard = Application.persistentDataPath + "/LeaderBoard.json";
        LoadLeaderBoard();
    }

    public void SaveData()
    {
        //Updating data
        playerData.meters = (float)Math.Round(metersController.Meters, 2);
        playerData.dateHour = DateTime.Now.ToString();

        //Saving Data in persistentDataPath
        string playerDataJson = JsonUtility.ToJson(playerData);
        Debug.Log(savePathPlayer);
        File.WriteAllText(savePathPlayer, playerDataJson);
        Debug.Log("Data Saved");
    }

    public void LoadData()
    {
        string playerDataJson = File.ReadAllText(savePathPlayer);
        playerData = JsonUtility.FromJson<PlayerData>(playerDataJson);
        playerController.startingPosition = playerData.meters;
        playerController.positionZ = playerData.meters;
        Debug.Log("Data Loaded");
    }
    public void AddScore()
    {
        PlayerScore playerScore = new();
        playerScore.name = scoreNameInput.text;
        playerScore.meters = (float)Math.Round(metersController.Meters, 2);
        playerScore.isShown = false;
        leaderBoard.playerScore.Add(playerScore);

        string leaderBoardDataJson = JsonUtility.ToJson(leaderBoard);
        Debug.Log(savePathLeaderBoard);
        File.WriteAllText(savePathLeaderBoard, leaderBoardDataJson);
        Debug.Log("LeaderBoard Saved");
    }
    public void LoadLeaderBoard()
    {
        string leaderBoardDataJson = File.ReadAllText(savePathLeaderBoard);
        leaderBoard = JsonUtility.FromJson<LeaderBoard>(leaderBoardDataJson);
        Debug.Log("LeaderBoard Loaded");
    }
}

[Serializable]
public class PlayerData
{
    public float meters;
    public string dateHour;
}
[Serializable]
public class PlayerScore
{
    public string name;
    public float meters;
    public bool isShown;
}
[Serializable]
public class LeaderBoard
{
    public List<PlayerScore> playerScore;
}
