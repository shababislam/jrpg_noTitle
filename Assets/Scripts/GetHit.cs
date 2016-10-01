using UnityEngine;
using System.Collections;

public class GetHit : MonoBehaviour {
	public bool hit;

	private void OnTriggerEnter(Collider other){
		if (other.tag == "Hit"){
			hit = true;
		}
	}

	private void OnTriggerExit(){
		hit = false;
	}
}

