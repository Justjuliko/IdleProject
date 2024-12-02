using System;
using System.Collections;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    CombatManager combatManager;
    UIEventManager uiEventManager;
    SFXManager sfxManager;

    [SerializeField] int eventInterval;
    [SerializeField] float doubleGoldTimer;
    [SerializeField] float halfGoldTimer;
    public void startMethod()
    {
        combatManager = GetComponent<CombatManager>();
        uiEventManager = GetComponent<UIEventManager>();
        sfxManager = GetComponent<SFXManager>();
        checkEventStart();
        combatManager.getScripts();
    }
    public void checkEventStart()
    {
        StartCoroutine(CheckForEvents());
    }

    private IEnumerator CheckForEvents()
    {
        Debug.Log("Start Checking for event");
        yield return new WaitForSeconds(eventInterval);
        TriggerRandomEvent();

    }

    private void TriggerRandomEvent()
    {
        int eventChance = UnityEngine.Random.Range(0, 100);
        if (eventChance < 25)
        {
            StartCoroutine(ActivateDoubleGold());
            StopCoroutine(CheckForEvents());
        }
        if (eventChance >= 26 && eventChance <= 50)
        {
            StartCoroutine(ActivateHalfGold());
            StopCoroutine(CheckForEvents());
        }
        if (eventChance >= 51 && eventChance <= 65)
        {
            combatManager.StartCombat();
            sfxManager.CombatPlay();
            StopCoroutine(CheckForEvents());
        }
        if (eventChance >= 66 && eventChance <= 101)
        {
            Debug.Log("No event triggered");
            StopCoroutine(CheckForEvents());
            StartCoroutine(CheckForEvents());
        }
    }

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
