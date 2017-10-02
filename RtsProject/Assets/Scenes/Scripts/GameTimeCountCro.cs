using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeCountCro : MonoBehaviour {

    [SerializeField]
    GameManager gameManager;

    private const int MINUTE_MAX = 3;
    private const int SECOND_MAX = 60;

    public int nowMinute;
    public float nowSecond;
    public bool timeOver;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }

    private void Start()
    {
        nowMinute = MINUTE_MAX;
        nowSecond = 0;
        timeOver = false;
    }

    private void Update()
    {
        if ( gameManager.gameStatus != GameManager.GameStatus.Play )
        {
            return;
        }

        TimeCountUpDate();
    }

    private void TimeCountUpDate()
    {
        if ( nowSecond > 0 )
        {
            nowSecond -= Time.deltaTime;
        } else {
            nowMinute--;
            nowSecond = SECOND_MAX;
        }

        if ( nowMinute == 0 && nowSecond <= 0 )
        {
            timeOver = true;
        }
    }


    public int GetNowMinute()
    {
        return nowMinute;
    }

    public int GetNowSecond()
    {
        return (int)nowSecond;
    }
}
