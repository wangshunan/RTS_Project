using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseSelectCro : MonoBehaviour {

    [SerializeField]
    UnitSelectUiCro unitSelectCro;

    // 選択されたターゲット
    private GameObject selectBaseTarget;

    private void Awake()
    {
        unitSelectCro = GameObject.Find("UnitSelectPanel").GetComponent<UnitSelectUiCro>();
    }
	
    // ターゲット設定
    public void SetTarget( GameObject target )
    {
        if (selectBaseTarget != null )
        {
            ResetTarget();
        }

        selectBaseTarget = target;
        selectBaseTarget.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
		selectBaseTarget.GetComponent<BaseCro>().OnUnitProPanel();
        unitSelectCro.OnSelectPanel();
    }

    // ターゲットリセット
    public void ResetTarget()
    {
        selectBaseTarget.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
		selectBaseTarget.GetComponent<BaseCro>().OffUnitProPanel();
		unitSelectCro.OffSelectPanel();
        selectBaseTarget = null;
    }

    // 選択されたBaseにUnit生産
    public void ProUnit( string unitName, Vector3 unitPos )
    {
        if (selectBaseTarget == null )
        {
            return;
        }

        var targetCro = selectBaseTarget.GetComponent<BaseCro>();

        targetCro.PlayerProductionUnit( unitName, ObjNameManager.UNIT_PLAYER_TAG, unitPos );
    }
}