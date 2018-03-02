using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergieUICro : MonoBehaviour {

    [SerializeField]
    public Slider energie; // エネルギーゲージ

    [SerializeField]
    public Slider costGage; // コストゲージ

    [SerializeField]
    GameManager gameManager;

    private float energieSpeed; // ゲージ回復スピード

    private void Awake()
    {
        costGage = transform.FindChild("CostSlider").GetComponent<Slider>();
        energie = transform.FindChild("EnergieSlider").GetComponent<Slider>();
        gameManager = GameObject.Find("GameSystem").GetComponent<GameManager>();
    }

    private void Start()
    {
        costGage.value = 0;
        energie.value = 0;
        energieSpeed = 1.5f;
    }

    private void Update()
    {
        if ( gameManager.gameStatus != GameManager.GameStatus.Play )
        {
            return;
        }

        if ( energie.value < energie.maxValue )
        {
            energie.value += Time.deltaTime / energieSpeed * 2;
        }
    }

    // エネルギー回復
    public void ConsumeEnergie( float unitCost )
    {
        energie.value -= unitCost;
    }

    public void SetCostGage( float unitCost )
    {
        costGage.value = unitCost;
    }
}
