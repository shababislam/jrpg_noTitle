using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainTownScript : MonoBehaviour {

	public GameObject entrance;
	public GameObject gm;
	public float vx, vy, vz, ex, ey, ez;
	public string townName;

	private bool entered;
	public string PlaceToGo;
	public float newFOV;
	public float oldFOV;
	public GameObject CameraPosition;

	void Start () {

		if(!gm){
			gm = GameObject.Find("_gameMaster");
		}
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if(!GameMaster.inTown){
			player.transform.position = entrance.transform.position;
			GameMaster.mainCam.transform.position = CameraPosition.transform.position;
			GameMaster.mainCam.transform.eulerAngles = CameraPosition.transform.eulerAngles;
		} 
		if(GameMaster.inTown){
			GameMaster.mainCam.transform.position = GameMaster.lastCameraPosition;
			GameMaster.mainCam.transform.eulerAngles = GameMaster.lastCameraRotation;
			player.transform.position = GameMaster.lastTownPos;
		} 

		gm.GetComponent<GameMaster>().setPartyPosition(player);

		if(GameMaster.mainCam.fieldOfView!=newFOV){
			GameMaster.mainCam.fieldOfView = newFOV;
			gm.GetComponent<GameMaster>().changeFOV(newFOV);
		}
		GameMaster.overworldCam.enabled = false;
		GameMaster.smoothFollowCam.enabled = false;
		GameMaster.cr.enabled = false;
		GameMaster.inParty = true;
		GameMaster.currentLevel = townName;
		GameMaster.inTown = true;
	}

	void Update(){
		if (entered && Input.GetKey(KeyCode.T)){
			GameMaster.BattleCounterReset();
			GameMaster.inTown = false;
			GameMaster.mainCam.fieldOfView = oldFOV;
			GameMaster.cr.enabled = true;
			gm.GetComponent<GameMaster>().changeFOV(oldFOV);
			SceneManager.LoadScene(PlaceToGo);
		}
	} 

	public void setPP(GameObject location){
		gm.GetComponent<GameMaster>().setPartyPosition(location);
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
