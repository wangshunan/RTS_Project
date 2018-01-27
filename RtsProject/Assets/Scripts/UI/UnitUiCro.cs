using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitUiCro : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

    [SerializeField]
    BaseSelectCro baseSelectCro;

    [SerializeField]
    EnergieUICro energieCro;

    [SerializeField]
    UnitSelectUiCro UnitPanel;

    public float unitCost;

    private Animator anim;
    private GameObject clone;
    private const float OFFSET = 1.2f;
    private bool selected = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        baseSelectCro = GameObject.Find("GameSystem").GetComponent<BaseSelectCro>();
        energieCro = GameObject.Find("EnergieUI").GetComponent<EnergieUICro>();
        UnitPanel = GameObject.Find("UnitSelectPanel").GetComponent<UnitSelectUiCro>();
    }

    private void Update()
    {
        UnitProCro();
    }

    //　マウスが指す
    public void OnPointerEnter( PointerEventData data ) 
    {
        anim.SetTrigger("Vectory");
        gameObject.transform.localScale *= OFFSET;

        energieCro.SetCostGage(unitCost);

    }

    // マウスが離れる
    public void OnPointerExit( PointerEventData data )
    {
        anim.SetTrigger("UI_Idle");
        gameObject.transform.localScale /= OFFSET;

        energieCro.SetCostGage(0f);
    }

    // ユニット選択
    public void OnPointerClick( PointerEventData data )
    {
        if ( selected )
        {
            return;
        }

        if ( energieCro.energie.value >= unitCost )
        {   
            selected = true;
            UnitClone();
            anim.SetTrigger("Vectory");
            gameObject.transform.localScale *= OFFSET;
            UnitPanel.OffSelectPanel();
        }
    }

    //　ユニット生産コントロール
    private void UnitProCro()
    {
        if (!selected)
        {
            return;
        }

        Ray ray = new Ray();
        RaycastHit hit = new RaycastHit();
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.tag == "Ground")
            {
                clone.transform.position = hit.point;
            }

        }

        if (Input.GetMouseButtonDown(0))
        {
            baseSelectCro.ProUnit(gameObject.name, clone.transform.position);
            energieCro.ConsumeEnergie(unitCost);
        }

        if (Input.GetMouseButtonDown(1))
        {
            selected = false;
            Destroy(clone);
            UnitPanel.OnSelectPanel();
        }

    }

    private void UnitClone()
    {
        clone = GameObject.Instantiate(gameObject) as GameObject;
        clone.transform.localScale = new Vector3( 0.7f, 0.7f, 0.7f );
        Destroy( clone.GetComponent <UnitUiCro> ());
    }

}
