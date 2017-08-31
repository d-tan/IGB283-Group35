using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleObject : RenderObject {

	Collider2D col;
	Vector2 colliderOffset;

	public RenderObject follow;
	public int posNum = 1;

	protected override void Start() {
		gameObject.AddComponent<MeshRenderer> ();
		gameObject.AddComponent<MeshFilter> ();


		// Get the mesh and set the material
		mesh = GetComponent<MeshFilter> ().mesh;
		GetComponent<MeshRenderer> ().material = mat;

		// Initialise the properties for our custom transform
		scale = radius;
		origin = new Vector3(Position.x, 0, 0);

		DrawShape ();
		gameObject.AddComponent<BoxCollider2D> ();
		col = GetComponent<Collider2D> ();
		colliderOffset = new Vector2(-Position.x, 0);
	}

	// Update is called once per frame
	protected override void Update () {
//		MouseClick ();

		if (posNum == 1) {
			follow.pos1 = this.Position;

		} else if (posNum == 2) {
			follow.pos2 = this.Position;
		}

		SetMeshColours (Color.black);

		myPos = Position;
	}

	protected void SetMeshColours(Color colour) {
		// Set colours depending on their position
		Color[] colours = new Color[mesh.vertices.Length];

		for (int i = 0; i < colours.Length; i++) {
			colours [i] = colour;
		}

		mesh.colors = colours;
	}

	public void DragObject(Vector2 mousePos) {
		mesh.vertices = Translate (mousePos - (Vector2)Position, mesh);
		col.offset = colliderOffset + mousePos;
	}
}
