using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float health;
	public float currentHealth;
	public BarScript healthBar;
	private string name;
	Quaternion rotation;
	public bool hitting;
	public bool attacking;
	public GameObject currentTarget;

	void Start () {
		health = 100f;
		currentHealth = health;
		name = this.transform.ToString();
		rotation = this.transform.localRotation;
		currentTarget =  null;
	}


	public void takeDamage(float n){
		currentHealth-=n;
		if(healthBar)
			healthBar.setHealth(currentHealth-n);
	}

	public float CurrentHealth(){
		return currentHealth;
	}

	private Quaternion updatedRotation(){
		return this.transform.localRotation;
	}


	public ListEntry returnEntry(){
		return new ListEntry(GameMaster.currentLevel,name,updatedRotation(),health,currentHealth,transform.position.x,transform.position.y,transform.position.z);
	}
}