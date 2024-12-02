using System;
using System.Collections;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    EventManager eventManager;
    UIEventManager uiEventManager;
    SFXManager sfxManager;

    [Header("---COMBAT STATS---")]
    [SerializeField] float enemyHealth;
    [SerializeField] float enemyAttack;
    [SerializeField] float playerHealth;
    [SerializeField] float playerAttack;

    [Header("---ENEMY MULTIPLIER---")]
    [SerializeField] float enemyHealthMultiplier;
    [SerializeField] float enemyAttackPowerMultiplier;

    public void getScripts()
    {
        eventManager = GetComponent<EventManager>();
        uiEventManager = GetComponent<UIEventManager>();
        sfxManager = GetComponent<SFXManager>();
    }
    private void GetStatsValues()
    {
        playerHealth = GameManager.Instance.playerData.health;
        playerAttack = GameManager.Instance.playerData.attackPower;
        enemyHealth = GameManager.Instance.playerData.enemyHealth;
        enemyAttack = GameManager.Instance.playerData.enemyAttackPower;
    }
    public void StartCombat()
    {
        uiEventManager.CombatUI();
        GetStatsValues();
        StartCoroutine(CombatCoroutine());
    }

    private IEnumerator CombatCoroutine()
    {
        float maxPlayerHealth = playerHealth;
        float maxEnemyHealth = enemyHealth;

        while (playerHealth > 0 && enemyHealth > 0)
        {
            playerAttack = GameManager.Instance.playerData.attackPower;

            enemyHealth -= playerAttack;
            playerHealth -= enemyAttack;

            uiEventManager.UpdateHealthBars(playerHealth, maxPlayerHealth, enemyHealth, maxEnemyHealth);

            yield return new WaitForSeconds(1f);

            Debug.Log($"Player health: {playerHealth}");
            Debug.Log($"Enemy health: {enemyHealth}");
        }
        if (enemyHealth <= 0)
        {
            NextEnemy();

            Debug.Log("Player won");
            eventManager.checkEventStart();
            uiEventManager.CombatUI();
            uiEventManager.onPlayerWin();
            sfxManager.PlayerWinPlay();
            StopCoroutine(CombatCoroutine());
        }
        else if (playerHealth <= 0) 
        {
            Debug.Log("Enemy won");
            eventManager.checkEventStart();
            uiEventManager.CombatUI();
            uiEventManager.onPlayerLose();
            sfxManager.PlayerLosePlay();
            StopCoroutine(CombatCoroutine());
        }
    }
    private void NextEnemy()
    {
        GameManager.Instance.playerData.AddEnemyStats(enemyHealthMultiplier, enemyAttackPowerMultiplier);
    }
}
