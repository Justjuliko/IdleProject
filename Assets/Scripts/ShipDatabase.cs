using UnityEngine;

public class ShipDatabase : MonoBehaviour
{
    public static ShipDatabase Instance;

    [Header("Ship Prefabs")]
    public Ship tier1ShipPrefab;
    public Ship tier2ShipPrefab;
    public Ship tier3ShipPrefab;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public Ship GetShipByTier(int tier)
    {
        return tier switch
        {
            1 => tier1ShipPrefab,
            2 => tier2ShipPrefab,
            3 => tier3ShipPrefab,
            _ => null
        };
    }
}
