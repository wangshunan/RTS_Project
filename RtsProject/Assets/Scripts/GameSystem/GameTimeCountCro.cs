using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeCountCro : MonoBehaviour {

    [SerializeField]
    GameManager gameManager;

    private const int MINUTE_MAX = 3; //　最大時間：分
    private const int SECOND_MAX = 60; // 最大時間：秒

    public int nowMinute; // 現在の時間：分
    public float nowSecond; // 現在の時間: 秒
    public bool timeOver; // タイムオーバー

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

    // タイムカウント
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

    // 現在の時間：分
    public int GetNowMinute()
    {
        return nowMinute;
    }


    // 現在の時間：秒
    public int GetNowSecond()
    {
        return (int)nowSecond;
    }
}
