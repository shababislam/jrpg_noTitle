using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Town1 : MonoBehaviour {

	private bool entered;
	public string placeName;

	void Update(){
		if (entered && Input.GetKey(KeyCode.T)){
			SceneManager.LoadScene(placeName);
		}
	} 

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player"){
			GameMaster.currentPos = other.GetComponent<DamienOverworld>().lastPos();
			entered = true;
		} 
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Player"){
			entered = false;
		} 
	}
}
