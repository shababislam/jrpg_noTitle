using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MaggiesTavern : MonoBehaviour {

	public GameObject entrance;
	public GameObject gm;
	public float vx, vy, vz, ex, ey, ez;
	public string townName;
	private bool entered;
	public string PlaceToGo;
	public bool inTown;
	public float newFOV;
	public float oldFOV;

	void Start () {

		if(!gm){
			gm = GameObject.Find("_gameMaster");
		}
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		player.transform.position = entrance.transform.position;

		gm.GetComponent<GameMaster>().setPartyPosition(player);
		GameMaster.mainCam.transform.position = new Vector3(vx,vy,vz);
		GameMaster.mainCam.transform.eulerAngles = new Vector3(ex,ey,ez);
		GameMaster.overworldCam.enabled = false;
		GameMaster.smoothFollowCam.enabled = false;
		GameMaster.inParty = true;
		GameMaster.currentLevel = townName;
		GameMaster.inTown = true;
	}

	void Update(){
		if (entered && Input.GetKey(KeyCode.T)){
			GameMaster.BattleCounterReset();
			GameMaster.inTown = inTown;
			GameMaster.mainCam.fieldOfView = oldFOV;
			gm.GetComponent<GameMaster>().changeFOV(oldFOV);
			SceneManager.LoadScene(PlaceToGo);
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
