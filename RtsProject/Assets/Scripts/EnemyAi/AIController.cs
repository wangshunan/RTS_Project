using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    private List<BaseCro> baseCro;

    [SerializeField]
    ScoreManager baseAmount;

    public GameObject targetBase;

    private float count; // 生成カウント
    private float specialCount; // 特殊ユニットの生成カウント
    private string unitName;

    private void Awake()
    {
        gameManager = GameObject.Find("GameSystem").GetComponent<GameManager>();
        baseAmount = GameObject.Find("GameSystem").GetComponent<ScoreManager>();
    }

    private void Start()
    {
        baseCro = new List<BaseCro>();
        unitName = "Footman";
        count = 0;
        specialCount = 0;
    }

    private void Update()
    {
        if ( gameManager.gameStatus != GameManager.GameStatus.Play )
        {
            return;
        }

        SearchBase();
        UnitProduction();
    }

    // ユニット生成コントロール
    private void UnitProduction()
    {
        count += Time.deltaTime;

        if ( count >= 5 )
        {
            for ( int i = 0; i < baseCro.Count; i++ )
            {
                baseCro[i].EnemyProductionUnit(unitName, ObjNameManager.UNIT_ENEMY_TAG);
            }

            specialCount++;
            count = 0;
        }

        if ( specialCount == 4 )
        {
            for (int i = 0; i < baseCro.Count; i++)
            {
                baseCro[i].EnemyProductionUnit("Archer", ObjNameManager.UNIT_ENEMY_TAG);
                baseCro[i].EnemyProductionUnit("Knight", ObjNameManager.UNIT_ENEMY_TAG);
            }
            specialCount++;
        } 

        if ( specialCount == 10 )
        {
            for (int i = 0; i < baseCro.Count; i++)
            {
                baseCro[i].EnemyProductionUnit("Horseman", ObjNameManager.UNIT_ENEMY_TAG);
            }

            specialCount = 0;
        }

        if ( baseAmount.enemyBasesAmount == 1 )
        {
            unitName = "Knight";
        }

    }

    // Base探す
    private void SearchBase()
    {
        GameObject[] bases;

        bases = GameObject.FindGameObjectsWithTag(ObjNameManager.BASE_TAG);

        baseCro.Clear();

        for ( int i = 0; i < bases.Length; i++ )
        {
            if ( bases[i].name == ObjNameManager.BASE_ENEMY_NAME )
            {
                baseCro.Add(bases[i].GetComponent<BaseCro>());
            }
        }

    }

}
