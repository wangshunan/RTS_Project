using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavCro : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    private Status status;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        status = GetComponent<Status>();
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

    public void Destroy()
    {
        Destroy(agent);
    }

    public void SetTarget(GameObject target)
    {
        agent.SetDestination(target.transform.position);
    }
}
