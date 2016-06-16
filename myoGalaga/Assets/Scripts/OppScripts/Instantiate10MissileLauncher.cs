using UnityEngine;
using System.Collections;

public class Instantiate10MissileLauncher : MonoBehaviour {
	public GameObject missileLauncher1;
	public GameObject homingissile;
	bool isFire1 = false;
	float y;
	bool isScriptCalled = false;
	private ChoperHommingMissile choperSciprts;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		y = this.GetComponent<Transform>().position.y;

		if (y < 0.7 && isFire1 == false)
		{
			isFire1 = true;


			GameObject missile = homingissile;
			missile.GetComponent<Transform>().position = new Vector3(missileLauncher1.GetComponent<Transform>().position.x,
				missileLauncher1.GetComponent<Transform>().position.y,
				missileLauncher1.GetComponent<Transform>().position.z);
			Instantiate(missile);

			if (isScriptCalled == false)
			{
				choperSciprts = missile.GetComponent<ChoperHommingMissile>();
				//	Debug.Log ("Called");
				choperSciprts.setIsFireTrue();
				isScriptCalled = true;
			}
		}
	}
}
