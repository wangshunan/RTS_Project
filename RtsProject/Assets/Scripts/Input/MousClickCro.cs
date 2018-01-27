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

    private GameObject _clickTarget { get { return clickTarget; } }
    private bool selected;
    private Vector3 startPos = new Vector3();
    private Vector3 endPos = new Vector3();
    private new LineRenderer renderer;

    private void Awake()
    {
        baseSelectCro = GameObject.Find("GameSystem").GetComponent<BaseSelectCro>();
        gameManager = GameObject.Find("GameSystem").GetComponent<GameManager>();
        selectTargetManager = GameObject.Find("GameSystem").GetComponent<SelectTargetManager>();
        renderer = gameObject.GetComponent<LineRenderer>();
        
        selected = false;
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
            renderer.positionCount = 0;
        }


        if (selected)
        {

            endPos = Input.mousePosition;
            selectedTargetCheck();
            DrawSelectedLine( startPos, endPos);
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

    private void selectedTargetCheck()
    {
        foreach (GameObject g in selectTargetManager.units)
        {
            Vector3 tempPos = Camera.main.WorldToScreenPoint(g.transform.position);

            if (tempPos.x > startPos.x && tempPos.x < endPos.x && tempPos.y < startPos.y && tempPos.y > endPos.y)
            {
                if (g != null)
                {
                    selectTargetManager.AddSelectedUnits(g);
                }
            } else if (tempPos.x < startPos.x && tempPos.x > endPos.x && tempPos.y > startPos.y && tempPos.y < endPos.y)
            {
                if (g != null)
                {
                    selectTargetManager.AddSelectedUnits(g);
                }
            } else if (tempPos.x < startPos.x && tempPos.x > endPos.x && tempPos.y > startPos.y && tempPos.y < endPos.y)
            {
                if (g != null)
                {
                    selectTargetManager.AddSelectedUnits(g);
                }
            } else if (tempPos.x < startPos.x && tempPos.x > endPos.x && tempPos.y < startPos.y && tempPos.y > endPos.y)
            {
                if (g != null)
                {
                    selectTargetManager.AddSelectedUnits(g);
                }
            } else if (tempPos.x > startPos.x && tempPos.x < endPos.x && tempPos.y > startPos.y && tempPos.y < endPos.y)
            {
                if (g != null)
                {
                    selectTargetManager.AddSelectedUnits(g);
                }
            } else
            {
                selectTargetManager.DestroySelecetedUnits(g);
            }
        }
    }

    private void DrawSelectedLine( Vector3 start, Vector3 end )
    {
        Vector3 pos1 = Camera.main.ScreenToWorldPoint(new Vector3(start.x, start.y, 1.0f));
        Vector3 pos2 = Camera.main.ScreenToWorldPoint(new Vector3(start.x, end.y, 1.0f));
        Vector3 pos3 = Camera.main.ScreenToWorldPoint(new Vector3(end.x, end.y, 1.0f));
        Vector3 pos4 = Camera.main.ScreenToWorldPoint(new Vector3(end.x, start.y, 1.0f));

        renderer.startWidth = 0.003f;
        renderer.endWidth = 0.003f;
        renderer.positionCount = 5;

        renderer.SetPosition(0, pos1);
        renderer.SetPosition(1, pos2);
        renderer.SetPosition(2, pos3);
        renderer.SetPosition(3, pos4);
        renderer.SetPosition(4, new Vector3 ( pos1.x, pos1.y, pos1.z - 0.0015f ));
    }
}
