using System;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("---SCRIPTS---")]
    public static GameManager Instance; //Singleton for global access
    [SerializeField] EconomyManager economyManager; //EconomyManager script
    [SerializeField] UIGoldManager uiGoldManager;
    [SerializeField] UIBuyableManager uiBuyableManager;

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
        economyManager.GetComponent<EconomyManager>();
        uiGoldManager.GetComponent<UIGoldManager>();
        uiBuyableManager.GetComponent<UIBuyableManager>();

        uiGoldManager.DisplayGoldPerSecond();
    }

    //Update is called every frame, if the MonoBehaviour is enabled
    private void Update()
    {
        economyUpdateMethod();
        uiUpdateMethod();
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
                tier3Ships = 0
            };
            Debug.Log("New Game Created");
        }
    }
    //Update methods from EconomyManager script
    private void economyUpdateMethod() 
    {
        economyManager.goldGenerator();
    }
    //Update methods from UIManager script
    private void uiUpdateMethod()
    {
        uiGoldManager.DisplayGold();
    }
}
