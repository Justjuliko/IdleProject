using System;
using System.Collections;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [Header("---COMBAT STATS---")]
    [SerializeField] float enemyHealth;
    [SerializeField] float enemyAttack;
    [SerializeField] float playerHealth;
    [SerializeField] float playerAttack;

    [Header("---ENEMY MULTIPLIER---")]
    [SerializeField] float enemyHealthMultiplier;
    [SerializeField] float enemyAttackPowerMultiplier;

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
            GameManager.Instance.playerData.enableFirstEnemy();
            Debug.Log("Player won");
            StopCoroutine(CombatCoroutine());
        }
        else if (playerHealth <= 0) 
        {
            Debug.Log("Enemy won");
            StopCoroutine(CombatCoroutine());
        }
    }
    private void NextEnemy()
    {
        GameManager.Instance.playerData.AddEnemyStats(enemyHealthMultiplier, enemyAttackPowerMultiplier);
    }
}
