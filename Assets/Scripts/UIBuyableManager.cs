using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBuyableManager : MonoBehaviour
{
    Button button;

    [Header("---SHIP SCRIPTABLE OBJECT---")]
    [SerializeField] Ship ship;

    [Header("---SHIP IDENTIFIER---")]
    [SerializeField] TMP_Text nameText;

    [Header("---SHIP IDLE STATS---")]
    [SerializeField] TMP_Text goldProductionText;
    [SerializeField] TMP_Text goldCostText;

    [Header("---SHIP IDLE COMBAT STATS---")]
    [SerializeField] TMP_Text attackValueText;
    [SerializeField] TMP_Text healthValueText;

    public void getButton()
    {
        button = GetComponent<Button>();
    }
    public void DisplayShipValues()
    {
        float costMultiplier = GameManager.Instance.playerData.costMultiplier;

        nameText.text = ship.shipName;
        goldProductionText.text = ship.baseGoldProduction.ToString("F0");
        goldCostText.text = (ship.baseCost * costMultiplier).ToString("F0");
        attackValueText.text = "+ " + ship.attackPower.ToString("F0");
        healthValueText.text = "+ " + ship.health.ToString("F0");
    }
    public void isButtonInteractable()
    {
        float gold = GameManager.Instance.playerData.gold;
        float shipCost = ship.baseCost;
        float costMultiplier = GameManager.Instance.playerData.costMultiplier;

        if (gold >= (shipCost * costMultiplier))
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }
}
