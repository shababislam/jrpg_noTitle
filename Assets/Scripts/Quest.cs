using UnityEngine;
using System.Collections;

public class Quest : MonoBehaviour {

	public int questNumber;

	public bool isItemQuest;


	void Start () {
		questNumber = int.Parse(gameObject.name.Substring(5));
	}
	
	void Update () {
	}

	public void StartQuest(){
		gameObject.SetActive(true);
		GameMaster.questInProgress = true;

	}

	public void EndQuest(){
		QuestManager.QM.questComplete[questNumber] = true;
		GameMaster.questInProgress = false;
		//EventManager.EM.EnableEvent(questNumber);
		Debug.Log("Ending quest: "+questNumber);
		gameObject.SetActive(false);
	}
}
