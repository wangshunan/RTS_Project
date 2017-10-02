﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyTypeAi : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;

    private NavCro navController;
    private Status status;
    private AnimStateCro anim;
    private List<GameObject> targets;

    public int searchDistance;

    void Awake()
    {
        navController = GetComponent<NavCro>();
        status = GetComponent<Status>();
        anim = GetComponent<AnimStateCro>();
        gameManager = GameObject.Find("GameSystem").GetComponent<GameManager>();
    }

    void Start()
    {
        targets = new List<GameObject>();
        status.type = Status.UnitType.Fly;
    }

    void Update()
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

    void BattleCro()
    {
        if (status.attacked != false)
        {
            return;
        }

        var dis = Vector3.Distance(status.target.transform.position, transform.position);

        if (dis <= status.atkDistance && status.target.tag != ObjNameManager.STATUS_DEAD_TAG)
        {
            navController.OffAiNavGation();
            Attack();
        }
        else
        {
            navController.OnAiNavGation();
            navController.SetTarget(status.target);
        }

    }

    void SearchEnemy()
    {

        if (status.attacked != false)
        {
            return;
        }

        GameObject[] enemys;

        if (gameObject.tag == ObjNameManager.UNIT_PLAYER_TAG)
        {
            enemys = GameObject.FindGameObjectsWithTag(ObjNameManager.UNIT_ENEMY_TAG);

            for (int i = 0; i < enemys.Length; i++)
            {
                targets.Add(enemys[i]);
            }
        }

        if (gameObject.tag == ObjNameManager.UNIT_ENEMY_TAG)
        {
            enemys = GameObject.FindGameObjectsWithTag(ObjNameManager.UNIT_PLAYER_TAG);

            for (int i = 0; i < enemys.Length; i++)
            {
                targets.Add(enemys[i]);
            }
        }

        TargetCheck();

    }

    private void SearchBase()
    {
        if (status.target != null )
        {
            if (status.target.tag != ObjNameManager.BASE_TAG && status.target.tag != ObjNameManager.STATUS_DEAD_TAG )
            {
                return;
            }
        }

        GameObject[] bases = GameObject.FindGameObjectsWithTag(ObjNameManager.BASE_TAG);

        if (gameObject.tag == ObjNameManager.UNIT_PLAYER_TAG)
        {
            for (int i = 0; i < bases.Length; i++)
            {
                if (bases[i].name != ObjNameManager.BASE_PLAYER_NAME)
                {
                    targets.Add(bases[i]);
                }
            }
        }

        if (gameObject.tag == ObjNameManager.UNIT_ENEMY_TAG)
        {
            for (int i = 0; i < bases.Length; i++)
            {
                if (bases[i].name != ObjNameManager.BASE_ENEMY_NAME)
                {
                    targets.Add(bases[i]);
                }
            }
        }

        TargetCheck();
    }

    public void Attack()
    {
        if (status.attacked == false)
        {
            transform.LookAt(status.target.transform.position);
            anim.SetAttack(status.type);
            status.attacked = true;
        }
    }

    void TargetCheck()
    {
        if (targets.Count > 0)
        {

            GameObject tmpTarget = targets[0];
            float distance = 0;

            if (targets.Count <= 2)
            {
                tmpTarget = GetMarkTarget(targets[1], tmpTarget);
            }
            else
            {
                for (int i = 0; i < targets.Count - 1; i++)
                {
                    tmpTarget = GetMarkTarget(targets[i + 1], tmpTarget);
                }
            }

            distance = Vector3.Distance(tmpTarget.transform.position, gameObject.transform.position);

            if (distance <= searchDistance && tmpTarget.tag != ObjNameManager.BASE_TAG)
            {
                status.target = tmpTarget;
            }

            if (tmpTarget.tag == ObjNameManager.BASE_TAG)
            {
                status.target = tmpTarget;
            }

            targets.Clear();
        }

    }

    GameObject GetMarkTarget(GameObject enemyA, GameObject enemyB)
    {
        float disA = Vector3.Distance(enemyA.transform.position, gameObject.transform.position);
        float disB = Vector3.Distance(enemyB.transform.position, gameObject.transform.position);

        return disA < disB ? enemyA : enemyB;

    }
}