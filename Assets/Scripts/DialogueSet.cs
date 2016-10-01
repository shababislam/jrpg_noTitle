using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueSet : MonoBehaviour {

	public string[] dialogues;
	public GameObject dBox;
	public Text dText;
	public bool activeText;
	public int index;
	public bool opening;
	public bool closing;
	public float speechBubbleTime = 20f;

	void Start () {
		dBox.SetActive(false);
		activeText = false;
		index = 0;
		dText.text = dialogues[index];
		opening = false;
		closing = false;
	}
	
	void Update () {
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



		if(activeText && Input.GetKeyDown(KeyCode.Space)){
			index++;
		}
		if(activeText && Input.GetKeyDown(KeyCode.F)){
			closing = true;

		}
		if(index >= dialogues.Length){
			index = 0;
		}
		dText.text = dialogues[index];

	}


	public void ShowBox(){
		opening = true;
		dBox.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
		activeText = true;
		dBox.SetActive(true);
	}
}
