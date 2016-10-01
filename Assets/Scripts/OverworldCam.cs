using UnityEngine;
using System.Collections;

public class OverworldCam : MonoBehaviour {

	public GameObject target;
	private Vector3 offset;
	private Vector3 targetPos;

	// Use this for initialization
	void Start () {
		findPlayer();
		offset = new Vector3(0,20,-25f);
	}

	void LateUpdate () {
		if (!target)
			findPlayer();
		

		targetPos = target.transform.position + offset;
		transform.LookAt(target.transform.position + transform.rotation * Vector3.forward * Time.deltaTime,Vector3.up * Time.deltaTime * 5);

		transform.position = Vector3.Lerp(transform.position,targetPos,2 * Time.deltaTime);
	}

	private void findPlayer(){
		target = GameObject.FindGameObjectWithTag("CamTarget");

	}
}
