using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavCro : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent; // navMeshAgent

    [SerializeField]
    private UnitStatus status;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        status = GetComponent<UnitStatus>();
    }

    private void Start()
    {
        agent.speed = status.speed; 
    }

    public void OnAiNavGation()
    {
        if( agent.isStopped == false )
        {
            return;
        }

        agent.speed = status.speed;
        agent.isStopped = false;
    }
    
    public void OffAiNavGation()
    {
        if ( agent.isStopped == true )
        {
            return;
        }

        agent.velocity = Vector3.zero;
        agent.speed = 0;
        agent.isStopped = true;

    }

    // ナビゲーションコンポーネント破棄
    public void Destroy()
    {
        Destroy(agent);
    }

    // ターゲット設定
    public void SetTarget(GameObject target)
    {
        agent.SetDestination(target.transform.position);
    }

 }