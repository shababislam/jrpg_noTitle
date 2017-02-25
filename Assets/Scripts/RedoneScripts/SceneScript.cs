using UnityEngine;
using System.Collections;

public class SceneScript : MonoBehaviour {

	// Use this for initialization

	public Transform[] scenePeople;

	void Start () {
		if(scenePeople!=null)
			populateScene(scenePeople);
	}
	
	void onEnable(){
		if(scenePeople!=null)
			populateScene(scenePeople);
		
	}

	public void populateScene(Transform[] people){
		if(people.Length>0){
			Debug.Log(people.Length);
		} else {
			Debug.Log("Empty AF");
		}
	}
}
