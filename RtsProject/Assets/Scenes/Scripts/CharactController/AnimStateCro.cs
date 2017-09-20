using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimStateCro : MonoBehaviour {

    [SerializeField, HideInInspector]
    public Animator animator;

    [SerializeField, HideInInspector]
    Status status;

    [SerializeField, HideInInspector]
    NavMeshAgent nav;

    //animation status
    private int stand;
    private int[] attack = new int [2];
    private int dead;
    private int victory;

    static public AnimatorStateInfo animState;

    // Use this for initialization
    void Awake () {
        animator = GetComponent<Animator>();
        status = GetComponent<Status>();
        nav = GetComponent<NavMeshAgent>();
        attack[0] = Animator.StringToHash("Attack1");
        attack[1] = Animator.StringToHash("Attack2");
        stand = Animator.StringToHash("Stand");
        dead = Animator.StringToHash("Death");
        victory = Animator.StringToHash("Victory");
    }

    private void Update()
    {
        if( status.isdead == true )
        {
            return;
        }

        AnimaStateUpdate();
        StateCro();
    }

    void AnimaStateUpdate()
    {
        animState = animator.GetCurrentAnimatorStateInfo(0);
    }

    void StateCro()
    {
        animator.SetFloat(stand, nav.speed == 0 ? 0 : 1.0f);
    }

    public void SetDead()
    {
        if ( status.isdead == true )
        {
            return;
        }

        animator.SetTrigger(dead);
    }

    public void SetAttack(Status.UnitType type)
    {
        switch (type)
        {
            case Status.UnitType.Strike:
                StrikeAtkAnim();
                break;

            case Status.UnitType.Shot:
                ShotAtkAnim();
                break;

            case Status.UnitType.Fly:
                FlyAtkAnim();
                break;
        }
    }

    private void StrikeAtkAnim()
    {
        int rand = Random.Range(0, 2);
        animator.SetTrigger(attack[rand]);
    }

    private void ShotAtkAnim()
    {
        Status.UnitType targetType = status.target.GetComponent<Status>().type;

        if (targetType == Status.UnitType.Fly)
        {
            animator.SetTrigger(attack[1]);
        }
        else
        {
            animator.SetTrigger(attack[0]);
        }
    }

    private void FlyAtkAnim()
    {
       animator.SetTrigger(attack[0]);
    }
}
