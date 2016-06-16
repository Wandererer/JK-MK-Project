using UnityEngine;
using System.Collections;

public class SemiBoss1MissileLauncher : MonoBehaviour {

	public GameObject Missile;
	public GameObject MissileLauncher1;
	public GameObject MissileLauncher2;
	public GameObject HomingMissile;


	bool isFire=true,isScriptCalled=false;
	public bool isFireMiddleFirst=false;
	public bool isFireMiddle=false;
	public bool isInstantiate=false;
	public float distanceOnce;
	public float distanceContinue;
	float waitSeconds;


	// Use this for initialization
	void Start () {
	
	}
    void FixedUpdate() { }
	// Update is called once per frame
	void Update () {
		waitSeconds = this.GetComponent<Transform> ().GetComponent<SemiBoss1Move> ().wait1Seconds;
		distanceOnce = this.GetComponent<SemiBoss1Move> ().distanceOnce;
		distanceContinue = this.GetComponent<SemiBoss1Move> ().distanceContinue;

		//Debug.Log (distanceContinue + "sdfsadf");

		if (waitSeconds < 0.2)
			FireHomming ();

		if ((distanceOnce * 0.9) > distanceContinue && (distanceOnce * 0.7)<distanceContinue && isFireMiddleFirst==false ) {
			isInstantiate = false;
			isFireMiddleFirst = true;
			Fire ();
			isInstantiate = false;
			//Debug.Log ("Firerwerfas");
		}

		else if ((distanceOnce * 0.5) > distanceContinue && (distanceOnce * 0.3)<distanceContinue &&isFireMiddle==false) {
			isFireMiddle = true;
			isInstantiate = false;
			Fire ();
			isInstantiate = false;
			//Debug.Log ("Firerwerfas");
		}
	}

	void FireHomming()
	{
		if (isFire == true) {
			isFire = false;
			if (isInstantiate == false) {
				isInstantiate = true;

				GameObject fire1 = Instantiate (HomingMissile);
				GameObject fire2 =Instantiate (HomingMissile);

			


				fire1.GetComponent<Transform> ().position = new Vector3 (
					MissileLauncher1.GetComponent<Transform> ().position.x,
					MissileLauncher1.GetComponent<Transform> ().position.y,
					MissileLauncher1.GetComponent<Transform> ().position.z);

				fire2.GetComponent<Transform> ().position = new Vector3 (
					MissileLauncher2.GetComponent<Transform> ().position.x,
					MissileLauncher2.GetComponent<Transform> ().position.y,
					MissileLauncher2.GetComponent<Transform> ().position.z);
				

				if (isScriptCalled == false)
				{
					fire1.GetComponent<ChoperHommingMissile> ().setIsFireTrue ();
					fire2.GetComponent<ChoperHommingMissile> ().setIsFireTrue ();

					isScriptCalled = true;
				}

			}

			isFire = true;

		}
	}

	void Fire()
	{
		if (isFire == true) {
			isFire = false;
			if (isInstantiate == false) {
				isInstantiate = true;
				GameObject missile1 = Instantiate (Missile);
				GameObject missile2 = Instantiate (Missile);

				missile1.GetComponent<Transform> ().position = new Vector3 (
					MissileLauncher1.GetComponent<Transform> ().position.x,
					MissileLauncher1.GetComponent<Transform> ().position.y,
					MissileLauncher1.GetComponent<Transform> ().position.z);

				missile2.GetComponent<Transform> ().position = new Vector3 (
					MissileLauncher2.GetComponent<Transform> ().position.x,
					MissileLauncher2.GetComponent<Transform> ().position.y,
					MissileLauncher2.GetComponent<Transform> ().position.z);
			
			}
			isFire = true;
		}
	}
}
