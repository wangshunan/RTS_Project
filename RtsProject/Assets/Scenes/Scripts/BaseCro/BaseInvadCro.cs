using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInvadCro : MonoBehaviour {

    private Vector3 desBasePos;
    private string baseName;
    private int baseType;

    public void BaseDestroy( GameObject desBase, string unitTag )
    {
        if (unitTag == "Player")
        {
            baseName = "Base_Enemy";
        } else {
            baseName = "Base_Player";
        }
        desBasePos = desBase.transform.position;
        DestroyBase(desBase);
    }

    public void BaseCreation()
    {
        GameObject newBase = Resources.Load("Prefabs/Base/Base") as GameObject;
        newBase.tag = "Base";
        newBase.name = baseName;

        Instantiate(newBase, desBasePos, Quaternion.identity);
    }

    IEnumerator DestroyBase( GameObject desbase )
    {
        Destroy(desbase);

        yield return new WaitForSeconds(3);
        BaseCreation();
    }

}