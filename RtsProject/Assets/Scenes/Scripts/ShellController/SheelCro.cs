using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheelCro : MonoBehaviour {

    public GameObject target;
    public float speed;
    public int damage;

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    void Update() {
        ShotShell();
	}

    void ShotShell()
    {
        if ( target.tag == ObjNameManager.STATUS_DEAD_TAG )
        {
            Destroy(gameObject);
        }

        Status.UnitType targetType = target.GetComponent<Status>().type;
        float movespeed = speed * Time.deltaTime;
        var targetPos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);

        if ( targetType == Status.UnitType.Fly )
        {
            targetPos = target.transform.FindChild("Hips/BodyPos").transform.position;

            
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, movespeed);

    }

    void OnCollisionEnter(Collision collision)
    {
        var targetCro = target.GetComponent<BattleDisposition>();
        if ( targetCro != null )
        {
            targetCro.GetHit(damage);
        }
        Destroy(gameObject);
    }
}
