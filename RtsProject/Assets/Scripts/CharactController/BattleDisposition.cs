using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDisposition : MonoBehaviour { 

    [SerializeField]
    UnitStatus unitStatus;

    [SerializeField]
    BaseStatus baseStatus;

    // 弾のターゲット
    public GameObject shellTarget;

    private void Awake () {
        unitStatus = GetComponent<UnitStatus>();
        baseStatus = GetComponent<BaseStatus>();
    }

    // attack
    private void Hit() {


        if ( unitStatus.target == null )
        {
            return;
        }

        var targetBatCro = unitStatus.target.GetComponent<BattleDisposition>();

        if (unitStatus.target.tag == "Base" )
        {
            targetBatCro.BaseGetHit(unitStatus.atk, gameObject.tag);
            return;
        }

        targetBatCro.GetHit(unitStatus.atk);

    }

    // shotAttack
    private void Shot()
    {
        if ( shellTarget == null ) {
            return;
        }

        string shellLink = "Prefabs/Unit/";

        if ( gameObject.name == "Mage")
        {
            shellLink = string.Concat(shellLink, "Fireball");
        } else {
            shellLink = string.Concat(shellLink, "Shell");
        }

        // 弾を生成する
        GameObject shell = (GameObject)Resources.Load(shellLink);
        GameObject shellPos = gameObject.transform.FindChild("ShellPos").gameObject;
        var shellCro = shell.GetComponent<SheelCro>();
        shellCro.parentTag = gameObject.tag;

        // ShotTypeの目標はFlytypeかどうかのAtkの設定
        if (shellTarget.GetComponent<UnitStatus>() != null && shellTarget.GetComponent<UnitStatus>().type == UnitStatus.UnitType.Fly ) {
            shellPos = gameObject.transform.FindChild("ShellPos_Fly").gameObject;
            shellCro.damage = unitStatus.atk * 2;
        } else {
            shellCro.damage = unitStatus.atk;
        }

        shellCro.target = shellTarget;

        Instantiate(shell, shellPos.transform.position, Quaternion.identity);
    }

    // Attackリセット
    private void AttackReset()
    {
        unitStatus.attacked = false;
    }

    public void GetHit( int damage )
    {
        unitStatus.GetDamage( damage );
    }

    public void BaseGetHit( int damage, string unitTag )
    {
		if (baseStatus != null)
		{
			baseStatus.GetDamage(damage);

			if (baseStatus.hp <= 0)
			{
				baseStatus.killerTag = unitTag;
			}
		}
    }

}