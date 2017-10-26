using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    ScoreManager scoreManager;

    [SerializeField]
    GameTimeCountCro gameTimeCro;

    public enum GameStatus
    {
        Vectory,
        Lose,
        Play,
        Stop
    }

    public GameStatus gameStatus;

    private void Awake()
    {
        scoreManager = GetComponent<ScoreManager>();
        gameTimeCro = GetComponent<GameTimeCountCro>();
    }

    private void Start()
    {
        gameStatus = GameStatus.Stop;
    }

    private void FixedUpdate()
    {
        GameStatusCheck(); // ゲームステート監視
    }

    public void GameStatusCheck()
    {
        if ( scoreManager.playerScore == ScoreCro.MAX_SCORE )
        {
            gameStatus = GameStatus.Vectory;
        }

        if ( scoreManager.enemyScore == ScoreCro.MAX_SCORE )
        {
            gameStatus = GameStatus.Lose;
        }

        if ( scoreManager.enemyBasesAmount == 0 )
        {
            gameStatus = GameStatus.Vectory;
        }

        if ( scoreManager.playerBaseAmount == 0 )
        {
            gameStatus = GameStatus.Lose;
        }

        if (gameTimeCro.timeOver)
        {
            if (scoreManager.playerBaseAmount > scoreManager.enemyBasesAmount)
            {
                gameStatus = GameStatus.Vectory;
            }
            else
            {
                gameStatus = GameStatus.Lose;
            }
        }

        if ( gameStatus != GameStatus.Play && gameStatus != GameStatus.Stop ) {

        }
    }

}
