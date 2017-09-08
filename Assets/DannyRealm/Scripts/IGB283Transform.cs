using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IGB283Transform : MonoBehaviour {

	// valriables to store the transform's values
	public Vector3 Position { get; set; }
	public float rotation { get; set; }
	public float scale { get; set; }


	/// <summary>
	/// Translates and rotates this transform
	/// </summary>
	/// <returns>The vertices with the applied transformation.</returns>
	/// <param name="angle">The angle to rotate the mesh by in radians.</param>
	/// <param name="dir">The direction to translate to.</param>
	/// <param name="mesh">The mesh to apply the transformation to.</param>
	/// <param name="origin">The origin of the object.</param>
	public Vector3[] TranslateRotate (float angle, Vector2 dir, Mesh mesh, Vector3 origin) {

		Matrix3x3 transformation = new Matrix3x3 (); // the final transformation matrix
		Vector3[] vertices = mesh.vertices; // Store the mesh vertices

		// Rotation
		Matrix3x3 toOrigin = TranslationMatrix ((Vector2)(origin - Position)); // Origin to translate to
		transformation = toOrigin.inverse * RotationMatrix (angle) * toOrigin; // Original Pos, Rotate, To the origin

		// Translation
		transformation = TranslationMatrix (dir) * transformation;

		// Translate virtual position
		Position = TranslationMatrix (dir).MultiplyPoint (Position);

		// Apply transformation to each vertice
		for (int i = 0; i < vertices.Length; i++) {
			vertices [i] = transformation.MultiplyPoint (vertices [i]);
		}

		return vertices;
	}

	/// <summary>
	/// Calculates the translation matrix to translate to the given point
	/// </summary>
	/// <returns>The translation matrix for the given point.</returns>
	/// <param name="dir">The point in which to translate to.</param>
	Matrix3x3 TranslationMatrix(Vector2 dir) {
		Matrix3x3 transM = new Matrix3x3 ();

		// Create translation matrix
		transM.SetRow (0, new Vector3 (1, 0, dir.x));
		transM.SetRow (1, new Vector3 (0, 1, dir.y));
		transM.SetRow (2, new Vector3 (0, 0, 1));

		return transM;
	}

	/// <summary>
	/// Translates a mesh to the given point
	/// </summary>
	/// <returns>The array of vertices with the translation applied.</returns>
	/// <param name="dir">The point in which to translate to.</param>
	/// <param name="mesh">The mesh to apply the translation to.</param>
	public Vector3[] Translate(Vector2 dir, Mesh mesh) {
		
		Vector3[] vertices = mesh.vertices; // Store vertices
		Matrix3x3 transM = TranslationMatrix (dir); // Create translation matrix

		// Translate the virtual position
		Position = transM.MultiplyPoint (Position);

		// Apply translation to each vertice
		for (int i = 0; i < vertices.Length; i++) {
			vertices [i] = transM.MultiplyPoint (vertices [i]);
		}

		return vertices;
	}


	/// <summary>
	/// Calculates the rotation matrix around the origin by the given angle
	/// </summary>
	/// <returns>The rotation matrix around the origin by the given angle.</returns>
	/// <param name="angle">The angle to rotate by in radians.</param>
	Matrix3x3 RotationMatrix(float angle) {
		
		Matrix3x3 rotM = new Matrix3x3 ();

		// Create the rotation matrix
		rotM.SetRow (0, new Vector3 (Mathf.Cos (angle), -Mathf.Sin (angle), 0));
		rotM.SetRow (1, new Vector3 (Mathf.Sin (angle), Mathf.Cos (angle), 0));
		rotM.SetRow (2, new Vector3 (0, 0, 1));

		return rotM;
	}


	/// <summary>
	/// Rotates the mesh by the given angle around the origin
	/// </summary>
	/// <returns>The array of vertices with the rotation applied.</returns>
	/// <param name="angle">The angle to rotate by in radians.</param>
	/// <param name="mesh">The mesh to apply the rotation to.</param>
	public Vector3[] Rotate(float angle, Mesh mesh) {
		
		Vector3[] vertices = mesh.vertices; // Store vertices
		Matrix3x3 rotM = RotationMatrix (angle); // Create rotation matrix 

		// Apply rotation matrix to each vertice
		for (int i = 0; i < vertices.Length; i++) {
			vertices [i] = rotM.MultiplyPoint (vertices [i]);
		}

		return vertices;
	}

	/// <summary>
	/// Calculates the scale matrix for the given directions
	/// </summary>
	/// <returns>The scale matrix for the given directions.</returns>
	/// <param name="scale">The vector in which to scale in the x, y and z directions.</param>
	Matrix3x3 ScaleMatrix(Vector3 scale) {
		Matrix3x3 scaleM = new Matrix3x3 ();

		// Create the scale matrix
		scaleM.SetRow (0, new Vector3(scale.x, 0, 0));
		scaleM.SetRow (1, new Vector3(0, scale.y, 0));
		scaleM.SetRow (2, new Vector3(0, 0, scale.z));

		return scaleM;
	}

	/// <summary>
	/// Scales the mesh by the given directions
	/// </summary>
	/// <returns>The array of vertices with the scale applied.</returns>
	/// <param name="scale">The vector in which to scale in the x, y and z directions.</param>
	/// <param name="mesh">The mesh to apply the scaling to.</param>
	public Vector3[] Scale(Vector3 scale, Mesh mesh) {
		Vector3[] vertices = mesh.vertices;
		Matrix3x3 scaleM = ScaleMatrix (scale);

		for (int i = 0; i < vertices.Length; i++) {
			vertices [i] = scaleM.MultiplyPoint (vertices [i]);
		}

		return vertices;
	}
}
