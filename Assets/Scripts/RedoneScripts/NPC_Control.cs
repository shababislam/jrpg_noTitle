using UnityEngine;
using System.Collections;

public class NPC_Control : MonoBehaviour {

	public GameObject dBox;
	public bool talking;
	public Animator anim;
	Quaternion rotation;

	Vector3 targetPos;
	bool movingTowards = false;
	public float moveSpeed = 20f;

	public Transform TestObject;
	private CharacterController controller;
	Interaction interaction;
	private Vector3 initialPos;

	private bool resetBool = false;

	//public ConvoNode convo;
	//public ConvoContainer convoArray;


	void Start () {
		//dSet = GetComponent<DialogueSet>();
		controller = GetComponent<CharacterController>();
		anim = GetComponent<Animator>();
		interaction = GetComponent<Interaction>();

		rotation = transform.rotation;
		initialPos = transform.position;
	}

	void Update () {
		//transform.position.y = currentPos.y;

		if(dBox.activeSelf){
			talking = true;
			anim.SetBool("Talking",true);
		} else {
			talking = false;
			anim.SetBool("Talking",false);
			resetRotation();
		}
		/*
		if(!dBox.activeSelf){
			talking = false;
			anim.SetBool("Talking",false);
			resetRotation();
		}*/
			
		if(movingTowards && targetPos!=Vector3.zero){

			move();
		}

		if(resetBool && !talking){
			resetBool = false;
			GameMaster.currentChoice = 0;
			interaction.reset();
		}

	}

	void move(){

		if(targetPos!=Vector3.zero){
			//Quaternion rotation1 = Quaternion.LookRotation(targetPos-transform.position);
			//rotation1.y = 0;
			//this.transform.rotation = Quaternion.Slerp(transform.rotation, rotation1,Time.deltaTime*10);
			if((targetPos-transform.position).magnitude > 6){

				Vector3 horizontalVelocity = controller.velocity;
				horizontalVelocity = new Vector3(controller.velocity.x, 0, controller.velocity.z);

				Vector3 dest = targetPos-initialPos;
				dest+=new Vector3(0,-8,0);
				controller.Move(dest*Time.deltaTime);
				Vector3 dir = targetPos-transform.position;
				dir.y = 0f;
				rotation = Quaternion.LookRotation(dir);
			} else {
				targetPos = new Vector3(0,0,0);
				movingTowards = false;

			}

		} else {
			targetPos = new Vector3(0,0,0);
			movingTowards = false;
		}
		//transform.position = Vector3.MoveTowards(transform.position,targetPos, moveSpeed * Time.deltaTime);

	}

	private void resetRotation(){
		this.transform.rotation = Quaternion.Slerp(transform.rotation, rotation,Time.deltaTime*20);
	}

	public void moveTo(Vector3 targetArea){
		targetPos = targetArea;
		movingTowards = true;
	}

	public void say(ConvoNode a){
		interaction.externalNode = a;
		//interaction.target = targetPos;
		interaction.ExternalConvo();
		//interaction.conversationTest(a);
		interaction.ShowBox(targetPos);
	}

	public void say(ConvoContainer a){
		interaction.convoArray = a;
		interaction.ExternalConvo();
	}

	public void say(string line, float time){
		interaction.extConvo(line, time);
	}

	public void resetConvo(){
		resetBool = true;
	}
}
