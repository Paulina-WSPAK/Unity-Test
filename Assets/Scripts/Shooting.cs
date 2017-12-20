using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

	public float shootingRate = 2f;
	public float shotLifetime = 3f;
	public GameObject shot;

	void Start () {
		StartCoroutine (ShotCoroutine ());
	}
	
	IEnumerator ShotCoroutine () {

		while (gameObject.activeSelf) {
			GameObject clone = Instantiate (shot, transform.position, transform.rotation) as GameObject;
			Destroy (clone, shotLifetime);
			yield return new WaitForSeconds (1 / shootingRate);
		}

	}
}
