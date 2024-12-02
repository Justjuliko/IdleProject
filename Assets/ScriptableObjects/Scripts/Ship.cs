using UnityEngine;

[CreateAssetMenu(fileName = "NewShip", menuName = "IdleGame/Ship", order = 1)]
public class Ship : ScriptableObject
{
    [Header("---SHIP IDENTIFIER---")]
    public string shipName;
    public GameObject shipPrefab;

    [Header("---SHIP IDLE STATS---")]
    public float baseGoldProduction;
    public float baseCost;

    [Header("---SHIP IDLE COMBAT STATS---")]
    public int attackPower;
    public int health;
    public int tier;
}
