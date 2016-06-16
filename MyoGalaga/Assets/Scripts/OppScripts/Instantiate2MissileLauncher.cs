using UnityEngine;
using System.Collections;

public class Instantiate2MissileLauncher : MonoBehaviour {

	public GameObject missileLauncher1;
	public GameObject missileLauncher2;
	public GameObject missile;
	bool isFire1=false,isFire2=false,isFire3=false;
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

		if (distanceToHitPoint <= 214 && isFire1 == false) {
			isFire1 = true;
			GameObject fire1 = Instantiate (missile);
			GameObject fire2 = Instantiate (missile);

			fire1.GetComponent<Transform> ().position = new Vector3 (missileLauncher1.GetComponent<Transform> ().position.x,
				missileLauncher1.GetComponent<Transform> ().position.y,
				missileLauncher1.GetComponent<Transform> ().position.z
			);
			fire2.GetComponent<Transform> ().position = new Vector3 (missileLauncher2.GetComponent<Transform> ().position.x,
				missileLauncher2.GetComponent<Transform> ().position.y,
				missileLauncher2.GetComponent<Transform> ().position.z
			);


		}

		if (distanceToHitPoint <= 211.5 && isFire2 == false) {
			isFire2 = true;
			GameObject fire1 = Instantiate (missile);
			GameObject fire2 = Instantiate (missile);

			fire1.GetComponent<Transform> ().position = new Vector3 (missileLauncher1.GetComponent<Transform> ().position.x,
				missileLauncher1.GetComponent<Transform> ().position.y,
				missileLauncher1.GetComponent<Transform> ().position.z
			);
			fire2.GetComponent<Transform> ().position = new Vector3 (missileLauncher2.GetComponent<Transform> ().position.x,
				missileLauncher2.GetComponent<Transform> ().position.y,
				missileLauncher2.GetComponent<Transform> ().position.z
			);

		}


		if (distanceToHitPoint <= 208 && isFire3 == false) {
			isFire3 = true;
			GameObject fire1 = Instantiate (missile);
			GameObject fire2 = Instantiate (missile);

			fire1.GetComponent<Transform> ().position = new Vector3 (missileLauncher1.GetComponent<Transform> ().position.x,
				missileLauncher1.GetComponent<Transform> ().position.y,
				missileLauncher1.GetComponent<Transform> ().position.z
			);
			fire2.GetComponent<Transform> ().position = new Vector3 (missileLauncher2.GetComponent<Transform> ().position.x,
				missileLauncher2.GetComponent<Transform> ().position.y,
				missileLauncher2.GetComponent<Transform> ().position.z
			);

		}

	}
}
