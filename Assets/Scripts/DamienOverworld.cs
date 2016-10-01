using UnityEngine;
using System.Collections;

public class DamienOverworld : MonoBehaviour {

	public static bool playerExists;
	private CombatControl attack;
	private PlayerControl walk;

	void Start () {

		if(!playerExists){
			playerExists = true;
			attack = this.gameObject.GetComponent<CombatControl>();
			walk = this.gameObject.GetComponent<PlayerControl>();

			GameObject.DontDestroyOnLoad(this);
		} else {
			Destroy(gameObject);
		}
	}
	
	void Update () {
		if (GameMaster.fighting){
			attack.enabled = true;
			walk.enabled = false;
		}
		if (!GameMaster.fighting){
			attack.enabled = false;
			walk.enabled = true;
		}
	}

	public Vector3 lastPos(){
		return this.transform.position;
	}

	private Quaternion updatedRotation(){
		return this.transform.localRotation;
	}

}
