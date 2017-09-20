using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeTypeAi : MonoBehaviour {

    [SerializeField]
    NavCro navController;

    [SerializeField]
    Status status;

    [SerializeField]
    AnimStateCro anim;

    public GameObject garrisonBase;
    private List<GameObject> enemys;
    private int searchDistance;

    void Awake()
	{
        navController = GetComponent<NavCro>();
        status = GetComponent<Status>();
        anim = GetComponent<AnimStateCro>();
    }
    
    void Start()
    {
        enemys = new List<GameObject>();
        searchDistance = 100;
        status.type = Status.UnitType.Strike;
    }

    void Update()
	{
        if (status.isdead == true)
        {
            return;
        }

        SearchEnemy();
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

        string temp = null;
        GameObject[] targets;


        if ( gameObject.tag == "BLUE" )
        { 
            targets = GameObject.FindGameObjectsWithTag("RED");

            for ( int i = 0; i < targets.Length; i++ )
            {
                Status.UnitType targetType = targets[i].GetComponent<Status>().type;
                if (targetType != Status.UnitType.Fly)
                {
                    enemys.Add(targets[i]);
                }
            }

            temp = "Enemy_Base";
        }

        if ( gameObject.tag == "RED" )
        {
            targets = GameObject.FindGameObjectsWithTag("BLUE");

            for (int i = 0; i < targets.Length; i++)
            {
                Status.UnitType targetType = targets[i].GetComponent<Status>().type;
                if (targetType != Status.UnitType.Fly)
                {
                    enemys.Add(targets[i]);
                }
            }

            temp = "Player_Base";
        }

        if ( TargetCheck( status.target ) == null )
        {
            status.target = GameObject.FindGameObjectWithTag(temp);// garrisonBase;
        }

    }

    public void Attack()
    {
        if (status.attacked == false)
        {
            transform.LookAt(status.target.transform.position);
            anim.SetAttack( status.type );
            status.attacked = true;
        }
    }

    GameObject TargetCheck( GameObject enemy )
    { 
        if (enemys.Count > 0) {

            float distance = 0;
            status.target = enemys[0];

            for (int i = 0; i < enemys.Count - 1; i++)
            {
                enemy = GetMarkTarget(enemys[i + 1], status.target);
            }

            distance = Vector3.Distance(status.target.transform.position, gameObject.transform.position);
            enemys.Clear();

            if ( distance >= searchDistance )
            {
                return null;
            }

        } else {
            return null;
        }

        return enemy;
    }

    GameObject GetMarkTarget( GameObject enemyA, GameObject enemyB )
    {
        float disA = Vector3.Distance(enemyA.transform.position, gameObject.transform.position);
        float disB = Vector3.Distance(enemyB.transform.position, gameObject.transform.position);

        return disA < disB ? enemyA : enemyB;

    }
}