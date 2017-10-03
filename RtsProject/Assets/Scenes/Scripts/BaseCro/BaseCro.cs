using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BaseCro : MonoBehaviour {

    [SerializeField]
    Status status;

    private Vector3 instPos;
    private string prefabStr;
    private Renderer render;

    private void Awake()
    {
        status = GetComponent<Status>();
        render = GetComponent<Renderer>();
        prefabStr = "Prefabs/Unit/";
    }

    private void Start()
    {
        instPos = transform.FindChild("InstPos").gameObject.transform.position;
        if ( gameObject.name == ObjNameManager.BASE_ENEMY_NAME) {
            render.material.SetColor("_Color", Color.red);
        } else {
            render.material.SetColor("_Color", Color.white);
        }
    }

    
    public void PlayerProductionUnit( string name, string tag )
    {
        string unitName = string.Concat(prefabStr, name);
        GameObject unit = Resources.Load(unitName) as GameObject;
        var status = unit.GetComponent<Status>();

        unit.tag = tag;
        Instantiate(unit, instPos, transform.rotation);
    }

    public void EnemyProductionUnit( string name, string tag )
    {
        string unitName = string.Concat(prefabStr, name);
        GameObject unit = Resources.Load(unitName) as GameObject;
        var status = unit.GetComponent<Status>();

        unit.tag = tag;
        Instantiate(unit, instPos, transform.rotation);
    }

}