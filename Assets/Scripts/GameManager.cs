using System;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("---SCRIPTS---")]
    public static GameManager Instance; //Singleton for global access
    [SerializeField] EconomyManager economyManager; //EconomyManager script

    [Header("---PLAYERDATA---")]
    public PlayerData playerData; //PlayerData Instance

    private static string savePath => Application.persistentDataPath + "/playerData.json"; //Default path to save the file

    //Unity calls Awake when an enabled script instance is being loaded.
    private void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;
            CreateOrLoadPlayer();
        }
    }
    //Start is called on the frame when a script is enabled just before any of the Update methods are called the first time.
    private void Start()
    {
        economyManager = GetComponent<EconomyManager>();
        economyManager.startMethod();
    }

    //Update is called every frame, if the MonoBehaviour is enabled
    private void Update()
    {
        economyManager.updateMethod();
    }
    //Saves the game
    public void SavePlayerData() 
    {
        string json = JsonUtility.ToJson(Instance.playerData, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Game saved");
    }
    //Loads the game
    private void CreateOrLoadPlayer()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath); //Reads the JSON file
            Instance.playerData = JsonUtility.FromJson<PlayerData>(json); //Converts the JSON file to a PlayerData object
            Debug.Log("Game Loaded");
        }
        else
        {
            playerData = new PlayerData //If the file does not exist, create a new save file
            {
                gold = 0,
                goldPerSecond = 1,
                costMultiplier = 1,
                tier1Ships = 0,
                tier2Ships = 0,
                tier3Ships = 0,
                attackPower = 1,
                health = 100,
                enemyHealth = 100,
                enemyAttackPower = 1,
                firstEnemy = false
            };
            Debug.Log("New Game Created");
        }
    }
}
