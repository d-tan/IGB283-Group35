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
			GameObject spawnedObject;
			RenderObject script;
			for (int i = 0; i < spawnAmount; i++) {
				spawnedObject = Instantiate (spawnObject, startingPoint + Vector3.right * spacing * i, transform.rotation) as GameObject;
	
				script = spawnObject.GetComponent<RenderObject> ();
				script.moveTime = Random.Range (1.5f, 5f);
				script.rotationSpeed = Random.Range (-Mathf.PI / 2f, Mathf.PI / 2f);
				script.segments = Random.Range (3, 11);
			}
		}
}
