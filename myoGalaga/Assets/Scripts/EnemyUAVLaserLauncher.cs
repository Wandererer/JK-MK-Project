using UnityEngine;
using System.Collections;

public class EnemyUAVLaserLauncher : MonoBehaviour {

	public GameObject LaserLauner;
	public GameObject LaserObject;
	public float DistanceToHitPoint;
	bool isInstantiateLaser=false;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;

		if (Physics.Raycast (transform.position, new Vector3 (0, 0, -1), out hit)) {
			DistanceToHitPoint = Vector3.Distance(transform.position, hit.point);
		}

		if (DistanceToHitPoint < 220) {
			if (isInstantiateLaser == false) {
				isInstantiateLaser = true;
				GameObject laser = Instantiate (LaserObject);
				laser.GetComponent<Transform> ().position = new Vector3 (LaserLauner.GetComponent<Transform> ().position.x,
					LaserLauner.GetComponent<Transform> ().position.y,
					LaserLauner.GetComponent<Transform> ().position.z);

				laser.GetComponent<Transform> ().parent = LaserLauner.GetComponent<Transform> ();
			}
		}
	}
}
