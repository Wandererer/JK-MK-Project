using UnityEngine;
using System.Collections;

public class testMissile : MonoBehaviour {

	float fireSpeed=0.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float z = this.GetComponent<Transform> ().position.z;

		this.GetComponent<Transform> ().position = new Vector3 (this.GetComponent<Transform> ().position.x, 
		                                                        this.GetComponent<Transform> ().position.y
		                                                        , z += fireSpeed);
	
	
		if (z >= 50)
			Destroy (this.gameObject);
	}


}
