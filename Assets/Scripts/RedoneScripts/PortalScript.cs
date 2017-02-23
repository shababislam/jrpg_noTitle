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
	private CameraFollow CameraScript;
	private Vector3 lastPos;

	void Start(){
		if(!gm){
			gm = GameObject.Find("_gameMaster");
		}
		if(!currentArea)
			currentArea = this.transform.root;
		
		CameraScript = Camera.main.GetComponent<CameraFollow>();
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
			CameraScript.ScreenChange();

			lastPos = transform.position;

			//currentCam.gameObject.SetActive(false);

			if(rootObj){
				rootObj.gameObject.SetActive(true);
				Camera.main.transform.SetParent(rootObj);
			}
			other.gameObject.GetComponent<PlayerControl>().flipSwitch();

			if(moving && lastPos!=Vector3.zero){
				Camera.main.transform.position = lastPos;
			} else {
				Camera.main.transform.localPosition = newCam.transform.localPosition;
			}
			Camera.main.transform.rotation = newCam.transform.rotation;
			//other.gameObject.transform.position = destination.position;
			//other.gameObject.transform.rotation = destination.rotation;
			//gm.GetComponent<GameMaster>().setPartyPosition(destination.gameObject);
			CameraScript.xLower = xLower;

			CameraScript.xLower = xLower;
			CameraScript.xUpper = xUpper;
			CameraScript.zLower = zLower;
			CameraScript.zUpper = zUpper;
			CameraScript.yVal = yVal;
			CameraScript.moveSpeed = moveSpeed;
			CameraScript.moving = moving;
			if(camTarget){
				CameraScript.cameraTarget = camTarget;
			} else {
				CameraScript.cameraTarget = null;
			}
			if(rootObj)
				currentArea.gameObject.SetActive(false);

			//newCam.gameObject.SetActive(true);

			/*
			newCam.gameObject.GetComponent<CameraFollow>().turnOn();
			*/

			//currentCam.transform.root.gameObject.SetActive(false);

		} 
	}
}
