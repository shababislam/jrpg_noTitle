using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneChangeTown : MonoBehaviour {

	public GameObject destination;
	public float vx, vy, vz, ex, ey, ez;
	public GameObject CameraPosition;
	public MainTownScript m_town;

	void OnTriggerStay(Collider other){
		if (other.tag == "Player"&& Input.GetKeyUp(KeyCode.T)){
			//GameMaster.mainCam.transform.position = new Vector3(vx,vy,vz);
			//GameMaster.mainCam.transform.eulerAngles = new Vector3(ex,ey,ez);
			GameMaster.lastCameraPosition = CameraPosition.transform.position;
			GameMaster.lastCameraRotation= CameraPosition.transform.eulerAngles;
			GameMaster.mainCam.transform.position = CameraPosition.transform.position;
			m_town.GetComponent<MainTownScript>().setPP(destination);
			GameMaster.mainCam.transform.eulerAngles = CameraPosition.transform.eulerAngles;

			other.transform.position = destination.transform.position;
			//GameMaster.setPartyPosition(other.gameObject);
		} 
	}

}
