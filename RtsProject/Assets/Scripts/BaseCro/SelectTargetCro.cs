using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectTargetCro : MonoBehaviour {

    [SerializeField]
    UnitSelectUiCro unitSelectCro;

    // 選択されたターゲット
    private GameObject selectBaseTarget;

    private void Awake()
    {
        unitSelectCro = GameObject.Find("UnitSelectPanel").GetComponent<UnitSelectUiCro>();
    }
	

    public void SetTarget( GameObject target )
    {
        if (selectBaseTarget != null )
        {
            ResetTarget();
        }

        selectBaseTarget = target;
        selectBaseTarget.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        unitSelectCro.OnSelectPanel();
    }

    public void ResetTarget()
    {
        selectBaseTarget.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        unitSelectCro.OffSelectPanel();
        selectBaseTarget = null;
    }

    // 選択されたBaseにUnit生産
    public void ProUnit( string unitName )
    {
        if (selectBaseTarget == null )
        {
            return;
        }

        var targetCro = selectBaseTarget.GetComponent<BaseCro>();

        targetCro.PlayerProductionUnit( unitName, ObjNameManager.UNIT_PLAYER_TAG );
    }
}