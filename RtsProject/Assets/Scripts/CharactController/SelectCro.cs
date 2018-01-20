using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCro : MonoBehaviour {

    [SerializeField]
    HpCanvasCro hpCanvas;

    private GameObject highLight;
    private bool selected;

    public bool isSelected { get { return selected; } }

    void Awake () {
        highLight = gameObject.transform.FindChild("Selected").gameObject;
        hpCanvas = gameObject.transform.FindChild("HpCanvas").gameObject.GetComponent<HpCanvasCro>();

    }

    private void Start()
    {
        selected = false;
        highLight.SetActive(false);
    }

    public void Deselect()
    {
        selected = false;
        highLight.SetActive(false);

        hpCanvas.OffDrawSlider();
    }

    public void Select()
    {
        selected = true;
        highLight.SetActive(true);

        hpCanvas.OnDrawSlider();
    }

}
