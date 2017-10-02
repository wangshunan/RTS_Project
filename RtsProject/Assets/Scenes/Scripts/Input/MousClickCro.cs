using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousClickCro : MonoBehaviour {

    [SerializeField]
    SelectTargetCro selectTarget;

    [SerializeField]
    GameManager gameManager;

    private GameObject ClickTarget;
    private string targetName;

    private void Awake()
    {
        selectTarget = GetComponent<SelectTargetCro>();
        gameManager = GameObject.Find("GameSystem").GetComponent<GameManager>();
    }

    private void Start()
    {
        targetName = "Base_Player";
        ClickTarget = null;
    }

    private void Update()
    {
        if ( gameManager.gameStatus != GameManager.GameStatus.Play ) {
            return;
        }

        MouserClickCro();
    } 

    private void MouserClickCro()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray();
            RaycastHit hit = new RaycastHit();
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //マウスクリックした場所からRayを飛ばし、オブジェクトがあればtrue 
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.name == targetName)
                {
                    ClickTarget = hit.collider.gameObject;
                    selectTarget.SetTarget( ClickTarget );
                }
            }
        }
    }
}
