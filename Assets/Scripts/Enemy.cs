using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {
	private float targetDistance;
	public GameObject target; 
	private Animator stubbsAnim;
	public bool down;
	private bool running;
	private bool fainted;
	public float health;
	private Vector3 velocity;
	private float gravity = -9.81f;
	private GetHit getHit;
	public GameObject[] enemies;
	private float enemyCountdown;
	private float timeToAttack = 2;
	private float timeToWait = 0;
	private bool hit;
	private bool hitting;
	private bool attack;
	private bool attacking;
	private Player enemyPlayer;
	private Player thisPlayer;
	private EnemyHandCollider h_collider;
	public float Strength;

	void Start () {
		stubbsAnim = GetComponent<Animator>();
		FindTarget();
		fainted = false;
		getHit = GetComponent<GetHit>();
		h_collider = GetComponent<EnemyHandCollider>();
		thisPlayer = GetComponent<Player>();
		Strength = 5;
	}

	void Update () {
		hitting = h_collider.hit;

		if(enemies.Length < 2){
			FindTarget();
			target = enemies[Random.Range(0,2)];
		}
		if(!enemyPlayer){
			enemyPlayer = target.GetComponent<Player>();
		}
		bool gettingHit = getHit.hit;
		randomEnemy();
		isDown();
		isActive();
		targetDistance = Vector3.Distance(transform.position,target.transform.position);
		inScope();
		if (running)
			stubbsAnim.SetBool("Running",true);
		if (!running)
			stubbsAnim.SetBool("Running",false);
		if (!down){
			getPunched();
			getKicked();
		}
		if(stubbsAnim.GetCurrentAnimatorStateInfo(0).IsName("punching")){
			attacking = true;
		} 

		if(!stubbsAnim.GetCurrentAnimatorStateInfo(0).IsName("punching")){
			attacking = false;
		} 

		AttackCounter();

		Impact();

		if(attacking){
			h_collider.enabled = true;
		}
		if(!attacking){
			h_collider.enabled = false;
		}

		if(!fainted && attack && enemyPlayer.currentHealth > 0)
			Attack();

		thisPlayer.attacking = attacking;
		thisPlayer.currentTarget = target;
	}

	private void getPunched(){
		if(getHit.hit && enemyPlayer.attacking && enemyPlayer.currentTarget == this.gameObject){
			down = true;
			stubbsAnim.Play("head_hit",-1,0f);
			running = false;
			attacking = false;
		}

	}

	private void getKicked(){
		
	}

	private void isActive(){
		health = transform.GetComponent<Player>().CurrentHealth();

		if (health > 0){
			fainted = false;
			stubbsAnim.SetBool("Fainted",false);
		} 
		if (health < 0){
			fainted = true;
			stubbsAnim.SetBool("Fainted",true);
		}

	}

	private void inScope(){
		if (!fainted){
			if (!down && target && targetDistance <8 && targetDistance > 2.3f){
				transform.position = Vector3.MoveTowards(transform.position,target.transform.position,5*Time.deltaTime);
				transform.LookAt(target.transform);
				running = true;
			} else {
				running = false;
			}
		}
	}

	private void FindTarget(){
		enemies = GameObject.FindGameObjectsWithTag("Player");
		target = enemies[0];
	}
		
	private void isDown(){
		if (stubbsAnim.GetCurrentAnimatorStateInfo(0).IsName("getting_up")||stubbsAnim.GetCurrentAnimatorStateInfo(0).IsName("sweep_fall")||stubbsAnim.GetCurrentAnimatorStateInfo(0).IsName("head_hit")){
			down = true;
		} else {
			down = false;
		}
	}

	private void randomEnemy(){
		enemyCountdown--;
		if(enemyCountdown < 0){
			target = enemies[Random.Range(0,2)];
			enemyCountdown = 100;
		}
	}

	private void AttackCounterReset(){
		if(timeToWait < -0.6f)
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

	private void Attack(){
		if(targetDistance <= 3){
			transform.LookAt(target.transform);
			stubbsAnim.Play("punching",-1,0f);
			down = true;
		} else {
			down = false;
		}
	}

	private void Impact(){
		if(hitting && attacking){
			target.GetComponent<Player>().takeDamage(0.5f+Strength/100);
		}
	}
}
