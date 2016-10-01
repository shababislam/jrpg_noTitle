using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public GameObject dBox;
	public Text dText;

	private bool dialogueActive;

	void Start () {
		
	}
	
	void Update () {
		if(dialogueActive && Input.GetKeyDown(KeyCode.Space)){
			dBox.SetActive(false);
		}
	}
}
