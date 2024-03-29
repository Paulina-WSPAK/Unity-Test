﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;

	private Vector3 offset;

	void Start () {
		offset = transform.position - target.position;
	}

	void Update () {
		if (target != null) {
			transform.position = target.position + offset;
		}
	}
}
