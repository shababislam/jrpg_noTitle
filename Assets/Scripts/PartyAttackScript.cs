using UnityEngine;
using System.Collections;

public class PartyAttackScript : MonoBehaviour {

	public GameObject target;

	private float distance;
	private float speed = 8f;
	private Animator anim;
	bool running;
	private float rotationSpeed = 5f;
	private float timeToAttack = 2;
	private float timeToWait = 0;
	private bool attack;
	private bool attacking;
	private bool canRun = true;
	private HandCollider h_collider;
	private bool hit;
	private Player enemyPlayer;
	private GetHit getHit;
	private Player thisPlayer;

	void Start () {
		findPlayer();
		anim = GetComponent<Animator>();
		DontDestroyOnLoad(this);
		h_collider = GetComponent<HandCollider>();
		getHit = GetComponent<GetHit>();
		thisPlayer = GetComponent<Player>();
	}

	void Update () {
		hit = h_collider.hit;
		if(!target)
			findPlayer();
		if(!enemyPlayer){
			enemyPlayer = target.GetComponent<Player>();
		}
		distance = Vector3.Distance(transform.position,target.transform.position);

		if(running){
			anim.SetBool("Running",true);
		} 
		if (!running){
			anim.SetBool("Running",false);
		}
		findPlayer();
		follow();
		AttackCounter();

		Impact();

		if(anim.GetCurrentAnimatorStateInfo(0).IsName("punching")){
			attacking = true;
			canRun = false;
		} 

		if(!anim.GetCurrentAnimatorStateInfo(0).IsName("punching")){
			attacking = false;
			canRun = true;
		} 

		if(!anim.GetCurrentAnimatorStateInfo(0).IsName("getting_hit")){
			canRun = false;
		} 

		if(!anim.GetCurrentAnimatorStateInfo(0).IsName("getting_hit")){
			canRun = true;
		} 
		getPunched();

		if(attack && enemyPlayer.currentHealth > 0)
			Attack();

		thisPlayer.attacking = attacking;
		thisPlayer.currentTarget = target;

	}

	private void getPunched(){
		if(getHit.hit && enemyPlayer.attacking && enemyPlayer.currentTarget == this.gameObject){
			canRun = false;
			anim.Play("getting_hit",-1,0f);
			running = false;
			attacking = false;
		} else {
			canRun = true;
		}

	}
	private void AttackCounterReset(){
		if(timeToWait < -1)
			timeToWait = timeToAttack;
	}

	private void AttackCounter(){
		timeToWait-=Time.deltaTime;
		if(timeToWait < 0){
			attack = true;
		}
		if(timeToWait > 0){
			attack = false;
		}
		if(timeToWait < -0.1f){
			attack = false;
			AttackCounterReset();
		}
	}

	private void findPlayer(){
		if (!target)
			target = GameObject.FindGameObjectWithTag("Target");
	}


	private void follow(){
		if (canRun){
			if (distance > 3f){
				transform.position = Vector3.MoveTowards(transform.position,target.transform.position, speed * Time.deltaTime);
				transform.LookAt(target.transform);
				running = true;
			} 
		}
	}

	private void Attack(){
		if(distance <= 3){
			transform.LookAt(target.transform);
			anim.Play("punching",-1,0f);
			canRun = false;
			attacking = true;
		} else {
			canRun = true;
			attacking = false;
		}
	}

	private void Impact(){
		if(hit && attacking){
			target.GetComponent<Player>().takeDamage(0.9f);
		}
	}
}
