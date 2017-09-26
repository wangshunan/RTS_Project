using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {


    private int playerBases;
    private int enemyBases;

    private void Start()
    {
        playerBases = 0;
        enemyBases = 0;
    }

    private void Update()
    {
        SearchBase();
    }

    private void SearchBase()
    {
        GameObject[] temBases;

        temBases = GameObject.FindGameObjectsWithTag("Base");
        for (int i = 0; i < temBases.Length; i++)
        {
            if (temBases[i].name == "Base_Player")
            {
                playerBases ++;
            }

            if (temBases[i].name == "Base_Enemy")
            {
                playerBases ++;
            }
        }
    }

    public int GetPlayerBase()
    {
        return playerBases;
    }

    public int GetEnemyBase()
    {
        return enemyBases;
    }
}
