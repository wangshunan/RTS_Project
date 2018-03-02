using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelectUiCro : MonoBehaviour {

    private Animator selectUIAnim;
    private int unitPanel = Animator.StringToHash("UnitPanel");
	private int unitSelection = Animator.StringToHash("UnitSelection");
	private bool _unitUiSelected;
	public bool unitUiSelected { get { return _unitUiSelected; } }

	private void Awake()
    {
        selectUIAnim = GetComponent<Animator>();
		_unitUiSelected = false;
    }

    public void OnSelectPanel()
    {
		_unitUiSelected = true;
		selectUIAnim.SetBool(unitPanel, true);
    }

    public void OffSelectPanel()
    {
		_unitUiSelected = false;
		selectUIAnim.SetBool(unitPanel, false);
    }

	public void OnUnitUiSelection()
	{
		_unitUiSelected = true;
		selectUIAnim.SetBool(unitSelection, true);
	}

	public void OffUnitUiSelection()
	{
		_unitUiSelected = false;
		selectUIAnim.SetBool(unitSelection, false);
	}
}