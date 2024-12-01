using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    //Generates gold
    public void goldGenerator()
    {
        GameManager.Instance.playerData.AddGold(GameManager.Instance.playerData.goldPerSecond);
    }
    //Brings the specified ship to the playerData method
    public void BuyShip(Ship ship)
    {
        GameManager.Instance.playerData.BuyShip(ship);
    }
}
