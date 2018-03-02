using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseStatus : MonoBehaviour {

    [SerializeField]
    BaseInvadCro baseInvad;

    public enum UnitType
    {
        Strike,
        Shot,
        Fly,
        Base
    }

    public GameObject target;

    // status
    [Range (0,100)]
    public int hp;

    // unit状態
    public bool isdead;
    public UnitType type;
    public string killerTag;

    private const int MAX_HP = 100;

    private void Awake () {
        baseInvad = GameObject.Find("GameSystem").GetComponent<BaseInvadCro>();
        isdead = false;
    }

    private void Update ()
    {
        if ( isdead == true )
        {
            return;
        }

        StatusUpdate();
    }

    public void GetDamage( int damage )
    {
        hp -= damage;
    }

    private void StatusUpdate()
    {
        if ( hp <= 0 )
        {
          BaseInvaded();
        }
    }

    // Base破壊されるとき相手の情報
    public void BaseInvaded()
    {
        gameObject.tag = ObjNameManager.STATUS_DEAD_TAG;
        baseInvad.BaseDestroy(gameObject, killerTag);
    }

}