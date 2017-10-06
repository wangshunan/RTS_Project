using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergieUICro : MonoBehaviour {

    [SerializeField]
    public Slider energie;

    [SerializeField]
    GameManager gameManager;

    private float energieSpeed;

    private void Awake()
    {
        energie = GetComponent<Slider>();
        gameManager = GameObject.Find("GameSystem").GetComponent<GameManager>();
    }

    private void Start()
    {
        energie.value = 0;
        energieSpeed = 1.5f;
    }

    private void Update()
    {
        if ( gameManager.gameStatus != GameManager.GameStatus.Play )
        {
            return;
        }

        if (energie.value < energie.maxValue)
        {
            energie.value += Time.deltaTime / energieSpeed;
        }
    }

    public void ConsumeEnergie( float unitCost )
    {
        energie.value -= unitCost;
    }
}
