[System.Serializable]
public class PlayerData
{
    public float gold;
    public float goldPerSecond;
    public float currentCostMultiplier;
    public int tier1Ships;
    public int tier2Ships;
    public int tier3Ships;

    public void AddGold(float amount)
    {
        gold += amount;
    }
    public void AddShip(int tier)
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
}
