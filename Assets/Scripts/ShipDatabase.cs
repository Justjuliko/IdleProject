using UnityEngine;

/// <summary>
/// Manages a centralized database for ship prefabs, allowing access to ships based on their tier.
/// Implements the Singleton pattern to ensure only one instance of this class exists.
/// </summary>
public class ShipDatabase : MonoBehaviour
{
    public static ShipDatabase Instance; // Singleton instance for global access to the ShipDatabase.

    [Header("Ship Prefabs")]
    public Ship tier1ShipPrefab; // Prefab for tier 1 ships.
    public Ship tier2ShipPrefab; // Prefab for tier 2 ships.
    public Ship tier3ShipPrefab; // Prefab for tier 3 ships.

    /// <summary>
    /// Ensures that only one instance of ShipDatabase exists and initializes the Singleton.
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
            Instance = this; // Set the current instance as the Singleton.
        else
            Destroy(gameObject); // Destroy duplicate instances to enforce the Singleton pattern.
    }

    /// <summary>
    /// Retrieves the ship prefab associated with the given tier.
    /// </summary>
    /// <param name="tier">The tier of the desired ship (1, 2, or 3).</param>
    /// <returns>The corresponding ship prefab, or null if the tier is invalid.</returns>
    public Ship GetShipByTier(int tier)
    {
        return tier switch
        {
            1 => tier1ShipPrefab, // Return the tier 1 ship prefab if the tier is 1.
            2 => tier2ShipPrefab, // Return the tier 2 ship prefab if the tier is 2.
            3 => tier3ShipPrefab, // Return the tier 3 ship prefab if the tier is 3.
            _ => null             // Return null if the tier is invalid.
        };
    }
}
