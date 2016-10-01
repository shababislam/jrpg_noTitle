using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlaceEnter : MonoBehaviour {

	private bool entered;
	public string placeName;

	void Update(){
		if (entered && Input.GetKey(KeyCode.T)){
			SceneManager.LoadScene(placeName);
		}
	} 

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player"){
			if(!GameMaster.inTown){
				GameMaster.currentPos = other.GetComponent<DamienOverworld>().lastPos();
			}
			if(GameMaster.inTown){
				GameMaster.lastTownPos = this.transform.position;
			}
			entered = true;
		} 
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Player"){
			entered = false;
		} 
	}
}
