using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BarScript : MonoBehaviour {

	public Image bar;
	public float fillAmount;

	float stagger = 5f;

	// Use this for initialization
	private void Start(){
		fillAmount = 1;
		bar.enabled = false;
	}

	private void Update(){
		if(GameMaster.fighting){
			bar.enabled = true;
		} else {
			bar.enabled = false;
		}
		updateBar();

		/*
		if(stagger-- <= 0){
			updateBar();
			stagger = 5;
		}*/
	}


	public void setHealth(float cur){
		fillAmount = cur/100;
	}

	public void updateBar(){
		bar.fillAmount = Mathf.Lerp(bar.fillAmount, fillAmount,0.1f);

	}
}
