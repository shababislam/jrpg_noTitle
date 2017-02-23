using UnityEngine;
using System.Collections;

public class QuestTrigger : MonoBehaviour {

	public bool startQuest;
	public bool endQuest;
	public int QuestNumber;
	public bool requirePrevious;
	public int previousQuest;

	void Start () {
		if(QuestNumber!=0)
			previousQuest = QuestNumber-1;
	}


	void OnTriggerEnter(Collider other){
		if(QuestNumber==0){
			if(other.tag == "Player" && !QuestManager.QM.questComplete[QuestNumber]){
				if(startQuest && !QuestManager.QM.quests[QuestNumber].gameObject.activeSelf){
					QuestManager.QM.quests[QuestNumber].gameObject.SetActive(true);
					this.gameObject.SetActive(false);
				}
			}
		}
		if(QuestNumber!=0){
			if(other.tag == "Player" && !QuestManager.QM.questComplete[QuestNumber] && QuestManager.QM.questComplete[previousQuest]){
				if(startQuest && !QuestManager.QM.quests[QuestNumber].gameObject.activeSelf){
					QuestManager.QM.quests[QuestNumber].gameObject.SetActive(true);
					this.gameObject.SetActive(false);

				}
			}
		}

		if(endQuest && QuestManager.QM.quests[QuestNumber].gameObject.gameObject.activeSelf){
			//Debug.Log(endQuest);
			QuestManager.QM.quests[QuestNumber].EndQuest();
			this.gameObject.SetActive(false);

		}
			
	}

	/*
	void QuestInteraction(Collider other){
		if(QuestNumber==0){
			if(Input.GetKeyUp(KeyCode.Space) && other.tag == "Player" && !QuestManager.QM.questComplete[QuestNumber]){
				if(startQuest && !QuestManager.QM.quests[QuestNumber].gameObject.activeSelf){
					if(gameObject.GetComponent<DialogueSet>() && newDialogue.Length != 0){
						gameObject.GetComponent<DialogueSet>().dialogues = newDialogue;
					}
					QuestManager.QM.quests[QuestNumber].gameObject.SetActive(true);
				}
			}
		}
		if(QuestNumber!=0){
			if(Input.GetKeyUp(KeyCode.Space) && other.tag == "Player" && !QuestManager.QM.questComplete[QuestNumber] && QuestManager.QM.questComplete[previousQuest]){
				if(startQuest && !QuestManager.QM.quests[QuestNumber].gameObject.activeSelf){
					if(gameObject.GetComponent<DialogueSet>() && newDialogue.Length != 0){
						gameObject.GetComponent<DialogueSet>().dialogues = newDialogue;
					}
					QuestManager.QM.quests[QuestNumber].gameObject.SetActive(true);
				}
			}
		}
			
		if(Input.GetKeyUp(KeyCode.Space) && endQuest && QuestManager.QM.quests[QuestNumber].gameObject.gameObject.activeSelf){
			if(gameObject.GetComponent<DialogueSet>() && newDialogue.Length != 0){
				gameObject.GetComponent<DialogueSet>().dialogues = newDialogue;
			}

			QuestManager.QM.quests[QuestNumber].EndQuest();
		}

	}
	*/
}
