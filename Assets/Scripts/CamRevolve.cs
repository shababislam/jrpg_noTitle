using UnityEngine;
using System.Collections;

public class CamRevolve : MonoBehaviour {

	public Transform target;

	void LateUpdate(){
		transform.LookAt(target);
		transform.Translate(Vector3.right * 3 * Time.deltaTime);
	}
}
