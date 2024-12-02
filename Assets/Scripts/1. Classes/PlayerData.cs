using System.Diagnostics;

[System.Serializable]
public class PlayerData
{
    // Player's current gold amount
    public float gold;

    // Gold generation rate per second
    public float goldPerSecond = 1;

    // Multiplier applied to the cost of buyable items
    public float costMultiplier = 1;

    // Number of ships the player owns per tier
    public int tier1Ships;
    public int tier2Ships;
    public int tier3Ships;

    // Player's combat stats
    public float attackPower = 1;
    public float health = 1;

    // Enemy combat stats
    public float enemyHealth = 100;
    public float enemyAttackPower = 1;

    // Adds a specified amount of gold to the player's total
    public void AddGold(float amount)
    {
        gold += amount;
    }

    // Increases the count of ships for the specified tier
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
                return; // Do nothing if the tier is invalid
        }
    }

    // Increases the cost multiplier for buyable items
    public void AddCostMultiplier()
    {
        costMultiplier += 0.1f * costMultiplier;
    }

    // Increases the player's gold generation rate
    public void AddGoldPerSecond(float amount)
    {
        goldPerSecond += amount;
    }

    // Increases the player's health by a specified amount
    public void AddHealth(float amount)
    {
        health += amount;
    }

    // Increases the player's attack power by a specified amount
    public void addAttackPower(float amount)
    {
        attackPower += amount;
    }

    // Scales the enemy's health and attack power by specified multipliers
    public void AddEnemyStats(float healthMultiplier, float attackMultiplier)
    {
        enemyHealth += healthMultiplier * enemyHealth;
        enemyAttackPower += attackMultiplier * enemyAttackPower;
    }

    // Modifies the cost multiplier by a specified factor
    public void EventCostMultiplier(float amount)
    {
        costMultiplier *= amount;
    }

    // Modifies the gold generation rate by a specified factor
    public void EventGoldPerSecond(float amount)
    {
        goldPerSecond *= amount;
    }

    // Allows the player to buy a ship if they have enough gold, and triggers relevant actions
    public void BuyShip(Ship ship, UIEventManager uiEventManager, SFXManager sfxManager)
    {
        float shipCost = ship.baseCost;

        // Check if the player has enough gold to purchase the ship
        if (gold >= (shipCost * costMultiplier))
        {
            // Add the ship to the player's inventory
            AddShip(ship.tier);

            // Deduct the cost of the ship from the player's gold
            AddGold(-(shipCost * costMultiplier));

            // Increase the player's gold generation rate
            AddGoldPerSecond(ship.baseGoldProduction);

            // Increase the player's attack power
            addAttackPower(ship.attackPower);

            // Increase the player's health
            AddHealth(ship.health);

            // Update the cost multiplier for future purchases
            AddCostMultiplier();

            // Trigger the ship spawning UI
            uiEventManager.SpawnShip(ship);

            // Play the appropriate sound effect for buying a ship
            sfxManager.BuyShipPlay(ship);
        }
    }
}
