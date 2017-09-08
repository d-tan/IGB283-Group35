using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleObject : RenderObject {

	// Reference to collider
	Collider2D col;
	Vector2 colliderOffset;

	// Reference to the object this controls
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

		// Display handle
		DrawShape ();

		// Add collider 2D
		gameObject.AddComponent<BoxCollider2D> ();
		col = GetComponent<Collider2D> ();

		// Set offset
		colliderOffset = new Vector2(-Position.x, 0);
	}

	// Update is called once per frame
	protected override void Update () {

		// Set the respective position from the Object's script to this position
		// with respect to this handle number
		if (posNum == 1) {
			follow.pos1 = this.Position;

		} else if (posNum == 2) {
			follow.pos2 = this.Position;
		}

		// Set the colour of this mesh to black
		SetMeshColours (Color.black);

		myPos = Position;
	}

	// Set the mesh's colour given a colour
	protected void SetMeshColours(Color colour) {
		
		Color[] colours = new Color[mesh.vertices.Length];

		// Add the colour specified
		for (int i = 0; i < colours.Length; i++) {
			colours [i] = colour;
		}

		// Set the colours
		mesh.colors = colours;
	}

	// Translate this object's position to the specified position
	public void DragObject(Vector2 mousePos) {
		mesh.vertices = Translate (mousePos - (Vector2)Position, mesh); // translate the mesh
		col.offset = colliderOffset + mousePos; // translate the collider
	}
}
