using System;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("---SCRIPTS---")]
    public static GameManager Instance; // Singleton instance for global access to the GameManager
    private EconomyManager economyManager; // Reference to the EconomyManager script
    private EventManager eventManager; // Reference to the EventManager script
    private UIEventManager uiEventManager; // Reference to the UIEventManager script

    [Header("---PLAYERDATA---")]
    public PlayerData playerData; // Instance of PlayerData to store player-related stats and progress

    // Path to save and load the player's data file
    private static string savePath => Application.persistentDataPath + "/playerData.json";

    // Ensures only one instance of GameManager exists (Singleton pattern)
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Set this instance as the Singleton
            CreateOrLoadPlayer(); // Load existing player data or create new data
        }
    }

    // Initializes game components when the script is enabled
    private void Start()
    {
        economyManager = GetComponent<EconomyManager>(); // Retrieve EconomyManager
        uiEventManager = GetComponent<UIEventManager>(); // Retrieve UIEventManager
        economyManager.startMethod(); // Initialize EconomyManager

        eventManager = GetComponent<EventManager>(); // Retrieve EventManager
        eventManager.startMethod(); // Initialize EventManager

        RespawnShips(); // Respawn ships based on saved player data
    }

    // Updates the game logic every frame
    private void Update()
    {
        economyManager.updateMethod(); // Update EconomyManager logic

        // Pass touch or mouse input positions to the EventManager
        if (Input.GetMouseButtonDown(0)) // Mouse click
        {
            eventManager.PassVector2(Input.mousePosition);
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) // Touch input
        {
            eventManager.PassVector2(Input.GetTouch(0).position);
        }
    }

    // Saves the player's data to a JSON file
    public void SavePlayerData()
    {
        if (playerData == null)
        {
            Debug.LogError("playerData is null, cannot save.");
            return;
        }

        string json = JsonUtility.ToJson(playerData, true); // Convert PlayerData to JSON

        try
        {
            File.WriteAllText(savePath, json); // Write JSON to file
            Debug.Log("Player data saved successfully.");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error saving player data: {ex.Message}");
        }
    }

    // Saves player data when the application is closed or paused
    private void OnApplicationQuit()
    {
        SavePlayerData();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus) // Save progress if the app is paused
        {
            SavePlayerData();
            Debug.Log("Game progress saved on pause.");
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus) // Save progress if the app loses focus
        {
            SavePlayerData();
            Debug.Log("Game progress saved on loss of focus.");
        }
    }

    // Loads player data from a file or creates new data if none exists
    private void CreateOrLoadPlayer()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath); // Read saved JSON file
            playerData = JsonUtility.FromJson<PlayerData>(json); // Deserialize JSON into PlayerData

            if (playerData == null)
            {
                Debug.LogError("Error loading player data from file.");
                return;
            }

            Debug.Log("Game Loaded");
        }
        else
        {
            // Initialize new PlayerData if no save exists
            playerData = new PlayerData
            {
                gold = 0,
                baseGoldPerSecond = 1,
                costMultiplier = 1,
                tier1Ships = 0,
                tier2Ships = 0,
                tier3Ships = 0,
                attackPower = 1,
                health = 100,
                enemyHealth = 100,
                enemyAttackPower = 1
            };

            Debug.Log("New Game Created");
        }
    }

    // Respawns ships based on the player's saved progress
    private void RespawnShips()
    {
        if (uiEventManager == null)
        {
            Debug.LogError("UIEventManager not found in the scene.");
            return;
        }

        // Respawn ships for each tier
        for (int i = 0; i < playerData.tier1Ships; i++)
        {
            SpawnShipByTier(1, uiEventManager);
        }

        for (int i = 0; i < playerData.tier2Ships; i++)
        {
            SpawnShipByTier(2, uiEventManager);
        }

        for (int i = 0; i < playerData.tier3Ships; i++)
        {
            SpawnShipByTier(3, uiEventManager);
        }
    }

    // Spawns a ship of the specified tier using UIEventManager
    private void SpawnShipByTier(int tier, UIEventManager uiEventManager)
    {
        Ship ship = ShipDatabase.Instance.GetShipByTier(tier); // Retrieve ship data by tier

        if (ship == null)
        {
            Debug.LogWarning($"Ship for tier {tier} not found.");
            return;
        }

        uiEventManager.SpawnShip(ship); // Trigger ship spawn via UI
    }
}
