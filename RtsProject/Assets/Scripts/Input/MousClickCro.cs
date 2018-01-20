using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousClickCro : MonoBehaviour {

    [SerializeField]
    BaseSelectCro baseSelectCro;

    [SerializeField]
    SelectTargetManager selectTargetManager;

    [SerializeField]
    GameManager gameManager;

    public GameObject clickTarget;

    private bool selected;
    private Vector3 startPos = new Vector3();
    private Vector3 endPos = new Vector3();

    private void Awake()
    {
        baseSelectCro = GetComponent<BaseSelectCro>();
        gameManager = GameObject.Find("GameSystem").GetComponent<GameManager>();
        selectTargetManager = GameObject.Find("GameSystem").GetComponent<SelectTargetManager>();

        selected = false;
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

        MouseSelectCro();
        MouseClickCro();
    }

    private void MouseSelectCro()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            selected = true;

            selectTargetManager.SelectedUnitsClear();
        }

        if (Input.GetMouseButtonUp(0))
        {
            selected = false;
        }


        if (selected)
        {

            endPos = Input.mousePosition;
            foreach (GameObject g in selectTargetManager.units)
            {
                Vector3 tempPos = Camera.main.WorldToScreenPoint(g.transform.position);

                if (tempPos.x > startPos.x && tempPos.x < endPos.x && tempPos.y < startPos.y && tempPos.y > endPos.y)
                {
                    if (g != null)
                    {
                        selectTargetManager.AddSelectedUnits(g);
                    }
                }
            }
        }

    }

    private void MouseClickCro()
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

                if ( hit.collider.gameObject.tag == ObjNameManager.UNIT_PLAYER_TAG )
                {
                    selectTargetManager.SelectedUnitsClear();
                    selectTargetManager.AddSelectedUnits(hit.collider.gameObject);
                }

            }
        }

        if (Input.GetMouseButtonDown(1) ) {

            Ray ray = new Ray();
            RaycastHit hit = new RaycastHit();
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.name == ObjNameManager.BASE_ENEMY_NAME)
                {
                    foreach (GameObject g in selectTargetManager.selectedUnits)
                    {
                        g.GetComponent<UnitStatus>().target = hit.collider.gameObject;
                    }
                }
            }
        }
    }
}
