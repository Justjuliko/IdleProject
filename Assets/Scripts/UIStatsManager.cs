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
        if (value >= 1e30f) // Decillones
            return (value / 1e30f).ToString("F1") + "D"; // Decillions (10^30)
        else if (value >= 1e27f) // Nonillones
            return (value / 1e27f).ToString("F1") + "N"; // Nonillions (10^27)
        else if (value >= 1e24f) // Octillones
            return (value / 1e24f).ToString("F1") + "O"; // Octillions (10^24)
        else if (value >= 1e21f) // Septillones
            return (value / 1e21f).ToString("F1") + "S"; // Septillions (10^21)
        else if (value >= 1e18f) // Quintillones
            return (value / 1e18f).ToString("F1") + "Q"; // Quintillions (10^18)
        else if (value >= 1e15f) // Cuatrillones
            return (value / 1e15f).ToString("F1") + "Qa"; // Quadrillions (10^15)
        else if (value >= 1e12f) // Trillones
            return (value / 1e12f).ToString("F1") + "T"; // Trillions (10^12)
        else if (value >= 1e9f) // Mil millones
            return (value / 1e9f).ToString("F1") + "B"; // Billions (10^9)
        else if (value >= 1e6f) // Millones
            return (value / 1e6f).ToString("F1") + "M"; // Millions (10^6)
        else if (value >= 1e3f) // Miles
            return (value / 1e3f).ToString("F1") + "k"; // Thousands (10^3)
        else
            return value.ToString("F1"); // Menos de 1,000
    }


}
