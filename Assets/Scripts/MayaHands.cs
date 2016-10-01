using UnityEngine;
using System.Collections;

public class MayaHands : MonoBehaviour {
	public bool hit;

	void Update(){
		Debug.Log("is this working?: "+hit);
	}

	private void OnTriggerEnter(Collider other){
		if (other.tag == "EnemyBody"){
			hit = true;
		}
	}

	private void OnTriggerExit(){
		hit = false;
	}
}