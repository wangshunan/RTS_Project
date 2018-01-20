using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    bool select = false;

    Vector3 startPos = new Vector3();
    Vector3 endPos = new Vector3();

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            select = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            select = false;
        }

        Select();
    }

    void Select()
    {
        if(!select)
        {
            return;
        }

    }

    void OnMouseUpAsButton()
    {
        
    }
}
