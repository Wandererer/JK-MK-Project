using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {


	public GameObject test2;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float x= this.GetComponent<Transform> ().position.x;
		float y= this.GetComponent<Transform> ().position.y;
		float z = this.GetComponent<Transform> ().position.z; //get from this object transformation.z info

		this.GetComponent<Transform> ().position = new Vector3 (this.GetComponent<Transform> ().position.x, 
			this.GetComponent<Transform> ().position.y
			, z-=10);

		if (z < -1000) {
			GameObject objectTest = GameObject.Find ("test");
			if (objectTest == null) {
				objectTest = Instantiate (test2);
				objectTest.name = "test";
				objectTest.GetComponent<Transform> ().position = new Vector3 (x, y, z + 2000);

			}
		

		}

		if (z < -1500)
			Destroy (this.gameObject);
	}
}
