using UnityEngine;
using System.Collections;

public class EnemyHandCollider : MonoBehaviour {
	public bool hit;

	void Update(){
	}

	private void OnTriggerEnter(Collider other){
		if (other.tag == "PlayerBody"){
			hit = true;
		}
	}

	private void OnTriggerExit(){
		hit = false;
	}
}
