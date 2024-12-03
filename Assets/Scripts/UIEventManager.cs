using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] float minimumDistance = 1.5f;

    private List<Vector2> spawnedPositions = new List<Vector2>(); // Stores the positions of spawned ships

    [Header("---EVENT SETTINGS---")]
    [SerializeField] GameObject doubleGoldUIObject; // UI element for "Double Gold" event.
    [SerializeField] GameObject halfGoldUIObject;   // UI element for "Half Gold" event.
    [SerializeField] GameObject combatUIObject;     // UI element for combat events.
    [SerializeField] GameObject combatLossUIObject; // UI element displayed when the player loses combat.
    [SerializeField] GameObject combatWinUIObject;  // UI element displayed when the player wins combat.

    [Header("---CLICKER SETTINGS---")]
    [SerializeField] TextMeshProUGUI reusableGoldText; // Reusable TextMeshPro object.
    [SerializeField] Canvas canvas;                   // Reference to the Canvas.
    [SerializeField] float textLifetime = 0.1f;
    Vector2 screenPosition;

    [Header("---HEALTH BARS---")]
    [SerializeField] Image playerHealthBarFill; // Image representing the player's health bar fill.
    [SerializeField] Image enemyHealthBarFill;  // Image representing the enemy's health bar fill.

    [SerializeField] Color fullHealthColor = Color.green; // Color when health is full.
    [SerializeField] Color lowHealthColor = Color.red;    // Color when health is low.
    [SerializeField] float updateSpeed = 0.2f;            // Speed of the health bar animation.

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
        Vector2 randomPosition;
        int maxAttempts = 100; // Maximum attempts to find a valid position
        int attempts = 0;

        // Try to find a valid random position
        do
        {
            randomPosition = new Vector2(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y));

            attempts++;
        } while (!IsPositionValid(randomPosition) && attempts < maxAttempts);

        if (attempts >= maxAttempts)
        {
            minimumDistance = -1;
            Debug.LogWarning("Could not find a valid spawn position after maximum attempts.");
            return;
        }

        // Store the valid position
        spawnedPositions.Add(randomPosition);

        // Instantiate the ship
        GameObject spawnedShip = Instantiate(ship.shipPrefab, Vector3.zero, Quaternion.identity);
        spawnedShip.transform.SetParent(spawnParent.transform);
        spawnedShip.transform.localPosition = randomPosition;
    }

    private bool IsPositionValid(Vector2 position)
    {
        // Check all previously spawned positions
        foreach (Vector2 spawnedPosition in spawnedPositions)
        {
            if (Vector2.Distance(spawnedPosition, position) < minimumDistance)
            {
                return false; // Position is too close to an existing ship
            }
        }
        return true; // Position is valid
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
    /// Updates the health bars with an animated transition to represent the current health percentage.
    /// </summary>
    /// <param name="playerHealth">Current health of the player.</param>
    /// <param name="maxPlayerHealth">Maximum health of the player.</param>
    /// <param name="enemyHealth">Current health of the enemy.</param>
    /// <param name="maxEnemyHealth">Maximum health of the enemy.</param>
    public void UpdateHealthBars(float playerHealth, float maxPlayerHealth, float enemyHealth, float maxEnemyHealth)
    {
        // Calculate health percentages
        float playerHealthPercent = Mathf.Clamp01(playerHealth / maxPlayerHealth);
        float enemyHealthPercent = Mathf.Clamp01(enemyHealth / maxEnemyHealth);

        // Start animations for health bars
        StartCoroutine(AnimateHealthBar(playerHealthBarFill, playerHealthPercent));
        StartCoroutine(AnimateHealthBar(enemyHealthBarFill, enemyHealthPercent));
    }

    private IEnumerator AnimateHealthBar(Image healthBarFill, float targetFillAmount)
    {
        float initialFillAmount = healthBarFill.fillAmount;

        // Gradually interpolate fill amount
        float elapsedTime = 0f;
        while (elapsedTime < updateSpeed)
        {
            elapsedTime += Time.deltaTime;
            healthBarFill.fillAmount = Mathf.Lerp(initialFillAmount, targetFillAmount, elapsedTime / updateSpeed);

            // Update the color of the bar dynamically
            healthBarFill.color = Color.Lerp(lowHealthColor, fullHealthColor, healthBarFill.fillAmount);

            yield return null;
        }

        // Ensure the bar reaches the exact target value
        healthBarFill.fillAmount = targetFillAmount;
        healthBarFill.color = Color.Lerp(lowHealthColor, fullHealthColor, targetFillAmount);
    }


    public void ProcessClick()
    {
        float shipCount = 0.5f * GameManager.Instance.playerData.goldPerSecond;
            
        // Display the gold text
        ShowGoldText(shipCount);
    }

    private void ShowGoldText(float shipCount)
    {
        // Format the number using the large number formatting
        string formattedGold = FormatLargeNumbers(shipCount);

        // Set the text content
        reusableGoldText.text = "+ " + formattedGold;

        // Move the text to the touch position
        RectTransform rectTransform = reusableGoldText.GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            screenPosition,
            null,
            out Vector2 localPoint
        );
        rectTransform.localPosition = localPoint;

        // Enable the text and start the hide coroutine
        reusableGoldText.gameObject.SetActive(true);
        CancelInvoke(nameof(HideGoldText)); // Cancel any pending hiding
        Invoke(nameof(HideGoldText), textLifetime);
    }

    // Utility function to format numbers in "k", "M", etc.
    private string FormatLargeNumbers(float value)
    {
        if (value >= 1e12f)
            return (value / 1e12f).ToString("F1") + "T"; // Trillions
        else if (value >= 1e9f)
            return (value / 1e9f).ToString("F1") + "B"; // Billions
        else if (value >= 1e6f)
            return (value / 1e6f).ToString("F1") + "M"; // Millions
        else if (value >= 1e3f)
            return (value / 1e3f).ToString("F1") + "k"; // Thousands
        else
            return value.ToString("F1"); // Less than 1,000
    }

    private void HideGoldText()
    {
        reusableGoldText.gameObject.SetActive(false);
    }
    public void GetScreenPosition(Vector2 newScreenPosition)
    {
        screenPosition = newScreenPosition;
    }
}
