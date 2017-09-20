using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDisposition : MonoBehaviour { 

    [SerializeField]
    Status status;

    void Awake () {
        status = GetComponent<Status>();
    }


    void Hit() {
        var targetCro = status.target.GetComponent<BattleDisposition>();
        if (targetCro != null)
        {
            targetCro.GetHit(status.atk);
        }
    }

    void Shot()
    {
        GameObject shell = (GameObject)Resources.Load("Prefabs/Shell");
        GameObject shellPos = gameObject.transform.FindChild("ShellPos").gameObject;
        Status.UnitType targetType = status.target.GetComponent<Status>().type;
        var shellCro = shell.GetComponent<SheelCro>();

        if ( targetType == Status.UnitType.Fly )
        {
            shellPos = gameObject.transform.FindChild("ShellPos_Fly").gameObject;
        }

        shellCro.damage = status.atk;
        shellCro.target = status.target;
        Instantiate(shell, shellPos.transform.position, Quaternion.identity);
    }

    void AttackReset()
    {
        status.attacked = false;
    }

    public void GetHit( int damage )
    {
        status.GetDamage( damage );
        Debug.Log("- " + damage + "!!");
    }

}