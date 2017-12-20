using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public Text scoreText;
	public Animator youLooseAnimator;
	public AsteroidsController asteroidsController;
	public ShipController ship;

	public float shootingRate = 2f;
	public float shotLifetime = 3f;
	public GameObject shot;
	public Transform shotSpawn;

	private static int score;

	void Start () {
		//Random.InitState (1);
		asteroidsController.IstantiateAsteroids ();
		ResetScore ();

		StartCoroutine (ShotCoroutine ());
	}

	void UpdateScore () {
		scoreText.text = "Score: " + score;
	}

	public static void AddScore (int i) {
		score += i;
		//UpdateScore ();
	}
	IEnumerator ShotCoroutine () {

		while (gameObject.activeSelf) {
			GameObject clone = Instantiate (shot, transform.position, transform.rotation) as GameObject;
			Destroy (clone, shotLifetime);
			yield return new WaitForSeconds (1 / shootingRate);
		}

	}

	public void YouLoose () {
		youLooseAnimator.SetTrigger("YouLoose");

		//Freeze
		Object[] objects = FindObjectsOfType(typeof(Rigidbody2D)) ;
		foreach (Rigidbody2D o in objects) {
			o.velocity = Vector3.zero;
		}
	}

	void ResetScore () {
		score = 0;
		UpdateScore ();
	}

	public void RestartLevel () {
		ResetScore ();

		ship.Restart ();
		asteroidsController.ResetAsteroids ();
		youLooseAnimator.SetTrigger ("RestartLevel");
	}
}
