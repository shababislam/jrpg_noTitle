using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Town1Script : MonoBehaviour {

	public GameObject entrance;
	public GameObject gm;
	public float vx, vy, vz, ex, ey, ez;
	string townName;

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
		if(!GameMaster.inTown){
			player.transform.position = entrance.transform.position;
		} 
		if(GameMaster.inTown){
			player.transform.position = GameMaster.lastTownPos;
		} 

		gm.GetComponent<GameMaster>().setPartyPosition(player);
		GameMaster.mainCam.transform.position = new Vector3(vx,vy,vz);
		GameMaster.mainCam.transform.eulerAngles = new Vector3(ex,ey,ez);
		gm.GetComponent<GameMaster>().changeFOV(newFOV);
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
