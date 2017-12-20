using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	public float speed = 10;

	void Start () {
		
		GetComponent<Rigidbody2D>().velocity = speed * transform.up;
	}
	
}
