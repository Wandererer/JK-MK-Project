using UnityEngine;
using System.Collections;

public class Instantiate140Move : MonoBehaviour {

	float x, y, z;
	float moveSpeedZ = 0.8f, moveSpeedX = 0.2f, moveSpeedY = 0.2f;
	bool isUp=true,isDown=false;
	
    int countEndPosition=0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		x = this.GetComponent<Transform>().position.x;
		y = this.GetComponent<Transform>().position.y;
		z = this.GetComponent<Transform>().position.z;

		if (y < -7) {
			isUp = true;
			isDown = false;
			countEndPosition++;
		}

		if (y > 7) {
			isUp = false;
			isDown = true;
			countEndPosition++;
		}

		if (z > 17)
			this.GetComponent<Transform> ().position = new Vector3 (x, y, z -= moveSpeedZ);
		else if (isUp == false && z <= 17) {
			this.GetComponent<Transform> ().position = new Vector3 (x, y-=moveSpeedY, z);
		} else if (isDown == false && z <= 17) {
			this.GetComponent<Transform> ().position = new Vector3 (x, y+=moveSpeedY, z);
		}


		if (countEndPosition == 8)
			Destroy (this.gameObject);

	}
}
