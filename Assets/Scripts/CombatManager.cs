// The CombatManager script handles all aspects of combat logic, 
// including managing stats, initiating combat, updating the UI, 
// and applying win/loss consequences.

using System.Collections;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    // References to other managers in the game
    EventManager eventManager; // Manages in-game events
    UIEventManager uiEventManager; // Handles combat-related UI updates
    SFXManager sfxManager; // Plays sound effects for combat outcomes

    [Header("---COMBAT STATS---")]
    [SerializeField] float enemyHealth; // Enemy's current health
    [SerializeField] float enemyAttack; // Enemy's attack power per turn
    [SerializeField] float playerHealth; // Player's current health
    [SerializeField] float playerAttack; // Player's attack power per turn

    [Header("---ENEMY MULTIPLIER---")]
    [SerializeField] float enemyHealthMultiplier; // Multiplier for enemy health after a win
    [SerializeField] float enemyAttackPowerMultiplier; // Multiplier for enemy attack power after a win

    // Retrieves references to required scripts at runtime
    public void getScripts()
    {
        eventManager = GetComponent<EventManager>();
        uiEventManager = GetComponent<UIEventManager>();
        sfxManager = GetComponent<SFXManager>();
    }

    // Initializes the stats for the player and enemy from the GameManager
    private void GetStatsValues()
    {
        playerHealth = GameManager.Instance.playerData.health; // Player's health from persistent data
        playerAttack = GameManager.Instance.playerData.attackPower; // Player's attack power
        enemyHealth = GameManager.Instance.playerData.enemyHealth; // Enemy's health from persistent data
        enemyAttack = GameManager.Instance.playerData.enemyAttackPower; // Enemy's attack power
    }

    // Starts a combat session, enabling UI and running the combat coroutine
    public void StartCombat()
    {
        uiEventManager.CombatUI(); // Show combat UI
        GetStatsValues(); // Initialize player and enemy stats
        StartCoroutine(CombatCoroutine()); // Run combat loop
    }

    // Coroutine that simulates turn-based combat
    private IEnumerator CombatCoroutine()
    {
        // Store the initial health values for use in UI updates
        float maxPlayerHealth = playerHealth;
        float maxEnemyHealth = enemyHealth;

        // Combat continues until either player or enemy health drops to zero
        while (playerHealth > 0 && enemyHealth > 0)
        {
            // Fetch the player's attack power in case it has been updated dynamically
            playerAttack = GameManager.Instance.playerData.attackPower;

            // Apply damage from each side
            enemyHealth -= playerAttack; // Enemy takes damage
            playerHealth -= enemyAttack; // Player takes damage

            // Update the UI with the latest health values
            uiEventManager.UpdateHealthBars(playerHealth, maxPlayerHealth, enemyHealth, maxEnemyHealth);

            yield return new WaitForSeconds(1f); // Wait for a second between attacks

            Debug.Log($"Player health: {playerHealth}");
            Debug.Log($"Enemy health: {enemyHealth}");
        }

        // Handle the outcomes based on remaining health
        if (enemyHealth <= 0) // Player wins
        {
            NextEnemy(); // Prepare the next enemy with scaled stats
            Debug.Log("Player won");
            eventManager.checkEventStart(); // Check for post-combat events
            uiEventManager.CombatUI(); // Disable combat UI
            uiEventManager.onPlayerWin(); // Display win UI
            sfxManager.PlayerWinPlay(); // Play victory sound
            GameManager.Instance.playerData.baseGoldPerSecond *= 2; // Double gold income
            StopCoroutine(CombatCoroutine()); // End combat loop
        }
        else if (playerHealth <= 0) // Enemy wins
        {
            Debug.Log("Player lose");
            eventManager.checkEventStart(); // Check for post-combat events
            uiEventManager.CombatUI(); // Disable combat UI
            uiEventManager.onPlayerLose(); // Display loss UI
            sfxManager.PlayerLosePlay(); // Play defeat sound
            GameManager.Instance.playerData.gold *= 0.5f; // Halve player's gold
            StopCoroutine(CombatCoroutine()); // End combat loop
        }
    }

    // Scales up the enemy's stats for the next encounter
    private void NextEnemy()
    {
        GameManager.Instance.playerData.AddEnemyStats(enemyHealthMultiplier, enemyAttackPowerMultiplier);
    }
}
