using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {
	public GameController gameController;

	public float shootingRate = 2f;
	public float shotLifetime = 3f;
	public GameObject shot;
	public Transform shotSpawn;


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

	public void YouLose () {
		gameObject.SetActive (false);
		gameController.YouLoose ();
	}

	public void Restart () {
		transform.position = Vector3.zero;
		transform.rotation = Quaternion.identity;
		gameObject.SetActive(true);
	}
}
