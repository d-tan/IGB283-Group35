using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour {

	// Reference for holding object
	HandleObject heldObject;

	// Raycast
	RaycastHit2D hitInfo;
	
	// Update is called once per frame
	void Update () {

		// Mouse left click
		if (Input.GetMouseButtonDown(0)) {
			
			// Calculate mouse pos in world space
			Vector2 mousePos = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y, 11));

			// Get collider mouse is over
			Collider2D hitCollider = Physics2D.OverlapPoint (mousePos);

			// Check if a collider exists and tag
			if (hitCollider && hitCollider.transform.CompareTag ("Handle")) {

				// Store handle reference
				heldObject = hitCollider.GetComponent<HandleObject> ();
			}
		
		// Mouse left click release
		} else if (Input.GetMouseButtonUp(0)) {

			// Reset holding object to null
			heldObject = null;
		}

		// Check if our holding reference is not null
		if (heldObject) {

			// translate the handle to the mouse position in world space
			heldObject.DragObject (Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 11)));
		}
	}
}
