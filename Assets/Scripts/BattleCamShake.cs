using UnityEngine;
using System.Collections;

public class BattleCamShake : MonoBehaviour {

	float x;
	float y;
	float z;

	void Update () {
		x = Random.Range(0.7f,-0.7f);
		y = Random.Range(0.7f,-0.7f);
		z = Random.Range(0.7f,-0.7f);
		//transform.position += new Vector3(x,y,z);
		transform.position = Vector3.Lerp(transform.position,transform.position +new Vector3(x,y,z),2 * Time.deltaTime);

	}
}
