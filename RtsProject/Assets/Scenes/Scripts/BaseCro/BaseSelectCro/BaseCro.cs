using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BaseCro : MonoBehaviour {

    private GameObject baseArea;
    private Material areaMat;

    private void Awake()
    {
        baseArea = transform.FindChild("BaseArea").gameObject;
        areaMat = baseArea.GetComponent<Renderer>().material;
    }

    private void Start()
    {
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


    private void OnBaseArea()
    {
        baseArea.SetActive(true);
    }

    private void OffBaseArea()
    {
        baseArea.SetActive(false);
    }

}