using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelectUiCro : MonoBehaviour {

    [SerializeField]
    Animator selectAnim;

    private int select = Animator.StringToHash("UnitPanel"); 

    private void Awake()
    {
        selectAnim = GetComponent<Animator>();
    }

    public void OnSelectPanel()
    {
        selectAnim.SetBool(select,true);
    }

    public void OffSelectPanel()
    {
        selectAnim.SetBool(select, false);
    }
}