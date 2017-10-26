﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDisposition : MonoBehaviour { 

    [SerializeField]
    Status status;

    // 弾のターゲット
    public GameObject shellTarget;

    private void Awake () {
        status = GetComponent<Status>();
    }

    // attack
    private void Hit() {

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

        // FlytypeかどうかAtkの設定
        if ( shellTarget.GetComponent<Status>().type == Status.UnitType.Fly ) {
            shellPos = gameObject.transform.FindChild("ShellPos_Fly").gameObject;
            shellCro.damage = status.atk * 2;
        } else {
            shellCro.damage = status.atk;
        }

        shellCro.target = shellTarget;

        Instantiate(shell, shellPos.transform.position, Quaternion.identity);
    }

    // Attackリセット
    private void AttackReset()
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