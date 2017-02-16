using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Interaction : MonoBehaviour {

	//public bool hasDialogue = false;
	//public string[] dialogues;
	public bool hasQuest;
	//public string[] questDialogues;
	public GameObject dBox;
	public GameObject selectionMarker;
	public Text dText;
	public bool activeText;
	public int index;
	public bool opening;
	public bool closing;
	public float speechBubbleTime = 20f;
	private Vector3 target;
	private Quaternion rot;
	private bool selected = false;
	public Text cont;

	public bool startQuest;
	public bool endQuest;
	public int QuestNumber;
	public bool requirePrevious;
	public int previousQuest;

	public ConvoNode defaultNode;
	ConvoNode activeNode;
	bool nodeEnd = false;
	public ConvoContainer convoArray;
	private bool questCheck = true;

	private bool convoCheck = false;

	void Start () {
		dBox.SetActive(false);
		activeText = false;
		index = 0;
		//dText.text = dialogues[index];
		opening = false;
		closing = false;
		rot = transform.rotation;

		convoArray.updateList();

		/*
		if(hasQuest){
			if(QuestNumber>0)
				previousQuest = QuestNumber-1;
			activeNode = convoArray.convos[0];

		} else {
			activeNode = defaultNode;
		} */ 

		/*
		if(startQuest && GameMaster.currentQuest==QuestNumber){

			activeNode = convoArray.convos[0];
		}
		else if(endQuest && GameMaster.questInProgress && GameMaster.currentQuest==QuestNumber){
			activeNode = convoArray.convos[0];
		} else {
			activeNode = defaultNode;

		} 
		*/
		//activeNode = defaultNode;


	}

	void OnEnable(){
		//does he have a quest?
		//is the quest eligible?
		//does he start or end the quest?

		//newnode
		if(hasQuest){
			if(QuestNumber == 0){
				Debug.Log(transform.gameObject.name + " 1");
				if(startQuest && !GameMaster.questInProgress){
					Debug.Log(transform.gameObject.name + " 2");

					Debug.Log(transform.gameObject.name + " active node");
					activeNode = convoArray.convos[0];

				}
				else if(endQuest&& GameMaster.questInProgress){
					Debug.Log(transform.gameObject.name + " 3");

					Debug.Log(transform.gameObject.name + " active node");
					activeNode = convoArray.convos[0];

				} else {
					Debug.Log(transform.gameObject.name + " 4");

					Debug.Log(transform.gameObject.name + " default node");
					activeNode = defaultNode;

				}
			} else {
				if(QuestManager.QM.questComplete[QuestNumber-1]){
					if(startQuest && !GameMaster.questInProgress){
						Debug.Log(transform.gameObject.name + " active node");
						activeNode = convoArray.convos[0];

					}
					if(endQuest&& GameMaster.questInProgress){
						Debug.Log(transform.gameObject.name + " active node");
						activeNode = convoArray.convos[0];

					}
				}
			}
		}
		//defaultnode
		else {
			Debug.Log(transform.gameObject.name + " default node");
			activeNode = defaultNode;
		}


		//Debug.Log(transform.gameObject.name + " is active now");


	}


	void Update () {
		

		if(target != Vector3.zero){
			Debug.Log(target + " | " + transform.position);
			Quaternion rotation1 = Quaternion.LookRotation(target-transform.position);
			this.transform.rotation = Quaternion.Slerp(transform.rotation, rotation1,Time.deltaTime*10);
			//this.transform.rotation = rotation1;
		} 

		if(selected){
			selectionMarker.SetActive(true);
		} 
		if(!selected){
			selectionMarker.SetActive(false);
		} 

			

		if(opening){
			dBox.transform.localScale += new Vector3(0.5f,0.5f,0.5f) * speechBubbleTime *Time.deltaTime;
			if(dBox.transform.localScale.x > 1f){
				opening = false;
			}
		}

		if(closing){
			dBox.transform.localScale -= new Vector3(0.5f,0.5f,0.5f) * speechBubbleTime *Time.deltaTime;
			if(dBox.transform.localScale.x < 0.1f){
				closing = false;
				dBox.SetActive(false);
				activeText = false;
				index = 0;
				GameMaster.talking = false;
			}
		}

		//conversationTest(a);
		cont.text = "Press Space to continue.";

		if(activeText){
			triggerBranch();
			conversationTest(activeNode);
		}
	}



	public void ShowBox(Vector3 targetPos){
		opening = true;
		target = targetPos;
		dBox.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
		activeText = true;
		dBox.SetActive(true);
	}



	public void isSelected(){
		selected = true;
	}
	public void unSelect(){
		selected = false;
	}

	void regularConvoBranch(){
		
		if(index+1 == activeNode.getLines().Length){
			nodeEnd = true;
			if(activeNode.hasNext()){
				cont.text = "Press K for Yes, L for No."; 
			} else {
				cont.text = "Press F to close.";
			}
		} else {
			nodeEnd = false;
		}

		if(nodeEnd && activeNode.hasA() && Input.GetKeyDown(KeyCode.K)){
			index=0;
			activeNode = activeNode.getA();
			if(activeNode.hasNext()){
				activeNode = activeNode;
			}
		} 

		if(nodeEnd && activeNode.hasB() && Input.GetKeyDown(KeyCode.L)){
			index=0;

			activeNode = activeNode.getB();
			if(activeNode.hasNext()){
				activeNode = activeNode;
			}
		} 
		QuestInteraction();

	}

	void triggerBranch(){
		Debug.Log(index + " | " + activeNode.getLines().Length);
		if(index+1 >= activeNode.getLines().Length){
			nodeEnd = true;
			if(activeNode.hasNext()){
				cont.text = "Press K for Yes, L for No."; 
			} else {
				cont.text = "Press F to close.";
			}
		} else {
			nodeEnd = false;
		}

		if(nodeEnd && activeNode.hasA() && Input.GetKeyDown(KeyCode.K)){
			index=0;
			activeNode = activeNode.getA();
			if(activeNode.hasNext()){
				activeNode = activeNode;
			}
		} 


		if(nodeEnd && activeNode.hasB() && Input.GetKeyDown(KeyCode.L)){
			index=0;

			activeNode = activeNode.getB();
			if(activeNode.hasNext()){
				activeNode = activeNode;
			}
		} 

		QuestInteraction();
	}




	void QuestInteraction(){
		
		if(QuestNumber==0){
			if(!QuestManager.QM.questComplete[QuestNumber]){
				if(activeNode.triggersQuest && startQuest && !QuestManager.QM.quests[QuestNumber].gameObject.activeSelf){
					QuestManager.QM.quests[QuestNumber].gameObject.SetActive(true);
					GameMaster.currentQuest = QuestNumber;
					GameMaster.questInProgress = true;
					hasQuest = false;
				}
			}
		}
		if(QuestNumber!=0){
			if(!QuestManager.QM.questComplete[QuestNumber] && QuestManager.QM.questComplete[previousQuest]){
				if(activeNode.triggersQuest && activeNode.triggersQuest && startQuest && !QuestManager.QM.quests[QuestNumber].gameObject.activeSelf){
					QuestManager.QM.quests[QuestNumber].gameObject.SetActive(true);
					GameMaster.currentQuest = QuestNumber;
					GameMaster.questInProgress = true;
					hasQuest = false;

				}
			}
		}

		if(endQuest && QuestManager.QM.quests[QuestNumber].gameObject.activeSelf){

			QuestManager.QM.quests[QuestNumber].EndQuest();
			hasQuest = false;
			Debug.Log(this.transform.gameObject.name + " is ending quest");
			GameMaster.questInProgress = false;
		}
	}

/*
	void conversation(string[] lines){

		if(activeText && Input.GetKeyDown(KeyCode.Space)){
			index++;
		}

		if(activeText && Input.GetKeyDown(KeyCode.F)){
			closeConvo();
		}




		//dialogues.Length
		if(index >= currentLinesLength){
			index = 0;
		}


		dText.text = lines[index];

	}
*/
	public void conversationTest(ConvoNode node){
		int currentLength = node.getLines().Length;
		if(activeText && Input.GetKeyDown(KeyCode.Space)){
			index++;
		}

		if(!node.hasNext() && activeText && Input.GetKeyDown(KeyCode.F)){
			closeConvo();
		}

		//dialogues.Length
		if(index >= node.getLines().Length){
			index = node.getLines().Length-1;
		}

		dText.text = node.getLines()[index];

	}


	void closeConvo(){
		closing = true;
		target = Vector3.zero;
		if(activeNode.returnToStart)
			activeNode = convoArray.convos[0];
	}

	public void newConvo(ConvoContainer container){
		activeNode = container.convos[0];
	}

	public void newConvo(ConvoNode node){
		activeNode = node;
	}



}
