using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderObject : CustomTransform {

	Mesh mesh;
	public Material mat;

	[Header("Shape")]
	public int segments = 3;
	const int minSegments = 3;
	public float radius = 1f;

	[Header("Transformations")]
	public float translateSpeed = 2f;
	float rotationSpeed = -Mathf.PI / 2f;

	[Header("Task1")]
	public Vector3 pos1;
	public Vector3 pos2;
	float moveTimer = 0f;
	float moveTime = 3f;

	void Start() {
		gameObject.AddComponent<MeshRenderer> ();
		gameObject.AddComponent<MeshFilter> ();

		mesh = GetComponent<MeshFilter> ().mesh;
		GetComponent<MeshRenderer> ().material = mat;

		Scale = radius;
		Position = pos1;


//		verts [0] = Vector3.zero;
//		verts [1] = Vector3.right;
//		verts [2] = new Vector3 (Mathf.Cos (2 * Mathf.PI / 3f), Mathf.Sin (2 * Mathf.PI / 3f), 0f);
//		verts [3] = new Vector3 (Mathf.Cos (2 * Mathf.PI / 3f * 2), Mathf.Sin (2 * Mathf.PI / 3f * 2), 0f) + Vector3.up * 3;
//		verts [4] = new Vector3 (Mathf.Cos (2 * Mathf.PI / 3f * 3), Mathf.Sin (2 * Mathf.PI / 3f * 3), 0f) + Vector3.up * 3;
//		verts [5] = new Vector3 (Mathf.Cos (2 * Mathf.PI / 3f * 4), Mathf.Sin (2 * Mathf.PI / 3f * 4), 0f) + Vector3.up * 3;

//		mesh.colors = new Color[] {
//			Color.white,
//			Color.white,
//			Color.white,
//			Color.black,
//			Color.black,
//			Color.black
//		};
//

	}

	void Update() {
		Translate (Vector3.right * translateSpeed * Time.deltaTime);
		Rotate (rotationSpeed * Time.deltaTime);

		DrawShape ();
	}

	void DrawShape() {

		if (segments < minSegments)
			segments = minSegments;

		Vector3[] verts = new Vector3[segments * 3];

		float angle = 2 * Mathf.PI / (float)segments;

		for (int i = 0; i < verts.Length; i += 3) {
			verts [i] = Vector3.zero + this.Position;
			verts [i + 1] = new Vector3 (Mathf.Cos (i / 3 * angle + Rotation), Mathf.Sin (i / 3 * angle + Rotation), 0f) * Scale + this.Position;
			verts [i + 2] = new Vector3 (Mathf.Cos (i/3 * angle + angle + Rotation), Mathf.Sin (i/3 * angle + angle + Rotation), 0f) * Scale + this.Position;
//			Debug.Log ("Tri: " + verts [i] + " | " + verts [i + 1] + " | " + verts [i + 2]);
		}

		int[] tris = new int[verts.Length];

		for (int i = 0; i < tris.Length; i++) {
			tris [i] = i;
		}

		mesh.vertices = verts;

		mesh.triangles = tris;
	}

	void BetweenTwoPoints() {
		
	}
}
