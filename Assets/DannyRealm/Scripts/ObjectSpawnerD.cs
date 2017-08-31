using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnerD : MonoBehaviour {

	public GameObject spawnObject;
	public GameObject handle;

	[Header("Spawn Settings")]
	public int spawnAmount = 2;
	public float spacing = 2f;
	public Vector3 startingPoint;

	// Use this for initialization
	void Start () {
		

		for (int i = 0; i < spawnAmount; i++) {
			GameObject spawnedObject;
			GameObject handleObject;
			RenderObject renderScript;
			HandleObject handleScript;

			spawnedObject = Instantiate (spawnObject, startingPoint + Vector3.right * spacing * i, Quaternion.identity) as GameObject;

			renderScript = spawnedObject.GetComponent<RenderObject> ();
			renderScript.translateSpeed = Random.Range (1.5f, 3.5f);
			renderScript.rotationSpeed = Random.Range (-Mathf.PI / 2f, Mathf.PI / 2f);
			renderScript.segments = Random.Range (30, 40);
			renderScript.pos1 += startingPoint + Vector3.right * spacing * i;
			renderScript.pos2 += startingPoint + Vector3.right * spacing * i;


			handleObject = Instantiate (handle, renderScript.pos1, Quaternion.identity) as GameObject;
			handleScript = handleObject.GetComponent<HandleObject> ();
			handleScript.posNum = 1;

			handleObject = Instantiate (handle, renderScript.pos2, Quaternion.identity) as GameObject;
			handleScript = handleObject.GetComponent<HandleObject> ();
			handleScript.posNum = 2;

		}
	}
}
