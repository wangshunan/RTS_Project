using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BaseCro : MonoBehaviour {

    [SerializeField]
    SelectTargetManager selectTargetManager;

    private Vector3 instPos;
    private string playerPreLink; // unit ファイルリンク
    private string enemyPreLink; // unit ファイルリンク
    private Renderer render; // baseRenderer

    private void Awake()
    {
        render = GetComponent<Renderer>();
        selectTargetManager = GameObject.Find("GameSystem").GetComponent<SelectTargetManager>();
        playerPreLink = "Prefabs/Unit/PlayerTeam/";
        enemyPreLink = "Prefabs/Unit/EnemyTeam/";
    }

    // base初期化
    private void Start()
    {
        // 生成pos
        instPos = transform.FindChild("InstPos").gameObject.transform.position;
        if ( gameObject.name == ObjNameManager.BASE_ENEMY_NAME) {
            render.material.SetColor("_Color", Color.red);
        } else {
            render.material.SetColor("_Color", Color.white);
        }
    }

    // Playerユニット生産
    public void PlayerProductionUnit( string name, string unitTag, Vector3 pos )
    {
        string unitLink = string.Concat(playerPreLink, name);
        GameObject unit = Resources.Load(unitLink) as GameObject;

        var obj = Instantiate(unit, pos, transform.rotation);
        obj.tag = unitTag;
        obj.name = name;

        selectTargetManager.AddUnits(obj);
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