using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimStateCro : MonoBehaviour {

    [SerializeField]
    Status status;

    [SerializeField]
    NavMeshAgent nav;

    [SerializeField]
    GameManager gameManager;

    public Animator animator;

    //animation status
    private int stand;
    private int[] attack = new int [2];
    private int dead;
    private int vectory;

    static public AnimatorStateInfo animState;

    void Awake () {

        gameManager = GameObject.Find("GameSystem").GetComponent<GameManager>();
 
        animator = GetComponent<Animator>();
        status = GetComponent<Status>();
        nav = GetComponent<NavMeshAgent>();

        attack[0] = Animator.StringToHash("Attack1");
        attack[1] = Animator.StringToHash("Attack2");
        stand = Animator.StringToHash("Stand");
        dead = Animator.StringToHash("Death");
        vectory = Animator.StringToHash("Vectory");
    }

    private void Update()
    {
        if ( status.isdead == true )
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

        if ( gameObject.tag == ObjNameManager.UNIT_PLAYER_TAG && 
             gameManager.gameStatus == GameManager.GameStatus.Vectory )
        {
            SetVectory();
        }

        if ( gameObject.tag == ObjNameManager.UNIT_ENEMY_TAG &&
             gameManager.gameStatus == GameManager.GameStatus.Lose)
        {
            SetVectory();
        }
    }

    public void SetDead()
    {
        if ( status.isdead == true )
        {
            return;
        }

        animator.SetTrigger(dead);
    }

    public void SetAttack( Status.UnitType type )
    {
        if ( gameObject.tag == ObjNameManager.STATUS_DEAD_TAG ) {
            return;
        }

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

        if ( targetType == Status.UnitType.Fly )
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

    public void SetVectory()
    {

        if ( animState.fullPathHash == Animator.StringToHash("Base Layer.Vectory") )
        {
            return;
        }

        animator.SetTrigger(vectory);
    }
}
