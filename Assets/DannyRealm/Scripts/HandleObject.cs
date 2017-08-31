using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleObject : RenderObject {

	public RenderObject follow;
	public int posNum = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (posNum == 1) {
			follow.pos1 = Position;

		} else if (posNum == 2) {
			follow.pos2 = Position;
		}
	}
}
