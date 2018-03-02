using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitStatus : MonoBehaviour
{

	[SerializeField]
	BaseInvadCro baseInvad;

	public enum UnitType
	{
		Strike,
		Shot,
		Fly,
		Base
	}

	// Public
	public int hp;
	public int atk;
	public float atkDistance;
	public float speed;
	public bool isdead;
	public bool attacked;
	public GameObject target;
	public GameObject mainBaseTarget;
	public UnitType type;
	public string killerTag;

	// Private
	private AnimStateCro anim;
	private const int MAX_HP = 100;

	private void Awake()
	{
		anim = GetComponent<AnimStateCro>();
		baseInvad = GameObject.Find("GameSystem").GetComponent<BaseInvadCro>();
		isdead = false;
		attacked = false;
	}

	private void Update()
	{
		if (isdead == true)
		{
			return;
		}

		StatusUpdate();
	}

	public void GetDamage(int damage)
	{
		hp -= damage;
	}

	private void StatusUpdate()
	{
		if (hp <= 0)
		{
			if (type == UnitType.Base)
			{
				BaseInvaded();
			}
			else
			{
				StartCoroutine(IsDead());
			}
		}
	}

	public void BaseInvaded()
	{
		gameObject.tag = ObjNameManager.STATUS_DEAD_TAG;
		baseInvad.BaseDestroy(gameObject, killerTag);
	}

	private IEnumerator IsDead()
	{
		anim.SetDead();
		isdead = true;
		gameObject.tag = ObjNameManager.STATUS_DEAD_TAG;
		GetComponent<NavCro>().Destroy();
		yield return new WaitForSeconds(3);
		Destroy(gameObject);
	}

}