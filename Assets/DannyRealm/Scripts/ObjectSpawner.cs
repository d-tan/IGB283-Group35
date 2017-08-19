using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

    // The object prefab
    public GameObject polygon;

    public int number = 2;
    public int distance = 2;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < number; i++)
        {
            Vector3 position = new Vector3();
            position.x = i * distance;
            GameObject objectInstance = Instantiate(polygon);
            RenderObject ro = objectInstance.GetComponent<RenderObject>();
            objectInstance.transform.position = position;
            ro.rotationSpeed += i;
            ro.translateSpeed += i;
        }
	}
	

//	public GameObject spawnObject;
//
//	[Header("Spawn Settings")]
//	public int spawnAmount = 2;
//	public float spacing = 2f;
//	public Vector3 startingPoint;
//
//	// Use this for initialization
//	void Start () {
//		GameObject spawnedObject;
//		RenderObject script;
//		for (int i = 0; i < spawnAmount; i++) {
//			spawnedObject = Instantiate (spawnObject, startingPoint + Vector3.right * spacing * i, transform.rotation) as GameObject;
//
//			script = spawnObject.GetComponent<RenderObject> ();
//			script.moveTime = Random.Range (1.5f, 5f);
//			script.rotationSpeed = Random.Range (-Mathf.PI / 2f, Mathf.PI / 2f);
//			script.segments = Random.Range (3, 11);
//		}
//	}
}
