using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueSet : MonoBehaviour {

	public string[] dialogues;
	public GameObject dBox;
	public GameObject selectionMarker;
	public Text dText;
	public bool activeText;
	public int index;
	public bool opening;
	public bool closing;
	public float speechBubbleTime = 20f;
	private Vector3 target;
	private Quaternion rot;
	private bool selected = false;

	void Start () {
		dBox.SetActive(false);
		activeText = false;
		index = 0;
		dText.text = dialogues[index];
		opening = false;
		closing = false;
		rot = transform.rotation;
	}
	
	void Update () {
		if(selected){
			selectionMarker.SetActive(true);
		} else {
			selectionMarker.SetActive(false);
		}

		if(opening){
			dBox.transform.localScale += new Vector3(0.5f,0.5f,0.5f) * speechBubbleTime *Time.deltaTime;
			if(dBox.transform.localScale.x > 1f){
				opening = false;
			}
		}

		if(closing){
			dBox.transform.localScale -= new Vector3(0.5f,0.5f,0.5f) * speechBubbleTime *Time.deltaTime;
			if(dBox.transform.localScale.x < 0.1f){
				closing = false;
				dBox.SetActive(false);
				activeText = false;
				index = 0;
			}
		}

		if(target != Vector3.zero){
			Quaternion rotation1 = Quaternion.LookRotation(target-transform.position);
			this.transform.rotation = Quaternion.Slerp(transform.rotation, rotation1,Time.deltaTime*10);
			//this.transform.rotation = rotation1;
		} 

		if(activeText && Input.GetKeyDown(KeyCode.Space)){
			index++;
		}
		if(activeText && Input.GetKeyDown(KeyCode.F)){
			closing = true;
			target = Vector3.zero;

		}
		if(index >= dialogues.Length){
			index = 0;
		}
		dText.text = dialogues[index];

	}


	public void ShowBox(Vector3 targetPos){
		opening = true;
		target = targetPos;
		dBox.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
		activeText = true;
		dBox.SetActive(true);
	}


	public void isSelected(){
		selected = true;
	}
	public void unSelect(){
		selected = false;
	}
}
