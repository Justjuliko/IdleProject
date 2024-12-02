using System;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("---SCRIPTS---")]
    public static GameManager Instance; // Singleton instance for global access to the GameManager
    private EconomyManager economyManager; // Reference to the EconomyManager script
    private EventManager eventManager; // Reference to the EventManager script

    [Header("---PLAYERDATA---")]
    public PlayerData playerData; // Instance of PlayerData to store player-related stats and progress

    // Path to save and load the player's data file
    private static string savePath => Application.persistentDataPath + "/playerData.json";

    // Called when the script instance is loaded. Ensures only one instance of GameManager exists (Singleton pattern).
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Set this instance as the Singleton
            CreateOrLoadPlayer(); // Load existing player data or create new data
        }
    }

    // Called on the first frame when the script is enabled, used to initialize game components
    private void Start()
    {
        // Retrieve and initialize the EconomyManager
        economyManager = GetComponent<EconomyManager>();
        economyManager.startMethod();

        // Retrieve and initialize the EventManager
        eventManager = GetComponent<EventManager>();
        eventManager.startMethod();
    }

    // Called once per frame, used to update the EconomyManager
    private void Update()
    {
        economyManager.updateMethod();
    }

    // Saves the player's data to a file in JSON format
    public void SavePlayerData()
    {
        // Convert PlayerData object to JSON string
        string json = JsonUtility.ToJson(Instance.playerData, true);

        // Write JSON string to a file at the specified path
        File.WriteAllText(savePath, json);

        Debug.Log("Game saved"); // Confirm save in the console
    }

    // Loads the player's data from a file if it exists; otherwise, creates a new PlayerData instance
    private void CreateOrLoadPlayer()
    {
        // Check if the save file exists
        if (File.Exists(savePath))
        {
            // Read the JSON data from the file
            string json = File.ReadAllText(savePath);

            // Deserialize the JSON data into a PlayerData object
            Instance.playerData = JsonUtility.FromJson<PlayerData>(json);

            Debug.Log("Game Loaded"); // Confirm successful load in the console
        }
        else
        {
            // Create a new PlayerData instance with default values
            playerData = new PlayerData
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
                enemyAttackPower = 1
            };

            Debug.Log("New Game Created"); // Confirm new game creation in the console
        }
    }
}
