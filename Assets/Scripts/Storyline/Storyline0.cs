using UnityEngine;
using System.Collections;

public class Storyline0 : MonoBehaviour {

	public Transform Isaiah;
	private NPC_Control IsaiahControl;

	public Transform Viola;
	private NPC_Control ViolaControl;

	public ConvoContainer IsaiahConvo;
	public ConvoContainer ViolaConvo;
	private int currentState;
	private int currentChoice;
	public GameObject trigger0;
	public GameObject trigger1;

	public GameObject areaBlock;

	// Use this for initialization
	void Start () {
		if(!Isaiah){
			Isaiah = GameObject.Find("isaiah").transform;
		}
		if(!Viola){
			Viola = GameObject.Find("viola").transform;
		}


		IsaiahControl = Isaiah.GetComponent<NPC_Control>();
		ViolaControl = Viola.GetComponent<NPC_Control>();
		currentState = 0;
	}
	
	void Update () {



		if(GameMaster.state==0 && GameMaster.currentChoice == 0){
			IsaiahControl.say(IsaiahConvo);

		} 

		if(GameMaster.state == 0 && GameMaster.currentChoice == 1){
			GameMaster.state=1;
			//GameMaster.currentChoice = 0;
			IsaiahControl.resetConvo();
		}

		if(GameMaster.state == 1 && GameMaster.currentChoice == 0){
			GameMaster.state = 2;

			if(!trigger0.gameObject.activeSelf)
				trigger0.SetActive(true);


			if(areaBlock.gameObject.activeSelf)
				areaBlock.SetActive(false);

		}
	
		if(GameMaster.state == 2 && GameMaster.currentChoice == 0){
			if(Viola.gameObject.activeInHierarchy){
				ViolaControl.say(ViolaConvo);
			} 
			//Debug.Log("erm");

		}

		if(GameMaster.state == 2 && GameMaster.currentChoice == 3){
			trigger1.SetActive(true);
			ViolaControl.resetConvo();
			GameMaster.state = 3;
		}

		if(GameMaster.state == 3 && GameMaster.currentChoice == 3){
			Debug.Log("This is where are right now");
			GameMaster.state = 4;
		}


		//add a choice for F (choice = 3)
		//Debug.Log("We are at state: "+GameMaster.state + ". Our choice is: "+GameMaster.currentChoice);


	}

	void UpdateState(){
		currentChoice = GameMaster.currentChoice;
		currentState = GameMaster.state;
	}




}
