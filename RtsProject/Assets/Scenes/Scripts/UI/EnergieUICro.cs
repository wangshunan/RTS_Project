using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergieUICro : MonoBehaviour {

    [SerializeField]
    public Slider energie;

    [SerializeField]
    GameManager gameManager;

    private void Awake()
    {
        energie = GetComponent<Slider>();
        gameManager = GameObject.Find("GameSystem").GetComponent<GameManager>();
    }

    private void Start()
    {
        energie.value = 0;
    }

    private void Update()
    {
        if ( gameManager.gameStatus != GameManager.GameStatus.Play )
        {
            return;
        }

        if (energie.value < energie.maxValue)
        {
            energie.value += Time.deltaTime / 1.5f;
        }
    }

    public void ConsumeEnergie( float unitCost )
    {
        energie.value -= unitCost;
    }
}
