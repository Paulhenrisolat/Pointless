using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData = new PlayerData();

    private MetersController metersController;
    private string savePath;

    private void Start()
    {
        metersController = this.gameObject.GetComponent<MetersController>();
        savePath = Application.persistentDataPath + "/PlayerData.json";
    }

    public void SaveData()
    {
        //Updating data
        playerData.meters = (float)Math.Round(metersController.Meters, 2);
        playerData.dateHour = DateTime.Now.ToString();

        //Saving Data in persistentDataPath
        string playerDataJson = JsonUtility.ToJson(playerData);
        Debug.Log(savePath);
        File.WriteAllText(savePath, playerDataJson);
        Debug.Log("Data Saved");
    }

    public void LoadData()
    {
        string playerDataJson = File.ReadAllText(savePath);
        playerData = JsonUtility.FromJson<PlayerData>(playerDataJson);
        Debug.Log("Data Loaded");
    }
}

[System.Serializable]
public class PlayerData
{
    public float meters;
    public string dateHour;
}
