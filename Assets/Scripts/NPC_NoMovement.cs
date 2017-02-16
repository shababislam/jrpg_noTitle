using UnityEngine;
using System.Collections;

public class NPC_NoMovement : MonoBehaviour {

	public GameObject dBox;
	public bool talking;
	public Animator anim;
	Quaternion rotation;

	void Start () {
		//dSet = GetComponent<DialogueSet>();

		anim = GetComponent<Animator>();
		rotation = transform.rotation;
	}
	
	void Update () {
		if(dBox.activeSelf){
			talking = true;
		}

		if(dBox.activeSelf){
			talking = false;
		}

		if(talking){
			anim.SetBool("Talking",true);
		} 
		if(!talking){
			this.transform.rotation = Quaternion.Slerp(transform.rotation, rotation,Time.deltaTime*20);

			anim.SetBool("Talking",false);
		}
	}
}
