using UnityEngine;
using System.Collections;

public class CameraMain : MonoBehaviour {

	public OverworldCam overworldCam;
	public SmoothFollowCamera smoothFollowCam;
	public static bool mainCamExists;

	void Start () {
		if(!mainCamExists){
			mainCamExists = true;
			overworldCam = GetComponent<OverworldCam>();
			smoothFollowCam = GetComponent<SmoothFollowCamera>();
			GameObject.DontDestroyOnLoad(this);
		} else {
			Destroy(gameObject);
		}
	}
	
	void Update () {

	}
}
