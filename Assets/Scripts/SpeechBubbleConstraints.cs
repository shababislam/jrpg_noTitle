using UnityEngine;
using System.Collections;

public class SpeechBubbleConstraints : MonoBehaviour {

	Vector3 offset;
	Vector3 targetpos;
	public Camera cam;

	void Start () {
		//offset = new Vector3(5f,0,0);
		cam = Camera.main;
	}

	// Update is called once per frame
	void Update () {
		Vector3 LL = cam.ViewportToWorldPoint(new Vector3(0,0,cam.nearClipPlane));
		Vector3 UR = cam.ViewportToWorldPoint(new Vector3(1,1,cam.nearClipPlane));

		float distance = Vector3.Distance(transform.position,UR);

		offset = new Vector3(distance,distance,distance);
		targetpos = transform.position + offset;
		//transform.position = Vector3.Lerp(transform.position,targetpos,3*Time.deltaTime);
	
	}
}
