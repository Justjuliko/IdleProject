using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBuyableManager : MonoBehaviour
{
    Button button;

    [Header("---SHIP SCRIPTABLE OBJECT---")]
    [SerializeField] Ship ship; // The ship ScriptableObject that holds the ship data

    [Header("---SHIP IDENTIFIER---")]
    [SerializeField] TMP_Text nameText; // Text component to display the ship's name

    [Header("---SHIP IDLE STATS---")]
    [SerializeField] TMP_Text goldProductionText; // Text component to display the gold production rate of the ship
    [SerializeField] TMP_Text goldCostText; // Text component to display the cost of the ship in gold

    [Header("---SHIP IDLE COMBAT STATS---")]
    [SerializeField] TMP_Text attackValueText; // Text component to display the ship's attack power
    [SerializeField] TMP_Text healthValueText; // Text component to display the ship's health

    // Retrieves the button component attached to the GameObject
    public void getButton()
    {
        button = GetComponent<Button>(); // Get the Button component from the same GameObject
    }

    // Displays the ship's values (cost, production, attack, health) in the UI
    public void DisplayShipValues()
    {
        float costMultiplier = GameManager.Instance.playerData.costMultiplier; // Gets the cost multiplier from player data

        // Sets the displayed text for each ship value
        nameText.text = ship.shipName; // Displays the ship's name
        goldProductionText.text = "+ " + FormatNumber(ship.baseGoldProduction * GameManager.Instance.playerData.baseGoldPerSecond); // Displays the gold production rate
        goldCostText.text = "- " + FormatNumber(ship.baseCost * costMultiplier); // Displays the cost of the ship, adjusted by the multiplier
        attackValueText.text = "+ " + FormatNumber(ship.attackPower * GameManager.Instance.playerData.attackPower); // Displays the ship's attack power
        healthValueText.text = "+ " + FormatNumber(ship.health * GameManager.Instance.playerData.health); // Displays the ship's health
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



    // Determines if the ship's button should be interactable (if the player has enough gold to buy the ship)
    public void isButtonInteractable()
    {
        float gold = GameManager.Instance.playerData.gold; // Get the player's current gold
        float shipCost = ship.baseCost; // Get the base cost of the ship
        float costMultiplier = GameManager.Instance.playerData.costMultiplier; // Get the cost multiplier from player data

        // If the player has enough gold to buy the ship, make the button interactable
        if (gold >= (shipCost * costMultiplier))
        {
            button.interactable = true; // Enable the button
        }
        else
        {
            button.interactable = false; // Disable the button
        }
    }
}
