﻿using UnityEngine;
using System.Collections;

public class RotationTest : MonoBehaviour {
	public float speed = 5f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.back * Time.deltaTime * speed);

	}
}
