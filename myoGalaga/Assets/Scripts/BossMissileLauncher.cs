using UnityEngine;
using System.Collections;

public class BossMissileLauncher : MonoBehaviour {

	public GameObject simpleMissileLauncher1;
	public GameObject simpleMissileLauncher2;

	public GameObject simpleMissile;
	public GameObject homingMissile;

	public GameObject[] hommingMissileLauncherMatrix;

	bool isFireMissile=false;
	bool isFireLaser=false;
	bool isHomingFire=true;

	float firerate=0.5f;
	float homingFireRate=0.5f;

	int homingCount=0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		firerate -= Time.deltaTime;

		if (isFireMissile == true) {
			if (firerate < 0f) {
				GameObject simpleFire1 = Instantiate (simpleMissile);
				simpleFire1.GetComponent<Transform> ().position = new Vector3 (
					simpleMissileLauncher1.GetComponent<Transform>().position.x,
					simpleMissileLauncher1.GetComponent<Transform>().position.y,
					simpleMissileLauncher1.GetComponent<Transform>().position.z
				);

				GameObject simpleFire2 = Instantiate (simpleMissile);
				simpleFire2.GetComponent<Transform> ().position = new Vector3 (
					simpleMissileLauncher2.GetComponent<Transform>().position.x,
					simpleMissileLauncher2.GetComponent<Transform>().position.y,
					simpleMissileLauncher2.GetComponent<Transform>().position.z
				);

				firerate = 0.5f;
			}
		}


		if (isHomingFire == true)
		{
			homingFireRate -= Time.deltaTime;

			if (homingFireRate < 0f) 
			{
				
					GameObject homingFire = Instantiate (homingMissile);
					homingFire.GetComponent<Transform> ().position = new Vector3 (
					hommingMissileLauncherMatrix [homingCount].GetComponent<Transform> ().position.x,
					hommingMissileLauncherMatrix [homingCount].GetComponent<Transform> ().position.y,
					hommingMissileLauncherMatrix [homingCount].GetComponent<Transform> ().position.z);
				homingCount++;
				homingFireRate = 0.5f;
			}
				

				
			if(homingCount==8)

			{

				homingCount = 0;
				isHomingFire = false;

			}

		}

	}

}

