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
        goldProductionText.text = ship.baseGoldProduction.ToString("F0"); // Displays the gold production rate
        goldCostText.text = (ship.baseCost * costMultiplier).ToString("F0"); // Displays the cost of the ship, adjusted by the multiplier
        attackValueText.text = "+ " + ship.attackPower.ToString("F0"); // Displays the ship's attack power
        healthValueText.text = "+ " + ship.health.ToString("F0"); // Displays the ship's health
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
