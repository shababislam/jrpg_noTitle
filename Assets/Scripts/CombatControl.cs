using UnityEngine;
using System.Collections;

public class CombatControl : MonoBehaviour {

	private Animator animator;
	public GameObject target;
	private float targetDistance;
	public bool attacking;
	private CharacterController controller;
	private Quaternion targetRotation;
	private float rotSpeed = 700;
	private float walkSpeed = 5;
	private float runSpeed = 10;
	private bool running;
	//private HandCollider h_collider;
	private Player enemyPlayer;
	private Player thisPlayer;
	public bool hit;
	private GetHit getHit;
	bool canMove;
	RaycastHit hitObject;
	Vector3 fwd;
	int layer_mask;
	void Start () {
		controller = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
		FindTarget();
		thisPlayer = GetComponent<Player>();
		getHit = GetComponent<GetHit>();
		layer_mask = LayerMask.GetMask("Enemy");
	}
	
	void Update () {
		fwd = transform.TransformDirection(Vector3.forward) * 3;

		if (Physics.Raycast(transform.position + new Vector3(0,3,0),fwd,out hitObject,layer_mask) && hitObject.transform.root.tag == "Target"){
			hit = true;
		}
		if (!(Physics.Raycast(transform.position + new Vector3(0,3,0),fwd,out hitObject,layer_mask) && hitObject.transform.root.tag == "Target")){
			hit = false;
		}

		if(!target){
			FindTarget();
		}
		if(!enemyPlayer){
			enemyPlayer = target.GetComponent<Player>();
		}
		Punch();
		if(canMove && !attacking)
			Move();

		Impact();
		getPunched();

		if(animator.GetCurrentAnimatorStateInfo(0).IsName("punching")){
			attacking = true;
		} 

		if(!animator.GetCurrentAnimatorStateInfo(0).IsName("punching")){
			attacking = false;
		} 

		if(animator.GetCurrentAnimatorStateInfo(0).IsName("head_hit")){
			canMove = false;
		} 

		if(!animator.GetCurrentAnimatorStateInfo(0).IsName("head_hit")){
			canMove = true;
		} 
		thisPlayer.attacking = attacking;
		thisPlayer.currentTarget = target;

	}

	void Move(){
		Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));

		if (!(input == Vector3.zero)){
			targetRotation = Quaternion.LookRotation(input);
			transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y,targetRotation.eulerAngles.y,rotSpeed * Time.deltaTime);
		}

		Vector3 motion = input;
		motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1)?0.7f:1;
		motion *= runSpeed;

		motion+=Vector3.up*-8;
		animator.SetFloat("Speed",Mathf.Sqrt(motion.x * motion.x + motion.z * motion.z));
		controller.Move(motion * Time.deltaTime);
	}

	private void FindTarget(){
		target = GameObject.FindGameObjectWithTag("Target");

	}

	private void Punch(){
		if (Input.GetKeyUp(KeyCode.E)){
			if(!attacking){
				if (target){
					transform.LookAt(target.transform);
				} 
				animator.Play("punching",-1,0f);
			}
		} 
	}

	private void getPunched(){
		if(getHit.hit && enemyPlayer.attacking && enemyPlayer.currentTarget == this.gameObject){
			animator.Play("head_hit",-1,0f);
			running = false;
			attacking = false;
		}
	}


	private void Impact(){
		if(hit && attacking){
			enemyPlayer.takeDamage(0.50f);
		}
	}
}
