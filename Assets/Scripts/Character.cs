using UnityEngine;
using System.Collections;


public class Character : MonoBehaviour {

	//characterData data;
	public float health;
	public float currentHealth;
	public BarScript healthBar;

	void Start () {
		GameObject.DontDestroyOnLoad(this);
		health = 100f;
		currentHealth = health;
		//data.health = health;
	}

	public void takeDamage(float n){
		currentHealth-=n;
		healthBar.setHealth(currentHealth-n);
		//data.currentHealth = currentHealth;
	}

	public float CurrentHealth(){
		return currentHealth;
	}
}
	