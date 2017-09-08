using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnerD : MonoBehaviour {

	// Reference to objects needed to spawn
	public GameObject spawnObject;
	public GameObject handle;

	[Header("Spawn Settings")]
	public int spawnAmount = 2; // Number of objects to spawn
	public float spacing = 2f; // Spacing between each object
	public Vector3 startingPoint; // The starting point in which to start spawning our objects at

	// Use this for initialization
	void Start () {
		

		for (int i = 0; i < spawnAmount; i++) {

			// Hold reference to spawned objects
			GameObject spawnedObject;
			GameObject handleObject;
			RenderObject renderScript;
			HandleObject handleScript;

			// Spawn our translating and rotating object
			spawnedObject = Instantiate (spawnObject, startingPoint + Vector3.right * spacing * i, Quaternion.identity) as GameObject;

			// Set varliables
			renderScript = spawnedObject.GetComponent<RenderObject> ();
			renderScript.translateSpeed = Random.Range (1.5f, 3.5f); // Randomise translation speed
			renderScript.rotationSpeed = Random.Range (-Mathf.PI / 2f, Mathf.PI / 2f); // Randomise rotation speed
			renderScript.segments = Random.Range (30, 40); // Randomise number of segments
			renderScript.pos1 += startingPoint + Vector3.right * spacing * i; // Set positions to translate between
			renderScript.pos2 += startingPoint + Vector3.right * spacing * i;

			// Spawn first handle at pos1
			handleObject = Instantiate (handle, startingPoint + Vector3.right * spacing * i, Quaternion.identity) as GameObject;
			handleScript = handleObject.GetComponent<HandleObject> ();
			handleScript.posNum = 1; // set handle number
			handleScript.Position = renderScript.pos1; // set virtual position
			handleScript.follow = renderScript; // set reference

			// Spawn second handle at pos2
			handleObject = Instantiate (handle, startingPoint + Vector3.right * spacing * i, Quaternion.identity) as GameObject;
			handleScript = handleObject.GetComponent<HandleObject> ();
			handleScript.posNum = 2; // set handle number
			handleScript.Position = renderScript.pos2; // set virtual position 
			handleScript.follow = renderScript; // set reference

		}
	}
}
