﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheelCro : MonoBehaviour {

    [SerializeField]
    GameManager gameManager;

    public GameObject target;
    public string parentTag;
    public float speed;
    public int damage;

    private void Awake()
    {
        gameManager = GameObject.Find("GameSystem").GetComponent<GameManager>();
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    void Update() {

        if ( gameManager.gameStatus != GameManager.GameStatus.Play)
        {
            return;
        }

        ShellCro();
	}

    void ShellCro()
    {
        if ( target == null )
        {
            Destroy(gameObject);
            return;
        }

        float movespeed = speed * Time.deltaTime;
        var targetPos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);

        if ( target.GetComponent<UnitStatus>() != null && target.GetComponent<UnitStatus>().type  == UnitStatus.UnitType.Fly )
        {
            targetPos = target.transform.FindChild("Hips/BodyPos").transform.position; 
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, movespeed);

    }

    void OnCollisionEnter(Collision collision)
    {


        if ( target != null )
        {
            var targetCro = target.GetComponent<BattleDisposition>();

            if ( target.tag == ObjNameManager.BASE_TAG )
            {
                targetCro.BaseGetHit(damage, parentTag);
                Destroy(gameObject);
                return;
            }

            targetCro.GetHit(damage);
        }

        Destroy(gameObject);
    }
}
