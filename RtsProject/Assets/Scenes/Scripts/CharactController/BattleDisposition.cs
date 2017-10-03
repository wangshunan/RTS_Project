using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDisposition : MonoBehaviour { 

    [SerializeField]
    Status status;

    public GameObject shellTarget;

    void Awake () {
        status = GetComponent<Status>();
    }


    void Hit() {

        var targetBatCro = status.target.GetComponent<BattleDisposition>();

        if ( targetBatCro == null )
        {
            return;
        }

        if ( status.target.tag == "Base" )
        {
            targetBatCro.BaseGetHit(status.atk, gameObject.tag);
            return;
        }

        targetBatCro.GetHit(status.atk);

    }

    void Shot()
    {
        if ( shellTarget == null ) {
            return;
        }

        GameObject shell = (GameObject)Resources.Load("Prefabs/Unit/Shell");
        GameObject shellPos = gameObject.transform.FindChild("ShellPos").gameObject;
        Status.UnitType targetType = status.target.GetComponent<Status>().type;
        var shellCro = shell.GetComponent<SheelCro>();
        shell.tag = gameObject.tag;

        if ( targetType == Status.UnitType.Fly )
        {
            shellPos = gameObject.transform.FindChild("ShellPos_Fly").gameObject;
        }

        if ( shellTarget.GetComponent<Status>().type == Status.UnitType.Fly ) {
            shellCro.damage = status.atk * 2;
        } else {
            shellCro.damage = status.atk;
        }

        shellCro.target = shellTarget;

        Instantiate(shell, shellPos.transform.position, Quaternion.identity);
    }

    void AttackReset()
    {
        status.attacked = false;
    }

    public void GetHit( int damage )
    {
        status.GetDamage( damage );
    }

    public void BaseGetHit( int damage, string unitTag )
    {

        status.GetDamage(damage);

        if ( status.hp <= 0 )
        {
            status.killerTag = unitTag;
        }
    }

}