using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitUiCro : MonoBehaviour {

    public Camera rotateCamera;

    // Use this for initialization
    void Start () {
        transform.rotation = rotateCamera.transform.rotation;
    }
	
	// Update is called once per frame
	void Update () {
    }
}
