using UnityEngine;
using System.Collections;

public class MayaSpecial : MonoBehaviour {
	public static bool MayaExists;
	public PartyAttackScript attack;
	public PartyFollowScript follow;

	void Start () {
		if(!MayaExists){
			MayaExists = true;
			GameObject.DontDestroyOnLoad(this);
			attack = this.gameObject.GetComponent<PartyAttackScript>();
			follow = this.gameObject.GetComponent<PartyFollowScript>();
		} else {
			Destroy(gameObject);
		}
	}

	void Update(){
		if (GameMaster.fighting){
			attack.enabled = true;
			follow.enabled = false;
		} else {
			attack.enabled = false;
			follow.enabled = true;
		}
	}
}
