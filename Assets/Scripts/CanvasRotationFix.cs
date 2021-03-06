﻿using UnityEngine;
using System.Collections;

public class CanvasRotationFix : MonoBehaviour {

	public Camera m_Camera;
	Vector3 targetDistance;
	float distance;

	void Awake(){
		if (!m_Camera)
			m_Camera = Camera.main;
	}

	void Start(){
		if (!m_Camera)
			m_Camera = Camera.main;
	
	}

	void LateUpdate(){
		if (!m_Camera)
			m_Camera = Camera.main;


		transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward * Time.deltaTime,m_Camera.transform.rotation * Vector3.up * Time.deltaTime);

	}
} 

