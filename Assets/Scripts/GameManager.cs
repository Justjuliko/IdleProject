using System;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("---SCRIPTS---")]
    public static GameManager Instance; // Singleton instance for global access to the GameManager
    private EconomyManager economyManager; // Reference to the EconomyManager script
    private EventManager eventManager; // Reference to the EventManager script
    private UIEventManager uiEventManager;

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
        uiEventManager = GetComponent<UIEventManager>();
        economyManager.startMethod();

        // Retrieve and initialize the EventManager
        eventManager = GetComponent<EventManager>();
        eventManager.startMethod();

        RespawnShips();  // Respawn ships after loading the game
    }

    // Called once per frame, used to update the EconomyManager
    private void Update()
    {
        economyManager.updateMethod();

        if (Input.GetMouseButtonDown(0)) // Mouse click
        {
            eventManager.PassVector2(Input.mousePosition);
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) // Touch input
        {
            eventManager.PassVector2(Input.GetTouch(0).position);
        }    
}

    // Saves the player's data to a file in JSON format
    public void SavePlayerData()
    {
        // Convertir playerData a JSON
        if (playerData == null)
        {
            Debug.LogError("playerData es null, no se puede guardar.");
            return;
        }

        string json = JsonUtility.ToJson(playerData, true);

        try
        {
            // Escribir el JSON en un archivo
            File.WriteAllText(savePath, json);
            Debug.Log("Datos guardados correctamente.");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error al guardar datos: {ex.Message}");
        }
    }

    // Called when the application is quitting or the editor stops the play mode
    private void OnApplicationQuit()
    {
        SavePlayerData();
    }
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus) // Si la aplicación se pausa
        {
            SavePlayerData();
            Debug.Log("Game progress saved on pause.");
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus) // Si la aplicación pierde el foco
        {
            SavePlayerData();
            Debug.Log("Game progress saved on loss of focus.");
        }
    }


    // Loads the player's data from a file if it exists; otherwise, creates a new PlayerData instance
    private void CreateOrLoadPlayer()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            playerData = JsonUtility.FromJson<PlayerData>(json);

            if (playerData == null)
            {
                Debug.LogError("Error al cargar los datos del jugador desde el archivo.");
                return; // Detén la ejecución si los datos no se cargan correctamente
            }

            Debug.Log("Game Loaded");
        }
        else
        {
            // Si no existe el archivo de guardado, crea datos nuevos
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


    private void RespawnShips()
    {
        if (uiEventManager == null)
        {
            Debug.LogError("No se encontró UIEventManager en la escena.");
            return; // Detener ejecución si no se encuentra el objeto
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




    /// <summary>
    /// Spawns a ship based on its tier using the UIEventManager.
    /// </summary>
    private void SpawnShipByTier(int tier, UIEventManager uiEventManager)
    {
        // Asegúrate de que ship no sea null
        Ship ship = ShipDatabase.Instance.GetShipByTier(tier);
        if (ship == null)
        {
            Debug.LogWarning($"No se encontró el barco para el tier {tier}.");
            return; // Detén la ejecución si no se encuentra el barco
        }

        // Si ship no es null, procede a hacer el spawn
        uiEventManager.SpawnShip(ship);
    }

}
