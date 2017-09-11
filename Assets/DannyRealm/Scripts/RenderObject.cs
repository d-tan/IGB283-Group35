using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderObject : IGB283Transform {

	// Reference for mesh
	protected Mesh mesh;
	public Material mat;

	[Header("Shape")]
	public int segments = 3; // Number of segments to draw & When its 0, ObjectSpawn.cs Script will overide with a randomised number
    protected const int minSegments = 3; // the minimum number of segments allowable
	public float radius = 1f; // the radius of the shape

	[Header("Transformations")]
    public float step = 2f; // number to increase/decrease by
    public float translateSpeed = 2f; // when its bigger than max, ObjectSpawn.cs Script will overide with a randomised number
    public float maxTranslateSpeed = 10f;
    public float rotationSpeed = 2f; // when its bigger than max, ObjectSpawn.cs Script will overide with a randomised number
    public float maxRotationSpeed = 8f;
    public Color color1; // Colours to lerp between
    public Color color2;
	[HideInInspector]
	//public float rotationSpeed = -Mathf.PI / 2f;
	protected Vector3 origin;

    [Header("Task1")]
	public Vector3 pos1; // The starting position
	public Vector3 pos2; // The end position
	int direction = 1; // Direction the object is travelling (1 is towards pos2, -1 is towards pos1)
	public Vector3 myPos; // The current position of this object for reference


    protected virtual void Start() {
		gameObject.AddComponent<MeshRenderer> ();
		gameObject.AddComponent<MeshFilter> ();

		// Get the mesh and set the material
		mesh = GetComponent<MeshFilter> ().mesh;
		GetComponent<MeshRenderer> ().material = mat;

		// Initialise the properties for our custom transform
		scale = radius;
		Position = pos1;
		origin = new Vector3(pos1.x, 0, 0);

		DrawShape ();

        // set the collider radius
        GetComponent<CircleCollider2D>().radius = scale;

	}


	protected virtual void Update() {
		// Perform the movement of this object between the two specified points
		BetweenTwoPoints ();

		// Re-set the colours for the mesh
		SetMeshColours ();

		// Recalculate the bounds for the mesh
		mesh.RecalculateBounds ();

		// Show publicly the object's position
		myPos = Position;

        // Speed up and down the translate speed of all object using keyboard input
        GetComponent<CircleCollider2D>().offset = new Vector3(myPos.x - origin.x, myPos.y);
	}


	/// <summary>
	/// Draw a basic shape
	/// </summary>
	protected void DrawShape() {

		// Check if the number of segments is less that the minimum specified
		if (segments < minSegments)
			segments = minSegments;

		// Start Calculating the vertices
		Vector3[] verts = new Vector3[segments * 3];

		// The angle of each triangle at the centre
		float angle = 2 * Mathf.PI / (float)segments;

		// Initialise the vertices
		for (int i = 0; i < verts.Length; i += 3) {
			verts [i] = new Vector3(0, 0, 1f) * scale;
			verts [i + 1] = new Vector3 (Mathf.Cos (i / 3 * angle), Mathf.Sin (i / 3 * angle), 1f) * scale;
			verts [i + 2] = new Vector3 (Mathf.Cos (i/3 * angle + angle), Mathf.Sin (i/3 * angle + angle), 1f) * scale;

			// Translate position
			verts [i] += new Vector3 (0, Position.y, 1);
			verts [i + 1] += new Vector3 (0, Position.y, 1);
			verts [i + 2] += new Vector3 (0, Position.y, 1);
		}

		// Set up the triangles
		int[] tris = new int[verts.Length];

		for (int i = 0; i < tris.Length; i++) {
			tris [i] = i;
		}

		// Set the vertices for the mesh
		mesh.vertices = verts;

		// Set the Colours for the mesh
		SetMeshColours ();

		// Set the Triangles for the mesh
		mesh.triangles = tris;

		mesh.RecalculateBounds ();
	}


	/// <summary>
	/// Set the colours for the mesh based on the distance between the two points
	/// </summary>
	protected void SetMeshColours() {

		// Calculate the position percentage between the two points
		float p = Mathf.Clamp( Vector3.Distance(Position, pos1) / Vector3.Distance(pos1, pos2), 0f, 1f);

		// Set colours depending on their position
		Color[] colour = new Color[mesh.vertices.Length];

		// Add colours
		for (int i = 0; i < colour.Length; i++) {
            colour[i] = Color.Lerp(color1, color2, p);
		}

		// Hard set the first triangle to blue
		// This is to be able to see if the object is rotating, since our objects look circular
		colour [0] = Color.blue;
		colour [1] = Color.blue;
		colour [2] = Color.blue;

		// Set colours
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

		// Draw lines to visualise the positions
		Debug.DrawLine (pos1, pos1 + Vector3.right);
		Debug.DrawLine (pos2, pos2 + Vector3.right);

	}


	/// <summary>
	/// Transform towards v1 from v2
	/// </summary>
	/// <param name="v1">V1.</param>
	/// <param name="v2">V2.</param>
	/// <param name="dir">Direction to change when point is reached.</param>
	void DoDirectionalTransformation(Vector3 v1, Vector3 v2, int dir) {
		// Translate and Rotate the mesh
		mesh.vertices = TranslateRotate (rotationSpeed * Time.deltaTime, ((Vector2)(v1 - Position)).normalized * Time.deltaTime * translateSpeed, mesh, origin);

        // Check if our position has passed the point we are moving towards
        if (Vector3.Distance(v2, v1) <= Vector3.Distance(Position, v2)) {
			
			// Move the mesh back to v1 (the point we're moving towards
			mesh.vertices = TranslateRotate (rotationSpeed * Time.deltaTime, (Vector2)(v1 - Position), mesh, origin);
			direction = dir; // change direction
		}

		// Draw lines from both positions to the current position of the object
		Debug.DrawLine (v1, Position, Color.green);
		Debug.DrawLine (v2, Position, Color.red);
	}

    /// <summary>
    /// Speed up and down the translations.
    /// </summary>
    void OnMouseOver()
    {
		// Mouse left click
        if (Input.GetMouseButtonDown(0))
        {
			// increase translation speed if maximum has not been reached
            if (translateSpeed < maxTranslateSpeed) 
				translateSpeed += step;

			// increase rotation speed if maximum has not been reached
            if (rotationSpeed < maxRotationSpeed) 
				rotationSpeed += step;
        
		// Mouse right click
        } else if (Input.GetMouseButtonDown(1)) {

			// decrease translation speed, but check if speed is less than step
            if (translateSpeed < step)
                translateSpeed = 0;
            else
                translateSpeed -= step;

			// decrease rotation speed if it is greater than the -max
            if (rotationSpeed > -maxRotationSpeed) 
				rotationSpeed -= step;

		// Mouse middle click
        } else if (Input.GetMouseButtonDown(2)) {
			// Randomise Translation and Rotaion speeds
            translateSpeed = Random.Range(0, maxTranslateSpeed);
            rotationSpeed = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        }
    }
}
