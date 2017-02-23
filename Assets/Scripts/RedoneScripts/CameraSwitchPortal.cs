using UnityEngine;
using System.Collections;

public class CameraSwitchPortal : MonoBehaviour {

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
	public Transform rootObj;
	public Transform currentArea;
	private CameraFollow CameraScript;
	private Vector3 lastPos;

	private Vector3 oldCamPosition;
	public Transform _camTarget;
	public float _xLower;
	public float _xUpper;
	public float _zLower;
	public float _zUpper;
	public float _yVal;
	public float _moveSpeed;

	void Start(){
		CameraScript = Camera.main.GetComponent<CameraFollow>();
	}
		


	void OnTriggerExit(Collider other){
		if (other.tag == "Player"){

			if(lastPos!=Vector3.zero){
				Debug.Log("Let's do this");
				Camera.main.transform.position = lastPos;

				//Camera.main.transform.rotation = newCam.transform.rotation;
				//CameraScript.cameraTarget = _camTarget;

				CameraScript.xLower = _xLower;
				CameraScript.xUpper = _xUpper;
				CameraScript.zLower = _zLower;
				CameraScript.zUpper = _zUpper;
				CameraScript.yVal = _yVal;
				CameraScript.moveSpeed = _moveSpeed;
				CameraScript.cameraTarget = _camTarget;
				/*
				if(_camTarget){
					CameraScript.cameraTarget = _camTarget;
				} else {
					CameraScript.cameraTarget = null;
				}*/
				lastPos = Vector3.zero;
			} else {
				Debug.Log("We are here");
				_camTarget = CameraScript.cameraTarget;
				_xLower = CameraScript.xLower;
				_xUpper = CameraScript.xUpper;
				_zLower = CameraScript.zLower;
				_zUpper = CameraScript.zUpper;
				_yVal = CameraScript.yVal;
				_moveSpeed = moveSpeed;
				lastPos = CameraScript.transform.position;

				//other.gameObject.GetComponent<PlayerControl>().flipSwitch();


				Camera.main.transform.localPosition = newCam.transform.localPosition;

				Camera.main.transform.rotation = newCam.transform.rotation;

				CameraScript.xLower = xLower;
				CameraScript.xUpper = xUpper;
				CameraScript.zLower = zLower;
				CameraScript.zUpper = zUpper;
				CameraScript.yVal = yVal;
				CameraScript.moveSpeed = moveSpeed;
				CameraScript.cameraTarget = camTarget;
				/*
				if(camTarget){
				} else {
					CameraScript.cameraTarget = null;
				} */
			}


		} 
	}
}
