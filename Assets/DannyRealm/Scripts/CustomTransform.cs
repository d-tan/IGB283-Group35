using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CustomTransform : MonoBehaviour {

	public Vector3 Position { get; set; }
	public float rotation { get; set; }
	public float scale { get; set; }

	public Vector3[] TranslateRotate (float angle, Vector2 dir, Mesh mesh, Vector3 origin) {
		Matrix3x3 transformation = new Matrix3x3 ();
		Vector3[] vertices = mesh.vertices;

		// Rotation
		Matrix3x3 toOrigin = TranslationMatrix ((Vector2)(origin - Position));
		transformation = toOrigin.inverse * RotationMatrix (angle) * toOrigin;

		transformation = TranslationMatrix (dir) * transformation;

		Position = TranslationMatrix (dir).MultiplyPoint (Position);

		for (int i = 0; i < vertices.Length; i++) {
			vertices [i] = transformation.MultiplyPoint (vertices [i]);
		}

		return vertices;
	}


	Matrix3x3 TranslationMatrix(Vector2 dir) {
		Matrix3x3 transM = new Matrix3x3 ();
		transM.SetRow (0, new Vector3 (1, 0, dir.x));
		transM.SetRow (1, new Vector3 (0, 1, dir.y));
		transM.SetRow (2, new Vector3 (0, 0, 1));

		return transM;
	}

	public Vector3[] Translate(Vector2 dir, Mesh mesh) {
		Vector3[] vertices = mesh.vertices;
		Matrix3x3 transM = TranslationMatrix (dir);

		Position = transM.MultiplyPoint (Position);

		for (int i = 0; i < vertices.Length; i++) {
			vertices [i] = transM.MultiplyPoint (vertices [i]);
		}

		return vertices;
	}

	Matrix3x3 RotationMatrix(float angle) {
		Matrix3x3 rotM = new Matrix3x3 ();
		rotM.SetRow (0, new Vector3 (Mathf.Cos (angle), -Mathf.Sin (angle), 0));
		rotM.SetRow (1, new Vector3 (Mathf.Sin (angle), Mathf.Cos (angle), 0));
		rotM.SetRow (2, new Vector3 (0, 0, 1));

		return rotM;
	}

	public Vector3[] Rotate(float angle, Mesh mesh) {
		Vector3[] vertices = mesh.vertices;
		Matrix3x3 rotM = RotationMatrix (angle);

		for (int i = 0; i < vertices.Length; i++) {
			vertices [i] = rotM.MultiplyPoint (vertices [i]);
		}

		return vertices;
	}

	Matrix3x3 ScaleMatrix(Vector3 scale) {
		Matrix3x3 scaleM = new Matrix3x3 ();
		scaleM.SetRow (0, new Vector3(scale.x, 0, 0));
		scaleM.SetRow (1, new Vector3(0, scale.y, 0));
		scaleM.SetRow (2, new Vector3(0, 0, scale.z));

		return scaleM;
	}

	public Vector3[] Scale(Vector3 scale, Mesh mesh) {
		Vector3[] vertices = mesh.vertices;
		Matrix3x3 scaleM = ScaleMatrix (scale);

		for (int i = 0; i < vertices.Length; i++) {
			vertices [i] = scaleM.MultiplyPoint (vertices [i]);
		}

		return vertices;
	}
}
