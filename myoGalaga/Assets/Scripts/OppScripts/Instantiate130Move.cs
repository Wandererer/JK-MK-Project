using UnityEngine;
using System.Collections;

public class Instantiate130Move : MonoBehaviour {
	float x, y, z;
	float moveSpeedZ = 0.8f, moveSpeedX = 0.2f, moveSpeedY = 0.2f;
	bool isRight=true,isLeft=false;
	int countEndPosition=0;
	// Use this for initialization
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		x = this.GetComponent<Transform>().position.x;
		y = this.GetComponent<Transform>().position.y;
		z = this.GetComponent<Transform>().position.z;

		if (x < -14) {
			isLeft = true;
			isRight = false;
			countEndPosition++;
		}

		if (x > 14) {
			isLeft = false;
			isRight = true;
			countEndPosition++;
		}

		if (z > 17)
			this.GetComponent<Transform> ().position = new Vector3 (x, y, z -= moveSpeedZ);
		else if (isLeft == false && z <= 17) {
			this.GetComponent<Transform> ().position = new Vector3 (x -= moveSpeedX, y, z);
		} else if (isRight == false && z <= 17) {
			this.GetComponent<Transform> ().position = new Vector3 (x += moveSpeedX, y, z);
		}


		if (countEndPosition == 10)
			Destroy (this.gameObject);
	}
}
