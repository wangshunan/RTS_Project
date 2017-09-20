using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootManController : MonoBehaviour
{ 
	int i = 0;
	float speed = 0.2f;
	float rotateSpeed = 2f;
    int hp = 10;

    static int stand = Animator.StringToHash("Stand");

    [SerializeField, HideInInspector]Animator animator;
    [SerializeField, HideInInspector]Animation animation;

    int attack = Animator.StringToHash("Base Layer.Attack1");
    int damage = Animator.StringToHash("Base Layer.Damage");

	void FixeUpdate() {
		this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
	}

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo anim = animator.GetCurrentAnimatorStateInfo(0);
        //Debug.Log(anim.normalizedTime);
        if(anim.fullPathHash == damage)
        {
            Debug.Log("Damage");
            if(anim.normalizedTime >= 0.5f)
            {
                Debug.Log("!!!");
            }
        }

		// keyboard controller
		if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            transform.Translate(transform.forward * speed, Space.World); //正面
        }

        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            transform.rotation = Quaternion.LookRotation(transform.position +
            (Vector3.right * Input.GetAxisRaw("Horizontal")) +
            (Vector3.forward * Input.GetAxisRaw("Vertical"))
            - transform.position);
        }

        if (hp == 0)
        {
            Destroy(gameObject);
        }
    }

    void Hit()
    {
    }

    public void GetHit()
    {
		i++;
        hp--;
		Debug.Log( i );
    }
}