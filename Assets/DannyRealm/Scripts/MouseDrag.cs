using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour {

	HandleObject heldObject;

	// Raycast
	RaycastHit2D hitInfo;
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0)) {
			Vector2 mousePos = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y, 11));

			Collider2D hitCollider = Physics2D.OverlapPoint (mousePos);

			if (hitCollider && hitCollider.transform.CompareTag ("Handle")) {

				Debug.Log ("Mouse pos: " + mousePos + " collider: " + hitCollider.transform.position);

				heldObject = hitCollider.GetComponent<HandleObject> ();
			}
		} else if (Input.GetMouseButtonUp(0)) {
			heldObject = null;
		}

		if (heldObject) {
			heldObject.DragObject (Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 11)));
		}
	}
}
