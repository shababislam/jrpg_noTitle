using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour {
	private float health;
	// Use this for initialization
	void Start () {
		health = 100f;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log("Stubbs' health: " + " " + health);
	}

	public void takeDamage(float n){
		health = health - n;
	}
}
