using UnityEngine;

/// <summary>
/// Defines the data structure and properties for a ship in the game. 
/// This class is a ScriptableObject, allowing ship data to be reused and configured via the Unity Editor.
/// </summary>
[CreateAssetMenu(fileName = "NewShip", menuName = "IdleGame/Ship", order = 1)]
public class Ship : ScriptableObject
{
    // **Ship Identification**
    [Header("---SHIP IDENTIFIER---")]
    public string shipName; // The name of the ship.
    public GameObject shipPrefab; // The prefab used to represent the ship in the game.

    // **Idle Game Statistics**
    [Header("---SHIP IDLE STATS---")]
    public float baseGoldProduction; // The amount of gold the ship generates passively.
    public float baseCost; // The base cost to acquire or upgrade the ship.

    // **Combat Statistics**
    [Header("---SHIP IDLE COMBAT STATS---")]
    public float attackPower; // The ship's attack strength in combat.
    public float health; // The ship's maximum health value.
    public int tier; // The ship's tier, indicating its level or rank.
}
