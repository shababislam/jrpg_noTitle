using UnityEngine;
using System.Collections;
//[RequireComponent (typeof (CharacterController))]

public class PlayerControl : MonoBehaviour {
	private float rotSpeed = 700;
	public float walkSpeed = 1;
	public float runSpeed = 3;

	private CharacterController controller;
	private Quaternion targetRotation;
	private Animator animator;
	public Collider col;

	public float accel = 0.5f;
	public float deccel = 0.5f;
	bool sprinting = false;

	float newRun = 0f;
	float counter = 0;

	//public bool canMove = true;
	//bool talking = false;

	public Transform target;

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
			Vector3 input = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));

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



			if(Input.GetButton("Run") && !(input == Vector3.zero)){
				newRun+=accel;
				if(newRun >=runSpeed){
					newRun = runSpeed;
					sprinting = true;
				}
			} else {
				newRun-=deccel;
				if(newRun<=0){
					newRun = 0;
				}
			}



			//Debug.Log(input.x + ", "+ input.z);

			//walkDirection *= (Input.GetButton("Run"))?newRun:Mathf.Max(walkSpeed,newRun);

			walkDirection*=Mathf.Max(walkSpeed,newRun);

			walkDirection+=Vector3.up*-8;
			float sp = Mathf.Sqrt(walkDirection.x * walkDirection.x + walkDirection.z * walkDirection.z);
			animator.SetFloat("Speed",sp);


		if(GameMaster.canMove){

			if (!(input == Vector3.zero)){
				targetRotation = Quaternion.LookRotation(walkDirection);
				transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y,targetRotation.eulerAngles.y,rotSpeed * Time.deltaTime);
			}
			controller.Move(walkDirection * Time.deltaTime);

		} 
		if(GameMaster.talking){
			GameMaster.canMove = false;
			animator.SetFloat("Speed", 0);
			Vector3 noMove = new Vector3(0,-8,0);
			controller.Move(noMove * Time.deltaTime);

		}
		if(!GameMaster.canMove && !GameMaster.talking){
			animator.SetFloat("Speed", 0);
			Vector3 noMove = new Vector3(0,-8,0);
			controller.Move(noMove * Time.deltaTime);
			counter+=0.01f;
			if(counter>=1){
				GameMaster.canMove = true;
				counter = 0;
			}

		}
		checkFront();

	}

	private void checkFront(){
		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		RaycastHit hit;
		Debug.DrawRay(transform.position,fwd,Color.green);
		if (Physics.SphereCast(transform.position,3f,fwd,out hit,6) && hit.transform.GetComponent<Interaction>()){
			//hit.transform.GetComponent<DialogueSet>().isSelected();
			if(target==null)
				target = hit.transform;

			Interaction dLine = target.GetComponent<Interaction>();

			dLine.isSelected();

			if(Input.GetKeyUp(KeyCode.Space)){
				if(!dLine.activeText){
					GameMaster.talking = true;
					dLine.ShowBox(transform.position);
				}
			}
		
		

		} else {
			if(target){
				target.GetComponent<Interaction>().unSelect();
			}
			target = null;
		}

		/*
		if (Physics.SphereCast(transform.position,3f,fwd,out hit,6)&& hit.transform.GetComponent<DialogueSet>() && Input.GetKeyUp(KeyCode.Space)){
			DialogueSet dLine = hit.transform.GetComponent<DialogueSet>();
			if(!dLine.activeText){
				talking = true;
				Vector3 targetDir = this.transform.position - hit.transform.position;
				Quaternion rotation1 = Quaternion.LookRotation(-this.transform.forward);
				dLine.ShowBox(this.transform.position);

			}
		} */

	} 



	public void flipSwitch(){
		GameMaster.canMove = false;
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
