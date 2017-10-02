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

    private void Awake()
    {
        status = GetComponent<Status>();
        prefabStr = "Prefabs/Unit/";
    }

    private void Start()
    {
        instPos = transform.FindChild("InstPos").gameObject.transform.position;
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