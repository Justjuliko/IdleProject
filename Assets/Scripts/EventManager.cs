using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Handles triggering and managing in-game events, such as double gold, half gold, and combat events.
/// </summary>
public class EventManager : MonoBehaviour
{
    CombatManager combatManager;      // Reference to the CombatManager script, used for initiating combat events.
    UIEventManager uiEventManager;    // Reference to the UIEventManager script, used for updating UI during events.
    SFXManager sfxManager;            // Reference to the SFXManager script, used for playing sound effects during events.

    [SerializeField] int eventInterval;       // Time interval between checking for new events, in seconds.
    [SerializeField] float doubleGoldTimer;  // Duration for the double gold event, in seconds.
    [SerializeField] float halfGoldTimer;    // Duration for the half gold event, in seconds.

    /// <summary>
    /// Initializes the script by retrieving necessary components and starting the event check routine.
    /// </summary>
    public void startMethod()
    {
        // Retrieve component references
        combatManager = GetComponent<CombatManager>();
        uiEventManager = GetComponent<UIEventManager>();
        sfxManager = GetComponent<SFXManager>();

        // Start checking for events and initialize combat-related scripts
        checkEventStart();
        combatManager.getScripts();
    }

    /// <summary>
    /// Starts the coroutine that periodically checks for new events.
    /// </summary>
    public void checkEventStart()
    {
        StartCoroutine(CheckForEvents());
    }

    /// <summary>
    /// Coroutine that waits for the defined interval and triggers random events.
    /// </summary>
    private IEnumerator CheckForEvents()
    {
        Debug.Log("Start Checking for event");
        yield return new WaitForSeconds(eventInterval); // Wait for the specified time interval
        TriggerRandomEvent(); // Attempt to trigger a random event
    }

    /// <summary>
    /// Determines a random event to trigger based on predefined probabilities.
    /// </summary>
    private void TriggerRandomEvent()
    {
        int eventChance = UnityEngine.Random.Range(0, 100); // Generate a random number between 0 and 100

        if (eventChance < 10)
        {
            StartCoroutine(ActivateDoubleGold()); // Trigger a double gold event if the chance is less than 10
            StopCoroutine(CheckForEvents());
        }
        else if (eventChance >= 11 && eventChance <= 25)
        {
            StartCoroutine(ActivateHalfGold()); // Trigger a half gold event if the chance is between 11 and 25
            StopCoroutine(CheckForEvents());
        }
        else if (eventChance >= 26 && eventChance <= 50)
        {
            combatManager.StartCombat(); // Trigger a combat event if the chance is between 26 and 50
            sfxManager.CombatPlay();    // Play combat sound effects
            StopCoroutine(CheckForEvents());
        }
        else
        {
            Debug.Log("No event triggered"); // Log if no event is triggered
            StopCoroutine(CheckForEvents());
            StartCoroutine(CheckForEvents()); // Restart the event checking coroutine
        }
    }

    /// <summary>
    /// Coroutine that activates the half gold event, temporarily reducing gold generation.
    /// </summary>
    private IEnumerator ActivateHalfGold()
    {
        sfxManager.HalfPlay();               // Play sound effect for half gold event
        uiEventManager.HalfGoldUI();         // Update UI for half gold event
        Debug.Log("HalfGold started");

        // Temporarily reduce gold generation rate
        GameManager.Instance.playerData.SetEventGoldMultiplier(0.5f);

        yield return new WaitForSeconds(halfGoldTimer); // Wait for the event's duration

        Debug.Log("HalfGold finished");
        GameManager.Instance.playerData.SetEventGoldMultiplier(1f); // Restore gold generation rate
        uiEventManager.HalfGoldUI();                               // Update UI after event ends
        checkEventStart();                                         // Restart event checks
        StopCoroutine(ActivateHalfGold());
    }

    /// <summary>
    /// Coroutine that activates the double gold event, temporarily doubling gold generation.
    /// </summary>
    private IEnumerator ActivateDoubleGold()
    {
        sfxManager.DoublePlay();              // Play sound effect for double gold event
        uiEventManager.DoubleGoldUI();        // Update UI for double gold event
        GameManager.Instance.playerData.EventGoldPerSecond(2f); // Double the gold production rate
        Debug.Log("DoubleGold started");

        // Temporarily double gold generation rate
        GameManager.Instance.playerData.SetEventGoldMultiplier(2f);

        yield return new WaitForSeconds(doubleGoldTimer); // Wait for the event's duration

        Debug.Log("DoubleGold finished");
        GameManager.Instance.playerData.SetEventGoldMultiplier(1f); // Restore gold generation rate
        uiEventManager.DoubleGoldUI();                              // Update UI after event ends
        checkEventStart();                                          // Restart event checks
        StopCoroutine(ActivateDoubleGold());
    }

    /// <summary>
    /// Passes a 2D vector (e.g., screen touch or click position) to the UIEventManager for processing.
    /// </summary>
    public void PassVector2(Vector2 vector2)
    {
        uiEventManager.GetScreenPosition(vector2); // Process the input position in UIEventManager
    }
}
