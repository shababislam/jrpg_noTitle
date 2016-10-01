using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Town1Exit : MonoBehaviour {

	private bool entered;

	void Update(){
		if (entered && Input.GetKey(KeyCode.T)){
			GameMaster.BattleCounterReset();
			GameMaster.inTown = false;
			//GameMaster.mainCam.transform.position = GameMaster.currentPos;
			//GameMaster.overworldCam.enabled = true;

			SceneManager.LoadScene("terrain_test");
		}
	} 

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player"){
			entered = true;
		} 
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Player"){
			entered = false;
		} 
	}
}
