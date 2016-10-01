using UnityEngine;
using System.Collections;

public class NPCQuest : MonoBehaviour {

	public int QuestNumber;
	public bool startQuest;
	public bool endQuest;

	void Start () {
	
	}
	
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){

		if(other.tag == "Player" && !QuestManager.QM.questComplete[QuestNumber]){
			if(startQuest && !QuestManager.QM.quests[QuestNumber].gameObject.activeSelf){
				QuestManager.QM.quests[QuestNumber].gameObject.SetActive(true);
			}
		}

		if(endQuest && QuestManager.QM.quests[QuestNumber].gameObject.gameObject.activeSelf){
			QuestManager.QM.quests[QuestNumber].EndQuest();
		}

	}
}
