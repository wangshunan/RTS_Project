using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCro : MonoBehaviour {

    [SerializeField]
    ScoreManager scoreManager;

    [SerializeField]
    GameManager gameManager;

    public enum ScoreType
    {
        Player,
        Enemy
    }

    public ScoreType type;
    public const int MAX_SCORE = 3000;

    private Text maxScore;
    private Text nowScore;
    private int baseAmount;
    private float score;

    private void Awake()
    {
        gameManager = GameObject.Find("GameSystem").GetComponent<GameManager>();
        scoreManager = GameObject.Find("GameSystem").GetComponent<ScoreManager>();
        maxScore = transform.FindChild("MaxScore").gameObject.GetComponent<Text>();
        nowScore = transform.FindChild("NowScore").gameObject.GetComponent<Text>();
    }

    private void Start()
    {
        maxScore.text = MAX_SCORE.ToString();
        nowScore.text = 0.ToString();
        score = 0;
    }

    private void Update()
    {
        if ( gameManager.gameStatus != GameManager.GameStatus.Play )
        {
            return;
        }

        BaseAMTUpdate();
        ScoreUpdate();
    }

    private void BaseAMTUpdate()
    {
        switch(type)
        {
            case ScoreType.Player:
                baseAmount =  scoreManager.GetPlayerBase();
                break;
            case ScoreType.Enemy:
                baseAmount = scoreManager.GetEnemyBase();
                break;
        }
    }

    private void ScoreUpdate()
    {
        score += 6 * baseAmount * Time.deltaTime;

        if ( score >= MAX_SCORE )
        {
            score = MAX_SCORE;
        }

        if ( type == ScoreType.Player )
        {
            scoreManager.playerScore = score;
        } else {
            scoreManager.enemyScore = score;
        }

        nowScore.text = ((int)score).ToString();

    }

}
