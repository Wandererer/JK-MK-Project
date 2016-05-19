using UnityEngine;
using System.Collections;

public class Instantiate150Move : MonoBehaviour {
	float x, y, z;
	float moveSpeedZ = 0.8f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		x = this.GetComponent<Transform>().position.x;
		y = this.GetComponent<Transform>().position.y;
		z = this.GetComponent<Transform>().position.z;

		if (z > 20)
			this.GetComponent<Transform> ().position = new Vector3 (x, y, z -= moveSpeedZ);
	}
}
