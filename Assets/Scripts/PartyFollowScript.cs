using UnityEngine;
using System.Collections;

public class PartyFollowScript : MonoBehaviour {

	public GameObject target;

	private float distance;
	public float walkSpeed = 4f;
	public float runSpeed = 8f;
	private Animator anim;
	bool running;
	bool walking;
	private float rotationSpeed = 5f;

	void Start () {
		findPlayer();
		anim = GetComponent<Animator>();
		//DontDestroyOnLoad(this);

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
		if (distance < 10 && distance > 6){
			walking = true;
			running = false;
			transform.position = Vector3.MoveTowards(transform.position,target.transform.position, walkSpeed * Time.deltaTime);
			transform.LookAt(target.transform);
		}

		if (distance > 10){
			running = true;
			walking = false;
			transform.position = Vector3.MoveTowards(transform.position,target.transform.position, runSpeed * Time.deltaTime);
			transform.LookAt(target.transform);
		} 

		if(distance <= 6){
			running = false;
			walking = false;
		}
	}
}
