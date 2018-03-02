using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTargetManager : MonoBehaviour {

    private ArrayList _units;
    private ArrayList _selectedUnits;

    public ArrayList units { get { return _units; } }
    public ArrayList selectedUnits { get { return _selectedUnits; } }

    private void Awake()
    {
        _units = new ArrayList();
        _selectedUnits = new ArrayList();
    }

    public void AddUnits(GameObject unit)
    {
        units.Add(unit);
    }

    public void AddSelectedUnits(GameObject selectedUnit)
    {
        selectedUnits.Add(selectedUnit);
        if (!selectedUnit.GetComponent<SelectCro>().isSelected)
        {
            selectedUnit.GetComponent<SelectCro>().Select();
        }
    }

    public void DestroySelecetedUnits(GameObject selectedUnit)
    {
        foreach (GameObject g in _selectedUnits)
        {
			if (g == null)
			{
				continue;
			}

            if (selectedUnit.GetComponent<SelectCro>().isSelected && g == selectedUnit)
            {
                selectedUnit.GetComponent<SelectCro>().Deselect();
            }
        }
    }

    public void SelectedUnitsClear()
    {
        foreach ( GameObject g in selectedUnits )
        {
			if ( g != null )
            g.GetComponent<SelectCro>().Deselect();
        }
        selectedUnits.Clear();
    }
}
