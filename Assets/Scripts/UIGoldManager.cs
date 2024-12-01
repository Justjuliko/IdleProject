using UnityEngine;
using TMPro;

public class UIGoldManager : MonoBehaviour
{
    [SerializeField]TMP_Text goldDisplayText;
    [SerializeField]TMP_Text goldPerSecondDisplayText;
    public void DisplayGold()
    {
        goldDisplayText.text = GameManager.Instance.playerData.gold.ToString("FO");
    }
    public void DisplayGoldPerSecond()
    {
        goldPerSecondDisplayText.text = GameManager.Instance.playerData.goldPerSecond.ToString("FO");
    }
}
