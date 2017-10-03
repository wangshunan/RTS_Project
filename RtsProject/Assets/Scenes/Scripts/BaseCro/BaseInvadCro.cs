using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInvadCro : MonoBehaviour {

    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    UnitSelectUiCro unitSelectCro;

    private Vector3 desBasePos;
    private string baseName;

    private void Awake()
    {
        unitSelectCro = GameObject.Find("UnitSelectPanel").GetComponent<UnitSelectUiCro>();
        gameManager = GameObject.Find("GameSystem").GetComponent<GameManager>();
    }

    public void BaseDestroy( GameObject desBase, string unitTag )
    {
        if ( unitTag == ObjNameManager.UNIT_PLAYER_TAG )
        {
            baseName = ObjNameManager.BASE_PLAYER_NAME;
        } else {
            baseName = ObjNameManager.BASE_ENEMY_NAME;
        }

        if ( desBase.tag == ObjNameManager.BASE_PLAYER_NAME )
        {
            unitSelectCro.OffSelectPanel();
        }

        desBasePos = desBase.transform.position;
        Destroy(desBase);
        StartCoroutine(Creation());
    }

    public void BaseCreation()
    {
        if ( gameManager.gameStatus != GameManager.GameStatus.Play )
        {
            return;
        }

        GameObject newBase = Resources.Load("Prefabs/Base/Base") as GameObject;
        newBase.tag = ObjNameManager.BASE_TAG;

        var obj = Instantiate(newBase, desBasePos, Quaternion.identity) as GameObject;
        obj.transform.Rotate(new Vector3(1, 0, 0), -90); 
        obj.name = baseName;

    }

    private IEnumerator Creation()
    {
        yield return new WaitForSeconds(3);
        BaseCreation();
    }

}