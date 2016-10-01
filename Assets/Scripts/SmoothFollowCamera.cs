using UnityEngine;
using System.Collections;

public class SmoothFollowCamera : MonoBehaviour {

	private GameObject target;
	private Vector3 offset;
	private Vector3 targetPos;
	// Use this for initialization
	void Start () {
		if(!target)
			FindTarget();
		offset = new Vector3(0,10,-15f);
	}
	
	void LateUpdate () {

		if(!target)
			FindTarget();
		
		targetPos = target.transform.position + offset;
		transform.LookAt(target.transform.position + transform.rotation * Vector3.forward * Time.deltaTime,Vector3.up * Time.deltaTime * 5);

		transform.position = Vector3.Lerp(transform.position,targetPos,2 * Time.deltaTime);
	}

	private void FindTarget(){
		target = GameObject.FindGameObjectWithTag("CamTarget");
	}
}
