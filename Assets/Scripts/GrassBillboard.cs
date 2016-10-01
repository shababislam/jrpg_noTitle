using UnityEngine;
using System.Collections;

public class GrassBillboard : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.LookAt(Camera.main.transform);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
