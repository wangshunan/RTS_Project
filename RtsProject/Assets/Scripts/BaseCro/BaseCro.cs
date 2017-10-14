﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BaseCro : MonoBehaviour {

    [SerializeField]
    Status status;

    private Vector3 instPos;
    private string playerPreLink;
    private string enemyPreLink;
    private Renderer render;

    private void Awake()
    {
        status = GetComponent<Status>();
        render = GetComponent<Renderer>();
        playerPreLink = "Prefabs/Unit/PlayerTeam/";
        enemyPreLink = "Prefabs/Unit/EnemyTeam/";
    }

    // base初期化
    private void Start()
    {
        instPos = transform.FindChild("InstPos").gameObject.transform.position;
        if ( gameObject.name == ObjNameManager.BASE_ENEMY_NAME) {
            render.material.SetColor("_Color", Color.red);
        } else {
            render.material.SetColor("_Color", Color.white);
        }
    }

    // Playerユニット生産
    public void PlayerProductionUnit( string name, string unitTag )
    {
        string unitLink = string.Concat(playerPreLink, name);
        GameObject unit = Resources.Load(unitLink) as GameObject;

        var obj = Instantiate(unit, instPos, transform.rotation);
        obj.tag = unitTag;
        obj.name = name;
    }

    //　Enemyユニット生産
    public void EnemyProductionUnit( string name, string unitTag )
    {
        string unitLink = string.Concat(enemyPreLink, name);
        GameObject unit = Resources.Load(unitLink) as GameObject;

        unit.tag = unitTag;
        Instantiate(unit, instPos, transform.rotation);
    }

}