using UnityEngine;
using System.Collections;

public class Quest : MonoBehaviour {

	public int questNumber;

	public bool isItemQuest;
	public string[] tempDialogues;
	public string[] newDialogues;
	public DialogueSet tempDialogue;


	void Start () {
		questNumber = int.Parse(gameObject.name.Substring(5));
	}
	
	void Update () {
	
	}

	public void StartQuest(){
		gameObject.SetActive(true);
	}

	public void EndQuest(){
		QuestManager.QM.questComplete[questNumber] = true;
		gameObject.SetActive(false);
	}
}
