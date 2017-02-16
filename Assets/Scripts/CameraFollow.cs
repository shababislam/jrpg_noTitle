using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public Transform target;


	public Transform cameraTarget;
	public Texture2D fadeTex;

	public float xLower = 1f;
	public float xUpper = 1f;
	public float zLower = 1f;
	public float zUpper = 1f;
	public float yVal = 0;
	public float moveSpeed = 1f;

	public bool moving = false;
	public bool cameraActive = true;
	private float screenColor = 1f;

	private Vector3 lastPos;
	private Vector3 camTargetPos;

	void Start () {
		if(!target)
			target = GameObject.FindGameObjectWithTag("Player").transform;

		camTargetPos = cameraTarget.position;
	}


	void Update () {
		if(!target)
			target = GameObject.FindGameObjectWithTag("Player").transform;
		
		//cameraTarget.position = Vector3.Lerp(cameraTarget.position,target.position, Time.deltaTime*0.8f);
		if(!cameraActive){
			screenColor+=0.01f;
			if(screenColor>=1){
				screenColor = 1;
			}
			transform.gameObject.SetActive(false);

		} else {
			screenColor-=0.01f;
			if(screenColor<=0){
				screenColor = 0;
			}
		}

		if(!moving){
			cameraTarget.position = Vector3.Lerp(cameraTarget.position,target.position, Time.deltaTime*0.8f);

			var pos = cameraTarget.localPosition;
			pos.x =  Mathf.Clamp(cameraTarget.localPosition.x, xLower, xUpper);
			pos.z =  Mathf.Clamp(cameraTarget.localPosition.z, zLower, zUpper);
			pos.y = yVal;
			cameraTarget.localPosition = pos;
			transform.LookAt(cameraTarget);
		} else {
			transform.position = Vector3.Lerp(transform.position,target.position, Time.deltaTime*moveSpeed);
			Vector3 pos = transform.localPosition;
			pos.x =  Mathf.Clamp(transform.localPosition.x, xLower, xUpper);
			pos.z =  Mathf.Clamp(transform.localPosition.z, zLower, zUpper);
			pos.y = yVal;

			transform.localPosition = pos;

		}
	
	}


	public void turnOff(){
		lastPos = transform.position;
		if(cameraTarget){
			cameraTarget.gameObject.SetActive(false);
		}
		cameraActive = false;

	}

	public void turnOn(){
		screenColor = 1f;
		if(lastPos!=Vector3.zero){
			transform.position = lastPos;
		}
		if(cameraTarget){
			cameraTarget.position = camTargetPos;
			cameraTarget.gameObject.SetActive(true);
		}
		transform.gameObject.SetActive(true);

		cameraActive = true;
	}

	void OnGUI(){

		Color temp = GUI.color;

		temp.a = screenColor;

		GUI.color = temp;

		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),fadeTex);

	}

	public void setMoving(bool a){
		moving = a;
	}

	public bool isMoving(){
		return moving;
	}
}
