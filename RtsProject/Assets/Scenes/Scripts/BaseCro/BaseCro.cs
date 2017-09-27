using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BaseCro : MonoBehaviour {

    [SerializeField]
    Status status;

    [SerializeField]
    BaseInvadCro baseInvad;

    private GameObject baseArea;
    private Material areaMat;
    private Vector3 instPos;
    private string unitTag;
    private string prefabStr;

    private void Awake()
    {
        baseInvad = GameObject.Find("GameSystem").GetComponent<BaseInvadCro>();
        status = GetComponent<Status>();
        baseArea = transform.FindChild("BaseArea").gameObject;
        areaMat = baseArea.GetComponent<Renderer>().material;
        prefabStr = "Prefabs/Unit/";
    }

    private void Start()
    {
        instPos = transform.FindChild("InstPos").gameObject.transform.position;
        areaMat.SetVector("_AreaPos", gameObject.transform.position);
        baseArea.SetActive(false);
    }

    public void SetBaseArea( bool areaSwitch )
    {
        if ( areaSwitch )
        {
            OnBaseArea();
        } else {
            OffBaseArea();
        }
    }
    
    public void ProductionUnit( string targetName )
    {
        string unitName = string.Concat(prefabStr, targetName);
        GameObject unit = Resources.Load(unitName) as GameObject;
        unit.tag = unitTag;
        Instantiate(unit, instPos, transform.rotation);
    }

    private void OnBaseArea()
    {
        baseArea.SetActive(true);
    }

    private void OffBaseArea()
    {
        baseArea.SetActive(false);
    }

    void BaseInvaded()
    {
        if ( status.hp <= 0 )
        {
            gameObject.tag = "Dead";
            baseInvad.BaseDestroy(gameObject, status.killerTag);
        }
    }

}