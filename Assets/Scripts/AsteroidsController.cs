using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsController : MonoBehaviour {
	public GameObject asteroidPrefab;
	public float asteroidSpeedRange = 2f;
	public LayerMask layer;

	// A arrays of asteroid datas
	private Transform[] aArray;
	private float[] aXSpeed, aYSpeed, aXPosition, aYPosition;
	private List<int>[,] aMap;
	private bool[] aActive, aToDeactivate;

	// MY components for optimization
	private Transform myTransform;

	// task datas
	private int taskSize = 128;

	void Start () {
		aXSpeed = new float[taskSize * taskSize];
		aYSpeed = new float[taskSize * taskSize];
		aXPosition = new float[taskSize * taskSize];
		aYPosition = new float[taskSize * taskSize];
		aArray = new Transform[taskSize * taskSize];
		aMap = new List<int>[taskSize + 2, taskSize + 2];
		aActive = new bool [taskSize * taskSize];
		aToDeactivate = new bool [taskSize * taskSize];
		for (int x = 0; x < taskSize + 2; x++) {
			for (int y = 0; y < taskSize + 2; y++) {
				aMap [x, y] = new List<int> ();
			}
		}
		for (int i = 0; i < taskSize; i++) {
			aActive [i]	= false;
			aToDeactivate [i] = false;
		}
		myTransform = transform;
		StartCoroutine (AsteroidUpdate());
	}

	public void IstantiateAsteroids () {
		int x, y;
		float speed, direction;
		int id = 0;
		GameObject child;

		for (x = 0; x < taskSize; x++) {
			for (y = 0; y < taskSize; y++) {
				direction = Random.Range (0f, 360f);
				speed = Random.Range (0.1f, asteroidSpeedRange);

				aXPosition[id] = x - (taskSize / 2) + 0.5f;
				aYPosition[id] = y - (taskSize / 2) + 0.5f;
				aXSpeed[id] = Mathf.Sin (direction) * speed;
				aYSpeed[id] = Mathf.Cos (direction) * speed;

				child = Instantiate(asteroidPrefab, new Vector3(aXPosition[id], aYPosition[id], 0f), Quaternion.identity, myTransform) as GameObject;
				aArray [id] = child.transform;
				aActive [id] = true;

				id++;
			}
		}
			
	}

	IEnumerator AsteroidUpdate () {
		Transform asteroid;
		Vector3 position;
		int x, y, i;

		int xAround, yAround, listAround;
		int iArround;


		int taskWhole = taskSize * taskSize;
		int taskMap = taskSize + 2;

		while (true) {
			//Map for collisions clear
			for (x = 0; x < taskMap; x++) {
				for (y = 0; y < taskMap; y++) {
					aMap [x, y].Clear ();
				}
			}

			//asteroid moves and filling collision map
			for (i = 0; i < taskWhole; i++) {
				if (aActive [i]) {
					aXPosition [i] += aXSpeed [i] * Time.deltaTime;
					aYPosition [i] += aYSpeed [i] * Time.deltaTime;

					//put asteroid on a Map
					x = PositionOnMap (aXPosition [i]);
					y = PositionOnMap (aYPosition [i]);

					aMap [x, y].Add (i);
				}
			}

			// search for collisions 
			for (i = 0; i < taskWhole; i++) {
				if (aActive [i]) {
					// between asteroids
					x = PositionOnMap (aXPosition[i]);
					y = PositionOnMap (aYPosition[i]);
					for (xAround = x; xAround <= x + 1 && xAround < taskMap; xAround++) {
						for (yAround = y; yAround <= y + 1 && yAround < taskMap; yAround++) {
							for (listAround = 0; listAround < aMap [xAround, yAround].Count; listAround++) {
								iArround = aMap [xAround, yAround] [listAround];
								if (i != iArround) {
									//check distance;
									float yDistance = aYPosition[i] - aYPosition[iArround];
									float xDistance = aXPosition[i] - aXPosition[iArround];

									if (xDistance * xDistance + yDistance * yDistance <= 0.35f * 0.35f) {
										aToDeactivate[i] = true;
										aToDeactivate[iArround] = true;
									}
								}
							}
						}
					}

					// with ship and shots

					//Collider2D[] colliders = Physics2D.OverlapCircleAll (new Vector2(aXPosition[i], aYPosition[i]), 0.175f);

				}
			}
				
			//set all counted changes to asteroid GameObjects
			for (i = 0; i < taskWhole; i++) {
				if (aActive[i]) {
					asteroid = aArray[i];
					asteroid.localPosition = new Vector3 (aXPosition[i], aYPosition[i]);
					if (aToDeactivate[i]) {
						aActive[i] = false;
						asteroid.gameObject.SetActive(false);
					}
				}
			}

			yield return null;
		}
	}

	int PositionOnMap (float x) {
		int n = (int)x + 65;
		if (n < 0)
			n = 0;
		if (n >= 130)
			n = 129;
		return n;
	}

	public void ResetAsteroids () {
		foreach (Transform child in transform) {
//			child.GetComponent<AsteroidMovement> ().Reset ();
		}
	}
		
}
