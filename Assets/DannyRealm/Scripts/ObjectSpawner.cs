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
	
	// Update is called once per frame
	void Update () {
		
	}
}
