using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Handles triggering and managing in-game events, such as double gold, half gold, and combat events.
/// </summary>
public class EventManager : MonoBehaviour
{
    CombatManager combatManager;      // Reference to the CombatManager script.
    UIEventManager uiEventManager;    // Reference to the UIEventManager script.
    SFXManager sfxManager;            // Reference to the SFXManager script.

    [SerializeField] int eventInterval;       // Time interval between events in seconds.
    [SerializeField] float doubleGoldTimer;  // Duration for the double gold event in seconds.
    [SerializeField] float halfGoldTimer;    // Duration for the half gold event in seconds.

    /// <summary>
    /// Initializes the script by retrieving necessary components and starting event checks.
    /// </summary>
    public void startMethod()
    {
        combatManager = GetComponent<CombatManager>();
        uiEventManager = GetComponent<UIEventManager>();
        sfxManager = GetComponent<SFXManager>();

        checkEventStart();
        combatManager.getScripts();
    }

    /// <summary>
    /// Starts the coroutine to periodically check for triggering events.
    /// </summary>
    public void checkEventStart()
    {
        StartCoroutine(CheckForEvents());
    }

    /// <summary>
    /// Periodically checks if an event should be triggered based on the defined interval.
    /// </summary>
    private IEnumerator CheckForEvents()
    {
        Debug.Log("Start Checking for event");
        yield return new WaitForSeconds(eventInterval);
        TriggerRandomEvent();
    }

    /// <summary>
    /// Determines and triggers a random event based on predefined probabilities.
    /// </summary>
    private void TriggerRandomEvent()
    {
        int eventChance = UnityEngine.Random.Range(0, 100);

        if (eventChance < 25)
        {
            StartCoroutine(ActivateDoubleGold()); // Double gold event.
            StopCoroutine(CheckForEvents());
        }
        else if (eventChance >= 26 && eventChance <= 50)
        {
            StartCoroutine(ActivateHalfGold()); // Half gold event.
            StopCoroutine(CheckForEvents());
        }
        else if (eventChance >= 51 && eventChance <= 65)
        {
            combatManager.StartCombat(); // Combat event.
            sfxManager.CombatPlay();
            StopCoroutine(CheckForEvents());
        }
        else
        {
            Debug.Log("No event triggered"); // No event occurs.
            StopCoroutine(CheckForEvents());
            StartCoroutine(CheckForEvents());
        }
    }

    /// <summary>
    /// Activates the half gold event, modifying gold generation and updating UI.
    /// </summary>
    private IEnumerator ActivateHalfGold()
    {
        sfxManager.HalfPlay();
        uiEventManager.HalfGoldUI();
        GameManager.Instance.playerData.EventGoldPerSecond(0.5f);
        Debug.Log("HalfGold started");

        yield return new WaitForSeconds(halfGoldTimer);

        Debug.Log("HalfGold finished");
        GameManager.Instance.playerData.EventGoldPerSecond(2f);
        uiEventManager.HalfGoldUI();
        checkEventStart();
        StopCoroutine(ActivateHalfGold());
    }

    /// <summary>
    /// Activates the double gold event, doubling gold generation and updating UI.
    /// </summary>
    private IEnumerator ActivateDoubleGold()
    {
        sfxManager.DoublePlay();
        uiEventManager.DoubleGoldUI();
        GameManager.Instance.playerData.EventGoldPerSecond(2f);
        Debug.Log("DoubleGold started");

        yield return new WaitForSeconds(doubleGoldTimer);

        Debug.Log("DoubleGold finished");
        GameManager.Instance.playerData.EventGoldPerSecond(0.5f);
        uiEventManager.DoubleGoldUI();
        checkEventStart();
        StopCoroutine(ActivateDoubleGold());
    }
}
