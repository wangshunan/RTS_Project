using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeManager : MonoBehaviour {

    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    GameTimeCountCro gameTimeCro;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
        gameTimeCro = GetComponent<GameTimeCountCro>();
    }

    private void Update()
    {
        if ( gameTimeCro.timeOver )
        {
            gameManager.GameStatusCheck();
        }
    }
}
