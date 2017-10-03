﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    GameTimeCountCro gameTimeCro;

    [SerializeField]
    private List<BaseCro> baseCro;

    [SerializeField]
    ScoreManager baseAmount;

    public GameObject targetBase;

    private float count;
    private float specialCount;
    private string unitName;

    private void Awake()
    {
        gameManager = GameObject.Find("GameSystem").GetComponent<GameManager>();
        gameTimeCro = GameObject.Find("GameSystem").GetComponent<GameTimeCountCro>();
        baseAmount = GameObject.Find("GameSystem").GetComponent<ScoreManager>();
    }

    private void Start()
    {
        baseCro = new List<BaseCro>();
        unitName =  "Red/Footman";
        count = 0;
        specialCount = 0;
    }

    private void Update()
    {
        if ( gameManager.gameStatus != GameManager.GameStatus.Play )
        {
            return;
        }

        SearchBase();
        UnitProduction();
    }

    private void UnitProduction()
    {
        count += Time.deltaTime;

        if ( count >= 4 )
        {
            for ( int i = 0; i < baseCro.Count; i++ )
            {
                baseCro[i].EnemyProductionUnit(unitName, ObjNameManager.UNIT_ENEMY_TAG);
            }

            specialCount++;
            count = 0;
        }

        if ( specialCount == 3 )
        {
            for (int i = 0; i < baseCro.Count; i++)
            {
                baseCro[i].EnemyProductionUnit("Red/Archer", ObjNameManager.UNIT_ENEMY_TAG);
            }
            specialCount++;
        } 

        if ( specialCount == 10 )
        {
            for (int i = 0; i < baseCro.Count; i++)
            {
                baseCro[i].EnemyProductionUnit("Red/Horseman", ObjNameManager.UNIT_ENEMY_TAG);
            }

            specialCount = 0;
        }

        if ( baseAmount.enemyBasesAmount == 1 )
        {
            unitName = "Red/Knight";
        }

        baseCro.Clear();
    }

    private void SearchBase()
    {
        GameObject[] bases;

        bases = GameObject.FindGameObjectsWithTag(ObjNameManager.BASE_TAG);

        for ( int i = 0; i < bases.Length; i++ )
        {
            if ( bases[i].name == ObjNameManager.BASE_ENEMY_NAME )
            {
                baseCro.Add(bases[i].GetComponent<BaseCro>());
            }
        }

    }

}
