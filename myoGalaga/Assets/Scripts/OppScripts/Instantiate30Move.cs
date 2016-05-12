using UnityEngine;
using System.Collections;

public class Instantiate30Move : MonoBehaviour {

	float x,y,z;
	float moveSpeedZ=0.1f,moveSpeedY=0.2f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		x = this.GetComponent<Transform> ().position.x;
		y = this.GetComponent<Transform> ().position.y;
		z = this.GetComponent<Transform> ().position.z;

		if (z > 17)
			this.GetComponent<Transform> ().position = new Vector3 (x, y, z -= moveSpeedZ);
		else if (z <= 17 && y > 10)
			Destroy (this.gameObject);
		else if (z <= 17) {
			this.GetComponent<Transform> ().position = new Vector3 (x, y += moveSpeedY, z);
			GetComponent<Transform> ().Rotate ((new Vector3 (0, 0.1f, 0)));
		}

	}
}
