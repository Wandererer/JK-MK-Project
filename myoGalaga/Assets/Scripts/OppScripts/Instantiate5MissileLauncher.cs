using UnityEngine;
using System.Collections;

public class Instantiate5MissileLauncher : MonoBehaviour {
	public GameObject missileLauncher1;
	public GameObject homingissile;
	bool isFire1 = false;
	float z;
	bool isScriptCalled=false;
	private ChoperHommingMissile choperSciprts;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		z = this.GetComponent<Transform> ().position.z;

		if (z < 17) {
			if (isFire1 == false) {
				isFire1 = true;
			

				GameObject missile = homingissile;

				Instantiate (missile,transform.position,new Quaternion(0,0,180,0));

				if (isScriptCalled == false) {
					choperSciprts = missile.GetComponent<ChoperHommingMissile> ();
				//	Debug.Log ("Called");
					choperSciprts.setIsFireTrue ();
					isScriptCalled = true;
				}
			}
		}
	
	}
}
