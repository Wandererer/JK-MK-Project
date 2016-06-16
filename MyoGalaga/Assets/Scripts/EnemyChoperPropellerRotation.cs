using UnityEngine;
using System.Collections;

public class EnemyChoperPropellerRotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Transform> ().Rotate ( (new Vector3 (0, 0, 1)), Time.deltaTime*1500f);
	}
}
