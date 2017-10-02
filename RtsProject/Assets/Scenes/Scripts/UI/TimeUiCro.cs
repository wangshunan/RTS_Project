using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUiCro : MonoBehaviour {

    [SerializeField]
    Text timeText;

    [SerializeField]
    GameTimeCountCro gameTimeCro;

    private int nowMinute;
    private int nowSecond;

    private void Awake()
    {
        timeText = transform.FindChild("TimeText").GetComponent<Text>();
        gameTimeCro = GameObject.Find("GameSystem").GetComponent<GameTimeCountCro>();
    }

    private void Start()
    {
        nowMinute = gameTimeCro.GetNowMinute();
        nowSecond = gameTimeCro.GetNowSecond();
    }

    private void Update()
    {
        nowMinute = gameTimeCro.GetNowMinute();
        nowSecond = gameTimeCro.GetNowSecond();

        string timeViewStr = nowMinute + ":" + nowSecond;
        timeText.text = timeViewStr;
    }
}
