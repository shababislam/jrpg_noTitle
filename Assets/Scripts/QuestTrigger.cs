using UnityEngine;
using System.Collections;

public class QuestTrigger : MonoBehaviour {

	public bool startQuest;
	public bool endQuest;
	public int QuestNumber;
	public string[] newDialogue;
	public string[] tempDialogue;
	public bool requirePrevious;
	public int previousQuest;

	void Start () {
		previousQuest = QuestNumber-1;
	}
	
	void Update () {

	}

	void OnTriggerStay(Collider other){
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
}
