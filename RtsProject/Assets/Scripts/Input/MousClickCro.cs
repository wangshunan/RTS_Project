using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousClickCro : MonoBehaviour {

    [SerializeField]
    BaseSelectCro baseSelectCro;

    [SerializeField]
    GameManager gameManager;

    private GameObject clickTarget;

    private void Awake()
    {
        baseSelectCro = GetComponent<BaseSelectCro>();
        gameManager = GameObject.Find("GameSystem").GetComponent<GameManager>();
    }

    private void Start()
    {
        clickTarget = null;
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

            if ( Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity) )
            {
                if ( hit.collider.gameObject.name == ObjNameManager.BASE_PLAYER_NAME )
                {
                    clickTarget = hit.collider.gameObject;
                    baseSelectCro.SetTarget(clickTarget);
                }
            }
        }
    }
}
