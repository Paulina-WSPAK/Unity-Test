using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour {
	private float xSpeed, ySpeed;

	private Transform myTransform; 

	void Start ( ) {
		myTransform = transform;
		
		float direction = Random.Range (0f, 360f);
		float speed = Random.Range (0.1f, 2f);

		xSpeed = Mathf.Sin (direction) * speed;
		ySpeed = Mathf.Cos (direction) * speed;

		//StartCoroutine (Movement ());
	}

	IEnumerator Movement () {
		for (;;) {
			myTransform.Translate (new Vector3 (xSpeed * Time.deltaTime, ySpeed * Time.deltaTime, 0f));
			yield return null;
		}
	}
}
