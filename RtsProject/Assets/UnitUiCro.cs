using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitUiCro : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
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
}
