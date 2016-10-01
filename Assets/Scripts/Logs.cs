using UnityEngine;
using System.Collections;

public class Logs : MonoBehaviour {
	public static bool LogsExists;

	void Start () {
		if(!LogsExists){
			LogsExists = true;
			DontDestroyOnLoad(this);
		} else {
			Destroy(gameObject);
		}
	}
	
	void Update () {
	
	}
}
