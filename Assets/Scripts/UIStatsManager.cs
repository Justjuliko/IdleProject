using UnityEngine;
using TMPro;

public class UIStatsManager : MonoBehaviour
{
    [Header("---TMP---")]
    [SerializeField] TMP_Text goldDisplayText;  // Reference to a TextMeshPro component that will display the gold amount.
    [SerializeField] TMP_Text healthDisplayText;  // Reference to a TextMeshPro component that will display the player's health.
    [SerializeField] TMP_Text attackPowerDisplayText;  // Reference to a TextMeshPro component that will display the player's attack power.

    // Method to display the player's gold and gold per second on the UI.
    public void DisplayGold()
    {
        // Formats and displays the player's gold and gold per second using abbreviated notation.
        goldDisplayText.text = FormatNumber(GameManager.Instance.playerData.gold) + " / " +
                               FormatNumber(GameManager.Instance.playerData.goldPerSecond) + " per sec";
    }

    // Method to display the player's fleet stats (health and attack power) on the UI.
    public void DisplayFleetStats()
    {
        // Formats and displays the player's current health.
        healthDisplayText.text = FormatNumber(GameManager.Instance.playerData.health);

        // Formats and displays the player's attack power.
        attackPowerDisplayText.text = FormatNumber(GameManager.Instance.playerData.attackPower);
    }

    /// <summary>
    /// Formats a number into a compact string representation (e.g., 1k, 1M, 1B).
    /// </summary>
    /// <param name="value">The number to format.</param>
    /// <returns>A formatted string representation of the number.</returns>
    private string FormatNumber(float value)
    {
        if (value >= 1_000_000_000_000) // Trillions (T)
            return (value / 1_000_000_000_000f).ToString("F1") + "T";
        else if (value >= 1_000_000_000) // Billions (B)
            return (value / 1_000_000_000f).ToString("F1") + "B";
        else if (value >= 1_000_000) // Millions (M)
            return (value / 1_000_000f).ToString("F1") + "M";
        else if (value >= 1_000) // Thousands (k)
            return (value / 1_000f).ToString("F1") + "k";
        else
            return value.ToString("F1"); // Default format for smaller numbers
    }

}
