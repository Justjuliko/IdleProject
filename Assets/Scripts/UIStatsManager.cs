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
        // Sets the text of goldDisplayText to show the player's current gold and the gold per second (formatted as integers).
        goldDisplayText.text = GameManager.Instance.playerData.gold.ToString("F0") + " / " +
                               GameManager.Instance.playerData.goldPerSecond.ToString("F1") + " per sec";
    }

    // Method to display the player's fleet stats (health and attack power) on the UI.
    public void DisplayFleetStats()
    {
        // Sets the text of healthDisplayText to show the player's current health.
        healthDisplayText.text = GameManager.Instance.playerData.health.ToString("F0");

        // Sets the text of attackPowerDisplayText to show the player's attack power.
        attackPowerDisplayText.text = GameManager.Instance.playerData.attackPower.ToString("F0");
    }
}
