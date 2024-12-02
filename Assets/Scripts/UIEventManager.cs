using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages UI elements for various in-game events and interactions, including spawning ships, event toggles, 
/// combat results, and updating health bars.
/// </summary>
public class UIEventManager : MonoBehaviour
{
    [Header("---BUY SPAWN SETTINGS---")]
    [SerializeField] Vector2 spawnAreaMin; // Minimum coordinates for spawning ships.
    [SerializeField] Vector2 spawnAreaMax; // Maximum coordinates for spawning ships.
    [SerializeField] GameObject spawnParent; // Parent object for spawned ships.

    [Header("---EVENT SETTINGS---")]
    [SerializeField] GameObject doubleGoldUIObject; // UI element for "Double Gold" event.
    [SerializeField] GameObject halfGoldUIObject;   // UI element for "Half Gold" event.
    [SerializeField] GameObject combatUIObject;     // UI element for combat events.
    [SerializeField] GameObject combatLossUIObject; // UI element displayed when the player loses combat.
    [SerializeField] GameObject combatWinUIObject;  // UI element displayed when the player wins combat.

    [Header("---HEALTH BARS---")]
    [SerializeField] Slider playerHealthBar; // Slider representing the player's health.
    [SerializeField] Slider enemyHealthBar;  // Slider representing the enemy's health.

    // Toggle states for various UI elements.
    bool toggleStatusDouble = false;
    bool toggleStatusHalf = false;
    bool toggleStatusCombat = false;
    bool toggleStatusLoseCombat = false;
    bool toggleStatusWinCombat = false;

    /// <summary>
    /// Spawns a ship within a random position inside the defined spawn area and sets it as a child of the specified parent.
    /// </summary>
    /// <param name="ship">The ship to spawn.</param>
    public void SpawnShip(Ship ship)
    {
        Vector2 randomPosition = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y));

        GameObject spawnedShip = Instantiate(ship.shipPrefab, randomPosition, Quaternion.identity);
        spawnedShip.transform.SetParent(spawnParent.transform);
        spawnedShip.transform.localPosition = randomPosition;
    }

    /// <summary>
    /// Toggles the visibility of the "Double Gold" UI element.
    /// </summary>
    public void DoubleGoldUI()
    {
        toggleStatusDouble = !toggleStatusDouble;
        doubleGoldUIObject.SetActive(toggleStatusDouble);
    }

    /// <summary>
    /// Toggles the visibility of the "Half Gold" UI element.
    /// </summary>
    public void HalfGoldUI()
    {
        toggleStatusHalf = !toggleStatusHalf;
        halfGoldUIObject.SetActive(toggleStatusHalf);
    }

    /// <summary>
    /// Toggles the visibility of the combat UI element.
    /// </summary>
    public void CombatUI()
    {
        toggleStatusCombat = !toggleStatusCombat;
        combatUIObject.SetActive(toggleStatusCombat);
    }

    /// <summary>
    /// Triggers the player's loss animation and UI display.
    /// </summary>
    public void onPlayerLose()
    {
        StartCoroutine(playerLose());
    }

    /// <summary>
    /// Coroutine that handles showing and hiding the player's loss UI element after a delay.
    /// </summary>
    private IEnumerator playerLose()
    {
        toggleStatusLoseCombat = !toggleStatusLoseCombat;
        combatLossUIObject.SetActive(toggleStatusLoseCombat);

        yield return new WaitForSeconds(5f);

        toggleStatusLoseCombat = !toggleStatusLoseCombat;
        combatLossUIObject.SetActive(toggleStatusLoseCombat);

        StopCoroutine(playerLose());
    }

    /// <summary>
    /// Triggers the player's win animation and UI display.
    /// </summary>
    public void onPlayerWin()
    {
        StartCoroutine(playerWin());
    }

    /// <summary>
    /// Coroutine that handles showing and hiding the player's win UI element after a delay.
    /// </summary>
    private IEnumerator playerWin()
    {
        toggleStatusWinCombat = !toggleStatusWinCombat;
        combatWinUIObject.SetActive(toggleStatusWinCombat);

        yield return new WaitForSeconds(5f);

        toggleStatusWinCombat = !toggleStatusWinCombat;
        combatWinUIObject.SetActive(toggleStatusWinCombat);

        StopCoroutine(playerWin());
    }

    /// <summary>
    /// Updates the health bars for the player and enemy based on their current and maximum health values.
    /// </summary>
    /// <param name="playerHealth">Current health of the player.</param>
    /// <param name="maxPlayerHealth">Maximum health of the player.</param>
    /// <param name="enemyHealth">Current health of the enemy.</param>
    /// <param name="maxEnemyHealth">Maximum health of the enemy.</param>
    public void UpdateHealthBars(float playerHealth, float maxPlayerHealth, float enemyHealth, float maxEnemyHealth)
    {
        playerHealthBar.value = playerHealth / maxPlayerHealth; // Updates player health bar.
        enemyHealthBar.value = enemyHealth / maxEnemyHealth;   // Updates enemy health bar.
    }
}
