using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    public int playerBaseAmount;
    public int enemyBasesAmount;
    public float playerScore;
    public float enemyScore;

    private void Start()
    {
        playerBaseAmount = 0;
        enemyBasesAmount = 0;
        playerScore = 0;
        enemyScore = 0;
    }

    private void FixedUpdate()
    {
        SearchBase();
    }

    private void SearchBase()
    {
        GameObject[] tmpBases;
        int tmpPlayerBases = 0;
        int tmpEnemyBases = 0;

        tmpBases = GameObject.FindGameObjectsWithTag("Base");
        for (int i = 0; i < tmpBases.Length; i++)
        {
            if (tmpBases[i].name == "Base_Player")
            {
                tmpPlayerBases++;
            }

            if (tmpBases[i].name == "Base_Enemy")
            {
                tmpEnemyBases++;
            }
        }

        playerBaseAmount = tmpPlayerBases;
        enemyBasesAmount = tmpEnemyBases;
    }

    public int GetPlayerBase()
    {
        return playerBaseAmount;
    }

    public int GetEnemyBase()
    {
        return enemyBasesAmount;
    }
}
