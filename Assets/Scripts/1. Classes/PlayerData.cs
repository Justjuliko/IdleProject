using System.Diagnostics;

[System.Serializable]
public class PlayerData
{
    // Represents the player's current gold amount
    public float gold;

    // Base rate at which the player generates gold per second
    public float baseGoldPerSecond = 1;

    // Effective gold generation per second, factoring in event multipliers
    public float goldPerSecond => baseGoldPerSecond * eventGoldMultiplier;
    private float eventGoldMultiplier = 1f; // Temporary multiplier for events

    // Multiplier applied to item costs, increases as more items are purchased
    public float costMultiplier = 1;

    // Tracks the number of ships owned by the player, organized by tiers
    public int tier1Ships;
    public int tier2Ships;
    public int tier3Ships;

    // Player's combat stats
    public float attackPower = 1; // Damage dealt per attack
    public float health = 1; // Total health points

    // Enemy combat stats
    public float enemyHealth = 100; // Enemy's starting health
    public float enemyAttackPower = 1; // Enemy's attack power

    // Adds a specified amount of gold to the player's total
    public void AddGold(float amount)
    {
        gold += amount;
    }

    // Increases the number of ships owned by the player for a specific tier
    public void AddShip(int tier)
    {
        switch (tier)
        {
            case 1:
                tier1Ships++;
                break;
            case 2:
                tier2Ships++;
                break;
            case 3:
                tier3Ships++;
                break;
            default:
                return; // Ignores invalid tier inputs
        }
    }

    // Scales up the cost multiplier for buyable items, making future purchases more expensive
    public void AddCostMultiplier()
    {
        costMultiplier += 0.5f * costMultiplier; // Exponential growth of costs
    }

    // Boosts the player's base gold generation rate
    public void AddGoldPerSecond(float amount)
    {
        baseGoldPerSecond += amount;
    }

    // Increases the player's health by the specified amount
    public void AddHealth(float amount)
    {
        health += amount;
    }

    // Increases the player's attack power by the specified amount
    public void addAttackPower(float amount)
    {
        attackPower += amount;
    }

    // Enhances the enemy's stats (health and attack power) for the next combat round
    public void AddEnemyStats(float healthMultiplier, float attackMultiplier)
    {
        enemyHealth += healthMultiplier * enemyHealth; // Scales health
        enemyAttackPower += attackMultiplier * enemyAttackPower; // Scales attack power
    }

    // Adjusts the cost multiplier during special events
    public void EventCostMultiplier(float amount)
    {
        costMultiplier *= amount;
    }

    // Adjusts the player's gold generation rate during special events
    public void EventGoldPerSecond(float amount)
    {
        baseGoldPerSecond *= amount;
    }

    // Temporarily sets a multiplier for gold generation during events
    public void SetEventGoldMultiplier(float multiplier)
    {
        eventGoldMultiplier = multiplier;
    }

    // Handles the logic for buying a ship, including validation, updates, and effects
    public void BuyShip(Ship ship, UIEventManager uiEventManager, SFXManager sfxManager)
    {
        float shipCost = ship.baseCost; // The base cost of the ship

        // Ensures the player has sufficient gold to make the purchase
        if (gold >= (shipCost * costMultiplier))
        {
            AddShip(ship.tier); // Adds the ship to the player's inventory
            AddGold(-(shipCost * costMultiplier)); // Deducts the cost from the player's gold

            // Increases player's stats based on the purchased ship's contributions
            AddGoldPerSecond(ship.baseGoldProduction * baseGoldPerSecond);
            addAttackPower(ship.attackPower * attackPower);
            AddHealth(ship.health * health);

            AddCostMultiplier(); // Updates the cost multiplier for future purchases

            uiEventManager.SpawnShip(ship); // Triggers a UI update for the new ship
            sfxManager.BuyShipPlay(ship); // Plays the sound effect for buying the ship
        }
    }
}
