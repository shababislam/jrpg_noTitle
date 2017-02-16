using UnityEngine;
using System.Collections;

public class QuestTrigger_New : MonoBehaviour {

	public bool startQuest;
	public bool endQuest;
	public int QuestNumber;
	public string[] newDialogue;
	public string[] tempDialogue;
	public bool requirePrevious;
	public int previousQuest;

	public ConvoNode test;

	public GameObject questGiver;

	void Start () {
		previousQuest = QuestNumber-1;

	}

	void Update () {

	}

	void QuestInteraction(Collider other){
		if(QuestNumber==0){
			if(!QuestManager.QM.questComplete[QuestNumber]){
				if(startQuest && !QuestManager.QM.quests[QuestNumber].gameObject.activeSelf){
					QuestManager.QM.quests[QuestNumber].gameObject.SetActive(true);
				}
			}
		}
		if(QuestNumber!=0){
			if(!QuestManager.QM.questComplete[QuestNumber] && QuestManager.QM.questComplete[previousQuest]){
				if(startQuest && !QuestManager.QM.quests[QuestNumber].gameObject.activeSelf){
					QuestManager.QM.quests[QuestNumber].gameObject.SetActive(true);
				}
			}
		}

		if(endQuest && QuestManager.QM.quests[QuestNumber].gameObject.gameObject.activeSelf){
			QuestManager.QM.quests[QuestNumber].EndQuest();
		}

	}
}
