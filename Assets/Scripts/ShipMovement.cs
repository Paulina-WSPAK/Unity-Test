using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour {

	public float movementSpeed = 3f;
	public float turnSpeed = 60f;

	private Rigidbody2D rb;
	private float movementInput;
	private float turnInput;

	// Use this for initialization
	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		movementInput = Input.GetAxis ("Vertical");
		turnInput = Input.GetAxis ("Horizontal");
	}

	void FixedUpdate () {
		Vector2 movement = transform.up * movementInput * movementSpeed * Time.deltaTime;
		rb.MovePosition (rb.position + movement);

		float turn = turnInput * turnSpeed * Time.deltaTime;
		rb.MoveRotation (rb.rotation - turn);
	}
}
