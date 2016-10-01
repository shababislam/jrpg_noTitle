using UnityEngine;
using System.Collections;
//[RequireComponent (typeof (CharacterController))]

public class PlayerControl : MonoBehaviour {
	private float rotSpeed = 700;
	private float walkSpeed = 5;
	private float runSpeed = 12;

	private CharacterController controller;
	private Quaternion targetRotation;
	private Animator animator;
	public Collider col;

	void Start () {
		controller = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
		forward.y = 0f;
		forward = forward.normalized;
		Vector3 right = new Vector3(forward.z,0.0f,-forward.x);
		Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));

		Vector3 walkDirection =  (input.x * right + input.z * forward);
		if(walkDirection.sqrMagnitude > 1f){
			walkDirection = walkDirection.normalized;
		}
		/*
		Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));

		if (!(input == Vector3.zero)){
			targetRotation = Quaternion.LookRotation(input);
			transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y,targetRotation.eulerAngles.y,rotSpeed * Time.deltaTime);
		}
			
		Vector3 motion = input;
		motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1)?0.7f:1;
		motion *= (Input.GetButton("Run"))?runSpeed:walkSpeed;

		motion+=Vector3.up*-8;
		animator.SetFloat("Speed",Mathf.Sqrt(motion.x * motion.x + motion.z * motion.z));
		controller.Move(motion * Time.deltaTime);
		*/
		if (!(input == Vector3.zero)){
			targetRotation = Quaternion.LookRotation(walkDirection);
			transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y,targetRotation.eulerAngles.y,rotSpeed * Time.deltaTime);
		}
		walkDirection *= (Input.GetButton("Run"))?runSpeed:walkSpeed;

		walkDirection+=Vector3.up*-8;
		animator.SetFloat("Speed",Mathf.Sqrt(walkDirection.x * walkDirection.x + walkDirection.z * walkDirection.z));

		controller.Move(walkDirection * Time.deltaTime);

		checkFront();

	}

	private void checkFront(){
		Vector3 fwd = transform.TransformDirection(Vector3.forward) * 3;
		RaycastHit hit;
		//Debug.DrawRay(transform.position + new Vector3(0,2.5f,0),fwd,Color.green);
		if (Physics.Raycast(transform.position + new Vector3(0,2.5f,0),fwd,out hit)&& hit.collider.transform.root.GetComponent<DialogueSet>() && Input.GetKeyUp(KeyCode.Space)){
			DialogueSet dLine = hit.collider.transform.root.GetComponent<DialogueSet>();
			if(!dLine.activeText){
				hit.collider.transform.root.LookAt(this.transform);
				dLine.ShowBox();
			}
		} 
	} 
	/*
	private void OnTriggerStay(Collider hit){
		col = hit;
		if (hit.gameObject.GetComponent<DialogueSet>() && Input.GetKeyUp(KeyCode.Space)){
			col = hit;
			DialogueSet dLine = hit.gameObject.GetComponent<DialogueSet>();
			if(!dLine.activeText){
				dLine.ShowBox();
			}
		} 
	}
	*/
}
