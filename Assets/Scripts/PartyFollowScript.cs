using UnityEngine;
using System.Collections;

public class PartyFollowScript : MonoBehaviour {

	public GameObject target;

	private float distance;
	private float walkSpeed = 4f;
	private float runSpeed = 8f;
	private Animator anim;
	bool running;
	bool walking;
	private float rotationSpeed = 5f;

	void Start () {
		findPlayer();
		anim = GetComponent<Animator>();
		DontDestroyOnLoad(this);

	}
	
	void Update () {
		if(walking){
			anim.SetBool("Walking",true);
		} 
		if (!walking){
			anim.SetBool("Walking",false);
		} 

		if(running){
			anim.SetBool("Running",true);
		} 
		if (!running){
			anim.SetBool("Running",false);
		} 

		findPlayer();
		follow();

	}

	private void findPlayer(){
		if (!target)
			target = GameObject.FindGameObjectWithTag("Player");
	}


	private void follow(){
		distance = Vector3.Distance(transform.position,target.transform.position);
		if (distance < 5 && distance > 3){
			walking = true;
			running = false;
			transform.position = Vector3.MoveTowards(transform.position,target.transform.position, walkSpeed * Time.deltaTime);
			transform.LookAt(target.transform);
		}

		if (distance > 5){
			running = true;
			walking = false;
			transform.position = Vector3.MoveTowards(transform.position,target.transform.position, runSpeed * Time.deltaTime);
			transform.LookAt(target.transform);
		} 

		if(distance <= 3){
			running = false;
			walking = false;
		}
	}
}
