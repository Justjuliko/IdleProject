using System.Collections;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    EventManager eventManager;
    UIEventManager uiEventManager;
    SFXManager sfxManager;

    [Header("---COMBAT STATS---")]
    [SerializeField] float enemyHealth; // Enemy's health value
    [SerializeField] float enemyAttack; // Enemy's attack power
    [SerializeField] float playerHealth; // Player's health value
    [SerializeField] float playerAttack; // Player's attack power

    [Header("---ENEMY MULTIPLIER---")]
    [SerializeField] float enemyHealthMultiplier; // Multiplier for the enemy's health after each combat
    [SerializeField] float enemyAttackPowerMultiplier; // Multiplier for the enemy's attack power after each combat

    // Method to get the necessary scripts for event management, UI updates, and sound effects
    public void getScripts()
    {
        eventManager = GetComponent<EventManager>();
        uiEventManager = GetComponent<UIEventManager>();
        sfxManager = GetComponent<SFXManager>();
    }

    // This method sets the values for the player and enemy's stats
    private void GetStatsValues()
    {
        playerHealth = GameManager.Instance.playerData.health; // Retrieves player's health from PlayerData
        playerAttack = GameManager.Instance.playerData.attackPower; // Retrieves player's attack power from PlayerData
        enemyHealth = GameManager.Instance.playerData.enemyHealth; // Retrieves enemy's health from PlayerData
        enemyAttack = GameManager.Instance.playerData.enemyAttackPower; // Retrieves enemy's attack power from PlayerData
    }

    // Starts the combat by enabling the UI and setting up initial stats
    public void StartCombat()
    {
        uiEventManager.CombatUI(); // Toggles the combat UI
        GetStatsValues(); // Sets up stats for the player and enemy
        StartCoroutine(CombatCoroutine()); // Starts the combat coroutine
    }

    // Coroutine that handles the combat sequence
    private IEnumerator CombatCoroutine()
    {
        float maxPlayerHealth = playerHealth; // Stores the initial player health for the health bar
        float maxEnemyHealth = enemyHealth; // Stores the initial enemy health for the health bar

        // The combat loop will continue as long as both the player and enemy have health
        while (playerHealth > 0 && enemyHealth > 0)
        {
            playerAttack = GameManager.Instance.playerData.attackPower; // Gets updated player attack power

            enemyHealth -= playerAttack; // Decreases enemy health by the player's attack power
            playerHealth -= enemyAttack; // Decreases player health by the enemy's attack power

            // Updates health bars on the UI
            uiEventManager.UpdateHealthBars(playerHealth, maxPlayerHealth, enemyHealth, maxEnemyHealth);

            yield return new WaitForSeconds(1f); // Wait for 1 second between attacks

            Debug.Log($"Player health: {playerHealth}");
            Debug.Log($"Enemy health: {enemyHealth}");
        }

        // If the enemy's health reaches zero, the player wins
        if (enemyHealth <= 0)
        {
            NextEnemy(); // Increases the difficulty for the next enemy

            Debug.Log("Player won");
            eventManager.checkEventStart(); // Check if any event should start after the combat
            uiEventManager.CombatUI(); // Toggles off combat UI
            uiEventManager.onPlayerWin(); // Triggers UI for player win
            sfxManager.PlayerWinPlay(); // Plays victory sound
            GameManager.Instance.playerData.baseGoldPerSecond = GameManager.Instance.playerData.baseGoldPerSecond * 2;
            StopCoroutine(CombatCoroutine()); // Stops the combat coroutine
        }
        // If the player's health reaches zero, the enemy wins
        else if (playerHealth <= 0)
        {
            Debug.Log("Player lose");
            eventManager.checkEventStart(); // Check if any event should start after the combat
            uiEventManager.CombatUI(); // Toggles off combat UI
            uiEventManager.onPlayerLose(); // Triggers UI for player loss
            sfxManager.PlayerLosePlay(); // Plays loss sound
            GameManager.Instance.playerData.gold = GameManager.Instance.playerData.gold * 0.5f;
            StopCoroutine(CombatCoroutine()); // Stops the combat coroutine
        }
    }

    // Prepares the player for the next enemy by adjusting the enemy's stats
    private void NextEnemy()
    {
        GameManager.Instance.playerData.AddEnemyStats(enemyHealthMultiplier, enemyAttackPowerMultiplier);
    }
}
