using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BaseCro : MonoBehaviour {

    public enum BaseType
    {
        Neutral,
        Player,
        Enemy
    }

    private GameObject baseArea;
    private Material areaMat;
    private Vector3 instPos;
    private string unitTag;
    private string prefabStr;

    public BaseType type;

    private void Awake()
    {
        baseArea = transform.FindChild("BaseArea").gameObject;
        areaMat = baseArea.GetComponent<Renderer>().material;
        prefabStr = "Prefabs/";
    }

    private void Start()
    {
        instPos = transform.FindChild("InstPos").gameObject.transform.position;
        areaMat.SetVector("_AreaPos", gameObject.transform.position);
        baseArea.SetActive(false);
        SetBaseTag();
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

    private void SetBaseTag()
    {
        switch (type)
        {
            case BaseType.Player:
                gameObject.name = "Base_Player";
                unitTag = "Player";
                break;
            case BaseType.Enemy:
                gameObject.name = "Base_Enemey";
                unitTag = "Enemy";
                break;
            case BaseType.Neutral:
                gameObject.name = "Base_Neutral";
                break;
        }
    }

}