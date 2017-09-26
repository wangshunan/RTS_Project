using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCro : MonoBehaviour {

    [SerializeField]
    ScoreManager scoreManager;

    public enum ScoreType
    {
        Player,
        Enemy
    }

    public ScoreType type;
    private Text maxScore;
    private Text nowScore;
    private int baseQuantity;
    private const int MAX_SCORE = 3000;
    private float score;

    private void Awake()
    {
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
        BaseQTYUpdate();
        ScoreUpdate();
    }

    private void BaseQTYUpdate()
    {
        switch(type)
        {
            case ScoreType.Player:
                baseQuantity =  scoreManager.GetPlayerBase();
                break;
            case ScoreType.Enemy:
                baseQuantity = scoreManager.GetEnemyBase();
                break;
        }
    }

    private void ScoreUpdate()
    {
        score += (6 * baseQuantity * Time.deltaTime);

        if ( score >= MAX_SCORE )
        {
            score = MAX_SCORE;
        }

        nowScore.text = ((int)score).ToString();
    }

}
