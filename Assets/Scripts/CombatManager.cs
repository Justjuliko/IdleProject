using System;
using System.Collections;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    EventManager eventManager;

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
        GetStatsValues();
        StartCoroutine(CombatCoroutine());
    }

    private IEnumerator CombatCoroutine()
    {
        while (playerHealth > 0 && enemyHealth > 0)
        {
            playerAttack = GameManager.Instance.playerData.attackPower;

            enemyHealth -= playerAttack;
            playerHealth -= enemyAttack;

            yield return new WaitForSeconds(1f);

            Debug.Log($"Player health: {playerHealth}");
            Debug.Log($"Enemy health: {enemyHealth}");
        }
        if (enemyHealth <= 0)
        {
            NextEnemy();

            Debug.Log("Player won");
            eventManager.checkEventStart();
            StopCoroutine(CombatCoroutine());
        }
        else if (playerHealth <= 0) 
        {
            Debug.Log("Enemy won");
            eventManager.checkEventStart();
            StopCoroutine(CombatCoroutine());
        }
    }
    private void NextEnemy()
    {
        GameManager.Instance.playerData.AddEnemyStats(enemyHealthMultiplier, enemyAttackPowerMultiplier);
    }
}
