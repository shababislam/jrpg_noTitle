using UnityEngine;
using System.Collections;
[RequireComponent(typeof(CharacterController))]
public class StubbsScript : MonoBehaviour {

	private Animator stubbsAnim;
	private CharacterController stubbsControl;
	private Player player;
	float health;

	// Use this for initialization
	void Start () {
		stubbsAnim = GetComponent<Animator>();
		stubbsControl = GetComponent<CharacterController>();
		player = GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		
		health = player.CurrentHealth();
		Vector3 motion = new Vector3(0,0,0);
		motion+=Vector3.up*-8;
		stubbsControl.Move(motion*Time.deltaTime);
	
	}

}
