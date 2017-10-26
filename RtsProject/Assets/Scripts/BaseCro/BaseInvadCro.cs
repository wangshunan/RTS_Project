using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInvadCro : MonoBehaviour {

    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    UnitSelectUiCro unitSelectCro;

    private Vector3 destBasePos;
    private string baseName;
    private string playerBaseLik; // base ファイルリンク
    private string enemyBaseLing; // base ファイルリンク

    private void Awake()
    {
        unitSelectCro = GameObject.Find("UnitSelectPanel").GetComponent<UnitSelectUiCro>();
        gameManager = GameObject.Find("GameSystem").GetComponent<GameManager>();
    }

    private void Start()
    {
        playerBaseLik = "Prefabs/Base/Base";
        enemyBaseLing = "Prefabs/Base/Base";
    }

    // Base破棄
    public void BaseDestroy( GameObject desBase, string unitTag )
    {
        string baseLink = null;

        // 次生成するbase名前を設定
        if ( unitTag == ObjNameManager.UNIT_PLAYER_TAG )
        {
            baseName = ObjNameManager.BASE_PLAYER_NAME;
            baseLink = playerBaseLik;
        } else {
            baseName = ObjNameManager.BASE_ENEMY_NAME;
            baseLink = enemyBaseLing;
        }

        // Playerの場合はunitUIをリセット
        if ( desBase.tag == ObjNameManager.BASE_PLAYER_NAME )
        {
            unitSelectCro.OffSelectPanel();
        }

        destBasePos = desBase.transform.position;
        Destroy(desBase);
        StartCoroutine(Creation(baseLink));
    }

    // Base生成
    public void BaseCreation( string baseLink )
    {
        if ( gameManager.gameStatus != GameManager.GameStatus.Play )
        {
            return;
        }

        GameObject newBase = Resources.Load("Prefabs/Base/Base") as GameObject;
        newBase.tag = ObjNameManager.BASE_TAG;

        var obj = Instantiate(newBase, destBasePos, Quaternion.identity) as GameObject;
        obj.transform.Rotate(new Vector3(1, 0, 0), -90); 
        obj.name = baseName;

    }

    private IEnumerator Creation( string link )
    {
        yield return new WaitForSeconds(3);
        BaseCreation( link );
    }

}