using UnityEngine;
using System.Collections;

public class NPC_NoMovement : MonoBehaviour {

	public DialogueSet dSet;
	public bool talking;
	public Animator anim;

	void Start () {
		dSet = GetComponent<DialogueSet>();
		anim = GetComponent<Animator>();
	}
	
	void Update () {
		if(dSet.dBox.activeSelf){
			talking = true;
		}

		if(!dSet.dBox.activeSelf){
			talking = false;
		}

		if(talking){
			anim.SetBool("Talking",true);
		} 
		if(!talking){
			anim.SetBool("Talking",false);
		}
	}
}
