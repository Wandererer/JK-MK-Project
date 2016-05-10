using UnityEngine;
using System.Collections;

public class EnemyHellicopterMissileLauncher : MonoBehaviour {

	public GameObject missileLauncher1;
	public GameObject missileLauncher2;
	public GameObject missile;
	bool isFire1=false,isFire2=false;
	public float distanceToHitPoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;

		if (Physics.Raycast (transform.position, new Vector3 (0, 0, -1), out hit)) {
			distanceToHitPoint = Vector3.Distance(transform.position, hit.point);
		}





	}
}
