using UnityEngine;
using System.Collections;

public class CameraSwitchPortal : MonoBehaviour {
	public Camera newCam;
	public Camera currentCam;

	void OnTriggerExit(Collider other){
		if (other.tag == "Player"){
			if(currentCam.gameObject.activeSelf){
				currentCam.gameObject.SetActive(false);
				newCam.gameObject.SetActive(true);
			} else {
				newCam.gameObject.SetActive(false);
				currentCam.gameObject.SetActive(true);

			}
		}
	}
}
