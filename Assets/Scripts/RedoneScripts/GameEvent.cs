using UnityEngine;
using System.Collections;

public class GameEvent : MonoBehaviour {

	public int eventNumber;



	void Start () {
		eventNumber = int.Parse(gameObject.name.Substring(5));
	}

	void Update () {
	}

	public void StartEvent(){
		gameObject.SetActive(true);

	}

	public void EndEvent(){
		//Event[eventNumber] = true;
		gameObject.SetActive(false);
	}
}
