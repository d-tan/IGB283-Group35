using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderObject : MonoBehaviour {

	Mesh mesh;
	public Material mat;

	[Header("Shape")]
	public int segments = 3;
	const int minSegments = 3;
	public float radius = 1f;


	void Start() {
		gameObject.AddComponent<MeshRenderer> ();
		gameObject.AddComponent<MeshFilter> ();

		mesh = GetComponent<MeshFilter> ().mesh;
		GetComponent<MeshRenderer> ().material = mat;

		if (segments < minSegments)
			segments = minSegments;

		Vector3[] verts = new Vector3[segments * 3];

		float angle = 2 * Mathf.PI / (float)segments;

		for (int i = 0; i < verts.Length; i += 3) {
			verts [i] = Vector3.zero;
			verts [i + 1] = new Vector3 (Mathf.Cos (i/3 * angle), Mathf.Sin (i/3 * angle), 0f) * radius;
			verts [i + 2] = new Vector3 (Mathf.Cos (i/3 * angle + angle), Mathf.Sin (i/3 * angle + angle), 0f) * radius;
			Debug.Log ("Tri: " + verts [i] + " | " + verts [i + 1] + " | " + verts [i + 2]);
//			Debug.Log ("Created Vert");
		}

		int[] tris = new int[verts.Length];

		for (int i = 0; i < tris.Length; i++) {
			tris [i] = i;
		}

//		verts [0] = Vector3.zero;
//		verts [1] = Vector3.right;
//		verts [2] = new Vector3 (Mathf.Cos (2 * Mathf.PI / 3f), Mathf.Sin (2 * Mathf.PI / 3f), 0f);
//		verts [3] = new Vector3 (Mathf.Cos (2 * Mathf.PI / 3f * 2), Mathf.Sin (2 * Mathf.PI / 3f * 2), 0f) + Vector3.up * 3;
//		verts [4] = new Vector3 (Mathf.Cos (2 * Mathf.PI / 3f * 3), Mathf.Sin (2 * Mathf.PI / 3f * 3), 0f) + Vector3.up * 3;
//		verts [5] = new Vector3 (Mathf.Cos (2 * Mathf.PI / 3f * 4), Mathf.Sin (2 * Mathf.PI / 3f * 4), 0f) + Vector3.up * 3;

		mesh.vertices = verts;

//		mesh.colors = new Color[] {
//			Color.white,
//			Color.white,
//			Color.white,
//			Color.black,
//			Color.black,
//			Color.black
//		};
//
		mesh.triangles = tris;
	}

	void Update() {
		
	}
}
