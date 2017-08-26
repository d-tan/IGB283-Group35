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
	[HideInInspector]
	public float rotationSpeed = -Mathf.PI / 2f;
	Vector3 origin;

	[Header("Task1")]
	public Vector3 pos1;
	public Vector3 pos2;
	float moveTimer = 0f;
	public float moveTime = 3f;
	int direction = 1;

	void Start() {
		gameObject.AddComponent<MeshRenderer> ();
		gameObject.AddComponent<MeshFilter> ();

		mesh = GetComponent<MeshFilter> ().mesh;
		GetComponent<MeshRenderer> ().material = mat;

		scale = radius;
		Position = pos1;
		origin = Position;

		DrawShape ();


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
		BetweenTwoPoints ();

		SetMeshColours ();

		mesh.RecalculateBounds ();
	}


	/// <summary>
	/// Draw a basic shape
	/// </summary>
	void DrawShape() {

		// Check if the number of segments is less that the minimum specified
		if (segments < minSegments)
			segments = minSegments;

		// Start Calculating the vertices
		Vector3[] verts = new Vector3[segments * 3];

		// The angle of each triangle at the centre
		float angle = 2 * Mathf.PI / (float)segments;

		// Initialise the vertices
		for (int i = 0; i < verts.Length; i += 3) {
			verts [i] = new Vector3(0, Position.y, 1f) * scale;
			verts [i + 1] = new Vector3 (Mathf.Cos (i / 3 * angle), Mathf.Sin (i / 3 * angle) + Position.y, 1f) * scale;
			verts [i + 2] = new Vector3 (Mathf.Cos (i/3 * angle + angle), Mathf.Sin (i/3 * angle + angle) + Position.y, 1f) * scale;
		}

		// Set up the triangles
		int[] tris = new int[verts.Length];

		for (int i = 0; i < tris.Length; i++) {
			tris [i] = i;
		}

		// Set each property for the mesh
		mesh.vertices = verts;

		SetMeshColours ();

		mesh.triangles = tris;
	}

	void SetMeshColours() {
		float p = Mathf.Clamp( Vector3.Distance(Position, pos1) / Vector3.Distance(pos1, pos2), 0f, 1f);

		// Set colours depending on their position
		Color[] colour = new Color[mesh.vertices.Length];

		for (int i = 0; i < colour.Length; i++) {
			colour [i] = new Color (p, p/2, p/3, 1);
		}

		mesh.colors = colour;
	}


	/// <summary>
	/// Moves the object between two of the specified points
	/// </summary>
	void BetweenTwoPoints() {

		if (direction < 0) {
			// Move back to pos1
			DoDirectionalTransformation (pos1, pos2, 1);
		} else if (direction > 0) {
			// Move towards pos2
			DoDirectionalTransformation (pos2, pos1, -1);
		}
			
		Debug.DrawLine (pos1, pos1 + Vector3.right);
		Debug.DrawLine (pos2, pos2 + Vector3.right);
		Debug.DrawLine (pos1, Position);
	}


	/// <summary>
	/// Transform towards v1 from v2
	/// </summary>
	/// <param name="v1">V1.</param>
	/// <param name="v2">V2.</param>
	/// <param name="dir">Direction to change when point is reached.</param>
	void DoDirectionalTransformation(Vector3 v1, Vector3 v2, int dir) {
		// Translate and Rotate the mesh
		mesh.vertices = TranslateRotate (rotationSpeed * Time.deltaTime, (Vector2)(v1 - Position).normalized * Time.deltaTime * translateSpeed, mesh, origin);
		// Check if our position has passed the point we are moving towards
		if (Vector3.Distance(v2, v1) <= Vector3.Distance(Position, v2)) {
			mesh.vertices = TranslateRotate (rotationSpeed * Time.deltaTime, (Vector2)(v1 - Position), mesh, origin);
			direction = dir;
		}
	}
}
