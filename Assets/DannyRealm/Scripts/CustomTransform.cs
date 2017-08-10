using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CustomTransform : MonoBehaviour {

	public Vector3 Position { get; set; }
	public float Rotation { get; set; }
	public float Scale { get; set; }

	public void Translate(Vector3 displacement) {
		Position += displacement;
	}

	public void Rotate(float angle) {
		Rotation += angle;
	}
}
