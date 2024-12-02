using System;
using System.Collections;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    private UIStatsManager uiStatsManager; // Reference to the UIStatsManager script to manage stat display
    private UIEventManager uiEventManager; // Reference to the UIEventManager script to handle UI events
    private SFXManager sfxManager; // Reference to the SFXManager script for sound effects

    // References to the UIBuyableManager scripts for different ship types
    [SerializeField] private UIBuyableManager uiBuyableManagerSloop;
    [SerializeField] private UIBuyableManager uiBuyableManagerBrigantine;
    [SerializeField] private UIBuyableManager uiBuyableManagerGalleon;

    private Coroutine goldCoroutine; // Coroutine for generating gold over time

    // Initialization method called in the Start phase
    public void startMethod()
    {
        goldGenerator(); // Start generating gold over time

        // Get references to required components and initialize UI stats
        uiStatsManager = GetComponent<UIStatsManager>();
        uiStatsManager.DisplayFleetStats();

        uiEventManager = GetComponent<UIEventManager>();
        sfxManager = GetComponent<SFXManager>();

        // Update ship values and button states
        DisplayShipValues();
        getButtonBuyable();
    }

    // Update method called every frame
    public void updateMethod()
    {
        uiStatsManager.DisplayGold(); // Update the gold display in the UI
        isButtonInteractable(); // Check and update button interactivity
    }

    // Starts the gold generation coroutine
    public void goldGenerator()
    {
        goldCoroutine = StartCoroutine(GoldGenerationCoroutine());
    }

    // Coroutine to generate gold over time
    private IEnumerator GoldGenerationCoroutine()
    {
        while (true)
        {
            // Add gold based on the player's gold per second value
            GameManager.Instance.playerData.AddGold(GameManager.Instance.playerData.goldPerSecond * 0.1f);

            // Wait for a short interval before adding more gold
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Handles buying a ship and updates related UI and stats
    public void BuyShip(Ship ship)
    {
        // Calls the BuyShip method from PlayerData and passes required components
        GameManager.Instance.playerData.BuyShip(ship, uiEventManager, sfxManager);

        // Update the ship and fleet stats in the UI
        DisplayShipValues();
        DisplayFleetStats();
    }

    // Updates the display values for all ship types in the UI
    public void DisplayShipValues()
    {
        uiBuyableManagerSloop.DisplayShipValues();
        uiBuyableManagerBrigantine.DisplayShipValues();
        uiBuyableManagerGalleon.DisplayShipValues();
    }

    // Updates the fleet stats display in the UI
    public void DisplayFleetStats()
    {
        uiStatsManager.DisplayFleetStats();
    }

    // Checks if the buy buttons for ships are interactable and updates their states
    private void isButtonInteractable()
    {
        uiBuyableManagerSloop.isButtonInteractable();
        uiBuyableManagerBrigantine.isButtonInteractable();
        uiBuyableManagerGalleon.isButtonInteractable();
    }

    // Retrieves button references from the buyable managers
    private void getButtonBuyable()
    {
        uiBuyableManagerSloop.getButton();
        uiBuyableManagerBrigantine.getButton();
        uiBuyableManagerGalleon.getButton();
    }
}
