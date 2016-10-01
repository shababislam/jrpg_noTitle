using UnityEngine;
using System.Collections;
[RequireComponent (typeof (CharacterController))]

public class CombatMovement : MonoBehaviour {
	private float rotSpeed = 300;
	private float walkSpeed = 3;
	private float runSpeed = 8;
	private float targetDistance;
	private bool hit = false;

	private CharacterController controller;
	private Quaternion targetRotation;
	private Animator animator;
	private Vector3 moveDirection = Vector3.zero;
	public GameObject target; 

	void Start () {
		controller = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
		target = GameObject.FindGameObjectWithTag("Target");

	}

	// Update is called once per frame
	void Update () {
		targetDistance = Vector3.Distance(transform.position,target.transform.position);
		Debug.Log(targetDistance);

		Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));
		Vector3 motion = input;
		motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1)?0.7f:1;
		motion *= runSpeed;

		motion+=Vector3.up*-1;
		animator.SetFloat("Speed",Mathf.Sqrt(motion.x * motion.x + motion.z * motion.z));


		if(Input.GetKey(KeyCode.W)) {
			transform.position += transform.forward * Time.deltaTime * runSpeed;
		}
		if(Input.GetKey(KeyCode.S)) {
			transform.position -= transform.forward * Time.deltaTime * runSpeed;
		}
		if(Input.GetKey(KeyCode.A)) {
			transform.Rotate(-Vector3.up * rotSpeed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.D)) {
			transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.Q)){
			animator.Play("punching",-1,0f);
			if (targetDistance < 2){
				hit = true;
			} else {
				hit = false;
			}
		}
		if (Input.GetKey(KeyCode.E)){
			animator.Play("roundhouse_kick",-1,0f);
			if (targetDistance < 2){
				hit = true;
			} else {
				hit = false;
			}
		}


	}
	
}
