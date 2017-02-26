using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Interaction : MonoBehaviour {

	public bool hasQuest;
	public GameObject dBox;
	public GameObject selectionMarker;
	public Text dText;
	public bool activeText;
	public int index;
	public bool opening;
	public bool closing;
	public float speechBubbleTime = 20f;
	public Vector3 target;
	private Quaternion rot;
	private bool selected = false;
	public Text cont;

	public bool startQuest;
	public bool endQuest;
	public int QuestNumber;
	public bool requirePrevious;
	public int previousQuest;

	public ConvoNode defaultNode;
	public ConvoNode externalNode;
	ConvoNode activeNode;
	bool nodeEnd = false;
	public ConvoContainer convoArray;
	private bool questCheck = true;

	private bool convoCheck = false;

	private bool externalConvo = false;
	private bool timedLine = false;
	private float timer = 1f;

	private float test = 5f;

	void Start () {
		dBox.SetActive(false);
		activeText = false;
		index = 0;
		opening = false;
		closing = false;
		rot = transform.rotation;

		convoArray.updateList();

		activeNode = defaultNode;
		cont.text = "Press Space to continue.";

	}



	public void reset(){
		externalNode = null;
		activeNode = defaultNode;
		externalConvo = false;

	}

	void Update () {

		if(timedLine){
			timer-=0.01f;
			if(timer<=0){
				timer = 0;
				timedLine = false;
				extConvoClose();
			}
		}
		if(target != Vector3.zero){
			//Debug.Log(target + " | " + transform.position);
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
			
			//GameMaster.canMove = false;
			dBox.transform.localScale += new Vector3(0.5f,0.5f,0.5f) * speechBubbleTime *Time.deltaTime;
			if(dBox.transform.localScale.x > 1f){
				opening = false;
			}
		}

		if(closing){
			cont.text = "Press Space to continue.";

			dBox.transform.localScale -= new Vector3(0.5f,0.5f,0.5f) * speechBubbleTime *Time.deltaTime;
			if(dBox.transform.localScale.x < 0.1f){
				closing = false;
				dBox.SetActive(false);
				activeText = false;
				index = 0;
				GameMaster.canMove = true;
				GameMaster.talking = false;
				//reset();
			}
		}

		//take this out of update
		//cont.text = "Press Space to continue.";

		if(externalConvo){
			activeNode = convoArray.convos[0];
			convoArray.updateList();
			externalConvo = false;

		} 

		if(activeText){
			GameMaster.canMove = false;
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

	public void ExternalConvo(){
		externalConvo = true;
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

		if(nodeEnd && activeNode.hasA() && Input.GetKeyUp(KeyCode.K)){
			index=0;
			GameMaster.currentChoice = 1;
			activeNode = activeNode.getA();
			if(activeNode.hasNext()){
				activeNode = activeNode;
			}
		} 


		if(nodeEnd && activeNode.hasB() && Input.GetKeyUp(KeyCode.L)){
			index=0;
			GameMaster.currentChoice = 2;
			activeNode = activeNode.getB();
			if(activeNode.hasNext()){
				activeNode = activeNode;
			}
		} 
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
			GameMaster.questInProgress = false;
		}
	}


	public void conversationTest(ConvoNode node){

		if(activeText && Input.GetKeyUp(KeyCode.Space)){
			index++;
		}

		if(!node.hasNext() && activeText && Input.GetKeyUp(KeyCode.F)){
			GameMaster.currentChoice = 3;
			//GameMaster.canMove = true;
			closeConvo();
		}

		//dialogues.Length
		if(index >= node.getLines().Length){
			index = node.getLines().Length-1;
		}

		dText.text = node.getLines()[index];

	}

	public void extConvo(string line, float time){
		cont.text = "";

		dText.text = line;
		timedLine = true;
		timer = time;
		opening = true;
		dBox.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
		dBox.SetActive(true);

	}

	private void extConvoClose(){
		closing = true;
	}


	void closeConvo(){
		cont.text = "Press Space to continue.";
		closing = true;
		target = Vector3.zero;
		if(activeNode.returnToStart)
			activeNode = convoArray.convos[0];
	}

	public IEnumerator testFunc(){
		if(test!=0){
			test-=0.01f;
			if(test<=0){
				test = 0;
			}
		}
		yield return test;
	}

}
