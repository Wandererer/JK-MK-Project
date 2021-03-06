﻿using UnityEngine;
using System.Collections;

public class Instantiate10Move : MonoBehaviour {
	float x,y,z;
	float moveSpeedZ=0.1f,moveSpeedY=0.1f,moveSpeedX=0.2f;

	// Use this for initialization
	void Start () {

	}

    void FixedUpdate() { }

	// Update is called once per frame
	void Update () {
		
		x = this.GetComponent<Transform> ().position.x;
		y = this.GetComponent<Transform> ().position.y;
		z = this.GetComponent<Transform> ().position.z;

		if (z > 17)
			this.GetComponent<Transform> ().position = new Vector3 (x, y, z -= moveSpeedZ);
		else if (x <= -16 && y <= -8)
			this.GetComponent<Transform> ().position = new Vector3 (x, y, z -= moveSpeedZ);
		else if (z <= 17) {
			this.GetComponent<Transform> ().position = new Vector3 (x -= moveSpeedX, y -= moveSpeedY, z -= moveSpeedZ);
			GetComponent<Transform> ().Rotate ((new Vector3 (0.1f, 0.1f, 0)));
		}
		if (z < -5) {
			Destroy (this.gameObject);
		}
	}
}
