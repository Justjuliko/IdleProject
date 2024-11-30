using UnityEngine;

public class GoldManager : MonoBehaviour
{
    float goldPerSecond;

    private void Start()
    {
        goldPerSecond = GameManager.Instance.playerData.goldPerSecond;
    }
    private void Update()
    {
        GameManager.Instance.playerData.AddGold(goldPerSecond);
    }
}
