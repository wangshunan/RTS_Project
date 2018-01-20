using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeTypeAi : AiCro {


    private void Start()
    {
        AiCroInitialization();
        status.type = UnitStatus.UnitType.Strike;
    }

    private void Update()
	{
        if ( status.isdead == true )
        {
            return;
        }

        if ( gameManager.gameStatus != GameManager.GameStatus.Play )
        {
            navController.OffAiNavGation();
            return;
        }

        SearchEnemy();
        SearchBase();
        BattleCro();
    }


    private void SearchEnemy()
    {
        if ( status.attacked != false )
        {
            return;
        }

        GameObject[] enemys;

        if ( gameObject.tag == ObjNameManager.UNIT_PLAYER_TAG )
        {
            enemys = GameObject.FindGameObjectsWithTag(ObjNameManager.UNIT_ENEMY_TAG);

            for ( int i = 0; i < enemys.Length; i++ )
            {
                var targetType = enemys[i].GetComponent<UnitStatus>().type;
                if (targetType != UnitStatus.UnitType.Fly)
                {
                    targets.Add(enemys[i]);
                }
            }
        }

        if ( gameObject.tag == ObjNameManager.UNIT_ENEMY_TAG )
        {
            enemys = GameObject.FindGameObjectsWithTag(ObjNameManager.UNIT_PLAYER_TAG);
            for (int i = 0; i < enemys.Length; i++)
            {
                var targetType = enemys[i].GetComponent<UnitStatus>().type;
                if (targetType != UnitStatus.UnitType.Fly)
                {
                    targets.Add(enemys[i]);
                }
            }
        }

        TargetCheck();

    }
}