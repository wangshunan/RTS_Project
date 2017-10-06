using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelectUiCro : MonoBehaviour {

    private Animator selectUIAnim;

    private int select = Animator.StringToHash("UnitPanel");

    private void Awake()
    {
        selectUIAnim = GetComponent<Animator>();
    }

    public void OnSelectPanel()
    {
        selectUIAnim.SetBool(select,true);
    }

    public void OffSelectPanel()
    {
        selectUIAnim.SetBool(select, false);
    }
}