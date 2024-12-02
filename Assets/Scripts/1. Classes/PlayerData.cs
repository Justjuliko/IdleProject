using System.Diagnostics;

[System.Serializable]
public class PlayerData
{
    public float gold;
    public float goldPerSecond = 1;
    public float costMultiplier = 1;
    public int tier1Ships;
    public int tier2Ships;
    public int tier3Ships;
    public float attackPower = 1;
    public float health = 1;
    public float enemyHealth = 100;
    public float enemyAttackPower = 1;

    public void AddGold(float amount) //To change the gold value
    {
        gold += amount;
    }
    public void AddShip(int tier) //To increase Ships values
    {
        switch (tier)
        {
            case 1:
                tier1Ships++; break;
            case 2:
            tier2Ships++; break;
            case 3:
            tier3Ships++; break;
            default:
                return;
        }
    }
    public void AddCostMultiplier() //To increase the cost of the buyables
    {
        costMultiplier += 0.1f * costMultiplier;
    }
    public void AddGoldPerSecond(float amount) //To increase the goldPerSecond
    {
        goldPerSecond += amount;
    }
    public void AddHealth(float amount)
    {
        health += amount;
    }
    public void addAttackPower(float amount)
    {
        attackPower += amount;
    }
    public void AddEnemyStats(float healthMultiplier, float attackMultiplier)
    {
        enemyHealth += healthMultiplier * enemyHealth;
        enemyAttackPower += attackMultiplier * enemyAttackPower;
    }
    public void EventCostMultiplier(float amount)  //To change the cost of buyables
    {
        costMultiplier *=  amount;
    }
    public void EventGoldPerSecond(float amount) //To change the goldPerSecond
    {
       goldPerSecond *= amount;
    }
    public void BuyShip(Ship ship) //To buy ship if the player has enough gold
    {
        float shipCost = ship.baseCost;

        if (gold >= (shipCost * costMultiplier))
        {
            AddShip(ship.tier);
            AddGold(-(shipCost * costMultiplier));
            AddGoldPerSecond(ship.baseGoldProduction);
            addAttackPower(ship.attackPower);
            AddHealth(ship.health);
            AddCostMultiplier();
        }
    }
}
