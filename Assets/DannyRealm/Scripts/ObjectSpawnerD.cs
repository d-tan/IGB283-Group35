using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnerD : MonoBehaviour {

	public GameObject spawnObject;

	[Header("Spawn Settings")]
	public int spawnAmount = 2;
	public float spacing = 2f;
	public Vector3 startingPoint;

	// Use this for initialization
	void Start () {
		

		for (int i = 0; i < spawnAmount; i++) {
			GameObject spawnedObject;
			spawnedObject = Instantiate (spawnObject, startingPoint + Vector3.right * spacing * i, transform.rotation) as GameObject;

			RenderObject script;
			script = spawnedObject.GetComponent<RenderObject> ();
			script.translateSpeed = Random.Range (1.5f, 3.5f);
			script.rotationSpeed = Random.Range (-Mathf.PI / 2f, Mathf.PI / 2f);
			script.segments = Random.Range (3, 11);
			script.pos1 += startingPoint + Vector3.right * spacing * i;
			script.pos2 += startingPoint + Vector3.right * spacing * i;
		}
	}
}
