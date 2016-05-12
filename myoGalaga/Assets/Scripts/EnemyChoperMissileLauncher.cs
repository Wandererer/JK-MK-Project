using UnityEngine;
using System.Collections;

public class EnemyChoperMissileLauncher : MonoBehaviour {
	public GameObject missileLauncher;
	public GameObject homingMissile;
	public float distanceToHitPoint;
	bool isFire=false;
	bool isScriptCalled=false;
	private ChoperHommingMissile choperSciprts;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;

		if (Physics.Raycast (transform.position, new Vector3 (0, 0, -1), out hit)) {
			distanceToHitPoint = Vector3.Distance(transform.position, hit.point);
		}
		if (isFire == false) {
			
			isFire = true;
			GameObject missile = Instantiate (homingMissile);
			missile.GetComponent<Transform> ().position = new Vector3 (missileLauncher.GetComponent<Transform>().position.x,
				missileLauncher.GetComponent<Transform>().position.y,
				missileLauncher.GetComponent<Transform>().position.z);
			
			if (isScriptCalled == false) {
				choperSciprts = missile.GetComponent<ChoperHommingMissile> ();
				Debug.Log ("Called");
				choperSciprts.setIsFireTrue ();
				isScriptCalled = true;
			}
		}

	}
}
