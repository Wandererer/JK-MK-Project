using UnityEngine;
using System.Collections;

public class Instantiate40Move : MonoBehaviour {


	float x,y,z;
	float moveSpeedZ=0.1f,moveSpeedX=0.2f;
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
		else if (x < -17)
			Destroy (this.gameObject);
		else if (z <= 17) {
			this.GetComponent<Transform> ().position = new Vector3 (x -= moveSpeedX, y, z);
			GetComponent<Transform> ().Rotate ((new Vector3 (0.2f, 0, 0)));
		}
		

	}
}
