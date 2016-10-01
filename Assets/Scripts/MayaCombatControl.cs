using UnityEngine;
using System.Collections;

public class MayaCombatControl : MonoBehaviour {

	private Animator animator;
	public GameObject target;
	private float targetDistance;
	private bool impacted;
	private bool attacking;

	void Start () {
		animator = GetComponent<Animator>();
		if(!target) FindTarget();
	}
	
	// Update is called once per frame
	void Update () {
		if(target)
		targetDistance = Vector3.Distance(transform.position,target.transform.position);
		impact();

		punch();
		kick();

	}

	private void FindTarget(){
		if (!target)
			target = GameObject.FindGameObjectWithTag("Target");
	}

	private void impact(){
		AnimatorStateInfo currentAnimation = animator.GetCurrentAnimatorStateInfo(0);
		if (targetDistance < 2){
			if (!impacted && target!=null){
				if (animator.GetCurrentAnimatorStateInfo(0).IsName("punching")){
					if(currentAnimation.normalizedTime%1 > 0.12f){
						target.GetComponent<Player>().takeDamage(5);
						impacted = true;
					}
				}
				if (animator.GetCurrentAnimatorStateInfo(0).IsName("roundhouse_kick")){
					if(currentAnimation.normalizedTime%1 > 0.28f){
						target.GetComponent<Player>().takeDamage(10);
						impacted = true;
					}
				}
			}
		}
	}
		
	private bool punch(){
		if (Input.GetKey(KeyCode.Q)){
			transform.LookAt(target.transform);
			animator.Play("punching",-1,0f);
			impacted = false;
			attacking = true;
		} else {
			attacking = false;
		}
		return true;
	}

	private bool kick(){
		if (Input.GetKey(KeyCode.E)){
			transform.LookAt(target.transform);
			animator.Play("roundhouse_kick",-1,0f);
			impacted = false;
			attacking = true;
		} else {
			attacking = false;
		}
		return true;
	}
}
