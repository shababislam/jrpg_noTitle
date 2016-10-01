using UnityEngine;
using System.Collections;

public class CombatCamera : MonoBehaviour {

	public GameObject target;
	public GameObject lookat;
	public GameObject player;
	public GameObject camPos;

	private bool smooth = true;
	private float smoothSpeed = 4f;
	private  Vector3 offset = new Vector3(0,0,-6f);

	private float dist;

	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag("Target");
		player = GameObject.FindGameObjectWithTag("Player");
		camPos = GameObject.FindGameObjectWithTag("CamTarget");
		dist = (player.transform.position - target.transform.position).magnitude;
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector3 targetPos = player.transform.position + offset;
		transform.position = Vector3.Lerp(transform.position,camPos.transform.position,smoothSpeed * Time.deltaTime);
		transform.rotation = Quaternion.Slerp(transform.rotation,player.transform.rotation,smoothSpeed * Time.deltaTime);
		//transform.LookAt(player.transform);

	}
		

}
