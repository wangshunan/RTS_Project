using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    //生成するゲームオブジェクト
    public GameObject target;
    public GameObject inspoint;

    void Update()
    {
        //スペースを押したら
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Instantiate( 生成するオブジェクト,  場所, 回転 );  回転はそのままなら↓
            Instantiate(target, inspoint.transform.position, transform.rotation);
        }
    }
}
