using UnityEngine;
using System.Collections;

public class QuestManager : MonoBehaviour {

	public Quest[] quests;
	public bool[] questComplete;
	public static QuestManager QM;

	public string itemCollected;

	void Start () {
		if(QM!=null)
			GameObject.Destroy(QM);
		else
			QM = this;
		
		questComplete = new bool[quests.Length];
		Debug.Log("I'm alive");
	}


}
