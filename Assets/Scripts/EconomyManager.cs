using System;
using System.Collections;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    UIStatsManager uiStatsManager;
    UIEventManager uiEventManager;
    SFXManager sfxManager;
    [SerializeField] UIBuyableManager uiBuyableManagerSloop;
    [SerializeField] UIBuyableManager uiBuyableManagerBrigantine;
    [SerializeField] UIBuyableManager uiBuyableManagerGalleon;

    private Coroutine goldCoroutine;
    public void startMethod()
    {
        goldGenerator();

        uiStatsManager = GetComponent<UIStatsManager>();
        uiStatsManager.DisplayFleetStats();

        uiEventManager = GetComponent<UIEventManager>();
        sfxManager = GetComponent<SFXManager>();

        DisplayShipValues();
        getButtonBuyable();
    }
    public void updateMethod()
    {
        uiStatsManager.DisplayGold();
        isButtonInteractable();
    }
    //Generates gold
    public void goldGenerator()
    {
        goldCoroutine = StartCoroutine(GoldGenerationCoroutine());
    }

    IEnumerator GoldGenerationCoroutine()
    {
        while (true)
        {
            GameManager.Instance.playerData.AddGold(GameManager.Instance.playerData.goldPerSecond * 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    //Brings the specified ship to the playerData method
    public void BuyShip(Ship ship)
    {
        GameManager.Instance.playerData.BuyShip(ship, uiEventManager, sfxManager);
        DisplayShipValues();
        DisplayFleetStats();
    }
    //Displays the shipValues in
    public void DisplayShipValues()
    {
        uiBuyableManagerSloop.DisplayShipValues();
        uiBuyableManagerBrigantine.DisplayShipValues();
        uiBuyableManagerGalleon.DisplayShipValues();
    }
    public void DisplayFleetStats()
    {
        uiStatsManager.DisplayFleetStats();
    }
    private void isButtonInteractable()
    {
        uiBuyableManagerSloop.isButtonInteractable();
        uiBuyableManagerBrigantine.isButtonInteractable();
        uiBuyableManagerGalleon.isButtonInteractable();
    }
    private void getButtonBuyable()
    {
        uiBuyableManagerSloop.getButton();
        uiBuyableManagerBrigantine.getButton();
        uiBuyableManagerGalleon.getButton();
    }
}
