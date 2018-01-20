using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitStatus : MonoBehaviour {

    public enum UnitType
    {
        Strike,
        Shot,
        Fly
    }

    public GameObject target;
    public GameObject mainBaseTarget;

    // status
    public int hp;
    public int atk;
    public float atkDistance;
    public float speed;

    // unit状態
    public bool isdead;
    public bool attacked;
    public UnitType type;

    private AnimStateCro anim;
    private const int MAX_HP = 100;

    private void Awake () {
        isdead = false;
        attacked = false;
    }

    public void GetDamage( int damage )
    {
        hp -= damage;
    }


}