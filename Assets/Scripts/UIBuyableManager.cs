using TMPro;
using UnityEngine;

public class UIBuyableManager : MonoBehaviour
{
    [Header("---SHIP IDENTIFIER---")]
    [SerializeField] TMP_Text nameText;

    [Header("---SHIP IDLE STATS---")]
    [SerializeField] TMP_Text goldProductiontext;
    [SerializeField] TMP_Text goldCostText;

    [Header("---SHIP IDLE COMBAT STATS---")]
    [SerializeField] TMP_Text attackValueText;
    [SerializeField] TMP_Text healthValueText;

    public void DisplayShipValues(Ship ship)
    {
        float costMultiplier = GameManager.Instance.playerData.costMultiplier;

        nameText.text = ship.name;
        goldProductiontext.text = ship.baseGoldProduction.ToString("F0");
        goldCostText.text = (ship.baseCost * costMultiplier).ToString("F0");
        attackValueText.text = ship.attackPower.ToString("F0");
        healthValueText.text = ship.health.ToString("F0");
    }
}
