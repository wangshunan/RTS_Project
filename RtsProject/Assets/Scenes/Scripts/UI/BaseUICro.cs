using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUICro : MonoBehaviour {

    public Camera rotateCamera;

    // Use this for initialization
    void Start () {
        rotateCamera = Camera.main;
    }
	
	// Update is called once per frame
	void Update () {
        LookAtCamera();
    }

    private void LookAtCamera()
    {
        transform.rotation = rotateCamera.transform.rotation;
    }
}