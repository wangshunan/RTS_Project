using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectTargetCro : MonoBehaviour {

    [SerializeField]
    UIManager UICro;

    [SerializeField]
    UnitSelectUiCro unitSelectCro;

    GameObject selectTarget;

    private void Awake()
    {
        UICro = GameObject.Find("UnitSelectPanel").GetComponent<UIManager>();
        unitSelectCro = GameObject.Find("UnitSelectPanel").GetComponent<UnitSelectUiCro>();
    }
	

    public void SetTarget( GameObject target )
    {
        if ( selectTarget != null )
        {
            ResetTarget();
        }

        selectTarget = target;
        selectTarget.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        unitSelectCro.OnSelectPanel();
    }

    public void ResetTarget()
    {
        if( selectTarget == null )
        {
            return;
        }

        selectTarget.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        unitSelectCro.OffSelectPanel();
        selectTarget = null;
    }
}
