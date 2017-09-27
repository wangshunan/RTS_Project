using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Status : MonoBehaviour {

    [SerializeField]
    AnimStateCro anim;

    public enum UnitType
    {
        Strike,
        Shot,
        Fly,
        Base
    }

    public int hp;
    public int atk;
    public int atkdistance;
    public float speed;
    public bool isdead;
    public bool attacked;
    public GameObject target;
    public UnitType type;
    public string killerTag;

    private const int MAX_HP = 100;
    private void Awake () {
        anim = GetComponent<AnimStateCro>();
        isdead = false;
        attacked = false;
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
        if ( hp <= 0 && type != UnitType.Base )
        {
            StartCoroutine(IsDead());
        }
    }

    private IEnumerator IsDead()
    {
        anim.SetDead();
        isdead = true;
        gameObject.tag = "Dead";
        GetComponent<NavCro>().Destroy();
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

}