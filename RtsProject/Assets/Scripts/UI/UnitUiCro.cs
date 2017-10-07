using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitUiCro : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

    [SerializeField]
    BaseSelectCro baseSelectCro;

    [SerializeField]
    EnergieUICro energieCro;

    public float unitCost;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        baseSelectCro = GameObject.Find("GameSystem").GetComponent<BaseSelectCro>();
        energieCro = GameObject.Find("EnergieSlider").GetComponent<EnergieUICro>();
    }

    public void OnPointerEnter( PointerEventData data ) 
    {
        anim.SetTrigger("Vectory");
        gameObject.transform.localScale *= 1.2f;
    }

    public void OnPointerExit( PointerEventData data )
    {
        anim.SetTrigger("UI_Idle");
        gameObject.transform.localScale /= 1.2f;
    }

    public void OnPointerClick( PointerEventData data )
    {
        if ( energieCro.energie.value >= unitCost )
        {
            baseSelectCro.ProUnit(gameObject.name);
            energieCro.ConsumeEnergie(unitCost);
        }
    }
}
