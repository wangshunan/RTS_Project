using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergieUICro : MonoBehaviour {

    [SerializeField]
    public Slider energie;

    private void Awake()
    {
        energie = GetComponent<Slider>();
    }

    private void Start()
    {
        energie.value = 0;
    }

    private void Update()
    {
        if (energie.value < energie.maxValue)
        {
            energie.value += Time.deltaTime / 2;
        }
    }

    public void ConsumeEnergie( float unitCost )
    {
        energie.value -= unitCost;
    }
}
