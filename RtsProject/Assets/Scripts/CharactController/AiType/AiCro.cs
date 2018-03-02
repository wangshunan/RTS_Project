using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiCro : MonoBehaviour {

    [SerializeField]
    protected GameManager gameManager;

    protected BattleDisposition battleCro;
    protected NavCro navController;
    protected UnitStatus status;
    protected AnimStateCro anim;
    protected List<GameObject> targets;

    public int searchDistance; // 索敵範囲

    protected void AiCroInitialization()
    {
        navController = GetComponent<NavCro>();
        status = GetComponent<UnitStatus>();
        anim = GetComponent<AnimStateCro>();
        gameManager = GameObject.Find("GameSystem").GetComponent<GameManager>();
        battleCro = GetComponent<BattleDisposition>();

        targets = new List<GameObject>();

        SearchBase();
        navController.SetTarget(status.target);
    }


    // Base捜索
    protected void SearchBase()
    {
        if (status.target != null)
        {
            if (status.target.tag != ObjNameManager.BASE_TAG && status.target.tag != ObjNameManager.STATUS_DEAD_TAG)
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

    // バトルコントロール
    protected void BattleCro()
    {
        if ( status.attacked != false || status.target == null )
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

    public void Attack()
    {
        transform.LookAt(status.target.transform.position);
        battleCro.shellTarget = status.target;
        anim.SetAttack(status.type);
        status.attacked = true;
    }

    protected void TargetCheck()
    {
        if ( status.attacked )
        {
            return;
        }

        if (targets.Count > 0)
        {

            GameObject tmpTarget = targets[0];
            float distance = 0;

            if (targets.Count == 2)
            {
                tmpTarget = GetMarkTarget(targets[1], targets[0]);
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
                if (status.target == null)
                {
                    status.target = tmpTarget;
                }

                if (status.target != null && status.target.tag != ObjNameManager.BASE_TAG)
                {
                    status.target = tmpTarget;
                }
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
