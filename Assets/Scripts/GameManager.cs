using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton for global access
    public PlayerData playerData; // PlayerData Instance

    private void Awake()
    {
        if(Instance == null) {
            Instance = this;
            CreateOrLoadPlayer();
    }
}

    private void CreateOrLoadPlayer()
    {
        //here jsonLogic

        //temporal code that starts a new player
        playerData = new PlayerData
        {
            gold = 0,
            goldPerSecond = 1,
            currentCostMultiplier = 1,
            tier1Ships = 0,
            tier2Ships = 0,
            tier3Ships = 0
        };
    }
}
