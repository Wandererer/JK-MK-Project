using UnityEngine;
using System.Collections;

public class Instantiate3MissileLauncher : MonoBehaviour {

	public GameObject missileLauncher1;
	public GameObject missileLauncher2;
	public GameObject missile;
	bool isFire1=false,isFire2=false,isFire3=false;
	float y;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		y = this.GetComponent<Transform> ().position.y;

		if (y > -4 && isFire1 == false) {
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

		if (y >0 && this.name == "helicopter17" && isFire2==false) {
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

		if (y > 4 && isFire3 == false) {
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
