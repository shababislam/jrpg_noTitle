using UnityEngine;
using System.Collections;

public class HandCollider : MonoBehaviour {
	public bool hit;

	void Update(){
	}

	private void OnTriggerStay(Collider other){
		if (other.tag == "EnemyBody"){
			hit = true;
		}

	}

	private void OnTriggerExit(){
		hit = false;
	}
}
