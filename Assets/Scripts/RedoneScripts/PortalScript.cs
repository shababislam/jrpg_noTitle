using UnityEngine;
using System.Collections;

public class PortalScript : MonoBehaviour {

	public Transform destination;
	public Camera newCam;
	//public Camera currentCam;
	public GameObject gm;
	public Transform camTarget;
	public float xLower;
	public float xUpper;
	public float zLower;
	public float zUpper;
	public float yVal;
	public float moveSpeed;
	public bool moving = false;
	public Transform rootObj;
	public Transform currentArea;

	void Start(){
		if(!gm){
			gm = GameObject.Find("_gameMaster");
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player"){
			//if(!newCam){
			//destination.root.gameObject.SetActive(true);
			//}

			/*
			other.gameObject.GetComponent<PlayerControl>().flipSwitch();
			currentCam.gameObject.GetComponent<CameraFollow>().turnOff();
			*/

			//currentCam.gameObject.SetActive(false);
			rootObj.gameObject.SetActive(true);
			other.gameObject.GetComponent<PlayerControl>().flipSwitch();

			Camera.main.transform.SetParent(rootObj);
			Camera.main.transform.localPosition = newCam.transform.localPosition ;
			Camera.main.transform.rotation = newCam.transform.rotation;
			other.gameObject.transform.position = destination.position;
			other.gameObject.transform.rotation = destination.rotation;
			gm.GetComponent<GameMaster>().setPartyPosition(destination.gameObject);
			Camera.main.GetComponent<CameraFollow>().xLower = xLower;

			Camera.main.GetComponent<CameraFollow>().xLower = xLower;
			Camera.main.GetComponent<CameraFollow>().xUpper = xUpper;
			Camera.main.GetComponent<CameraFollow>().zLower = zLower;
			Camera.main.GetComponent<CameraFollow>().zUpper = zUpper;
			Camera.main.GetComponent<CameraFollow>().yVal = yVal;
			Camera.main.GetComponent<CameraFollow>().moveSpeed = moveSpeed;
			Camera.main.GetComponent<CameraFollow>().moving = moving;
			if(camTarget){
				Camera.main.GetComponent<CameraFollow>().cameraTarget = camTarget;
			} else {
				Camera.main.GetComponent<CameraFollow>().cameraTarget = null;
			}
			currentArea.gameObject.SetActive(false);
			//newCam.gameObject.SetActive(true);

			/*
			newCam.gameObject.GetComponent<CameraFollow>().turnOn();
			*/

			//currentCam.transform.root.gameObject.SetActive(false);

		} 
	}
}
