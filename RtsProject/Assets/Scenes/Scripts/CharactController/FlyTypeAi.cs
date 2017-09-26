using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyTypeAi : MonoBehaviour
{

    [SerializeField]
    NavCro navController;

    [SerializeField]
    Status status;

    [SerializeField]
    AnimStateCro anim;

    private List<GameObject> targets;
    public int searchDistance;

    void Awake()
    {
        navController = GetComponent<NavCro>();
        status = GetComponent<Status>();
        anim = GetComponent<AnimStateCro>();
    }

    void Start()
    {
        targets = new List<GameObject>();
        status.type = Status.UnitType.Fly;
    }

    void Update()
    {
        if (status.isdead == true)
        {
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

        if (Vector3.Distance(status.target.transform.position, transform.position) <= status.atkdistance)
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

        if (gameObject.tag == "Player")
        {
            enemys = GameObject.FindGameObjectsWithTag("Enemy");

            for (int i = 0; i < enemys.Length; i++)
            {
                targets.Add(targets[i]);
            }
        }

        if (gameObject.tag == "Enemy")
        {
            enemys = GameObject.FindGameObjectsWithTag("Player");

            for (int i = 0; i < enemys.Length; i++)
            {
                targets.Add(targets[i]);
            }
        }

        TargetCheck();

    }

    private void SearchBase()
    {
        if (status.target != null)
        {
            if (status.target.tag != "Base")
            {
                return;
            }
        }

        GameObject[] bases = GameObject.FindGameObjectsWithTag("Base");

        if (gameObject.tag == "Player")
        {
            for (int i = 0; i < bases.Length; i++)
            {
                if (bases[i].name != "Base_Player")
                {
                    targets.Add(bases[i]);
                }
            }
        }

        if (gameObject.tag == "Enemy")
        {
            for (int i = 0; i < bases.Length; i++)
            {
                if (bases[i].name != "Base_Enemy")
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

            GameObject tmpTarget = null;
            float distance = 0;
            tmpTarget = targets[0];

            for (int i = 0; i < targets.Count - 1; i++)
            {
                tmpTarget = GetMarkTarget(targets[i + 1], tmpTarget);
            }

            distance = Vector3.Distance(tmpTarget.transform.position, gameObject.transform.position);
            targets.Clear();

            if (distance <= searchDistance)
            {
                status.target = tmpTarget;
            }
        }
    }

    GameObject GetMarkTarget(GameObject enemyA, GameObject enemyB)
    {
        float disA = Vector3.Distance(enemyA.transform.position, gameObject.transform.position);
        float disB = Vector3.Distance(enemyB.transform.position, gameObject.transform.position);

        return disA < disB ? enemyA : enemyB;

    }
}