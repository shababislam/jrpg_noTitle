using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {

	public static EventManager EM;
	public Event[] events;
	//public int[] eventNumbers;

	// Use this for initialization
	void Start () {
		if(EM!=null)
			GameObject.Destroy(EM);
		else
			EM = this;

		//eventNumbers = new int[eventNumbers.Length];

	}

	/*
	 	Runtime: instead of update method, call an updateEvent function from Quest class?
	 	
		Function template? - 

			func(Quest a) - if corresponding trigger exists, enable collider
		
			description:
				if the requirements are made, certain events can occur.
				example: quest 1 is complete, now trigger colliders are enabled 

				future - make chain triggers
	*/

	public void EnableEvent(int questNum){
		//deactivate previous events?


		if(events[questNum]){
			Debug.Log("Quest number "+questNum+ " just ended. Events are now triggered.");
			events[questNum].gameObject.SetActive(true);
		}
	}

}
