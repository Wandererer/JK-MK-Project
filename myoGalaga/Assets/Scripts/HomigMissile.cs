using UnityEngine;
using System.Collections;

public class HomigMissile : MonoBehaviour {

	Rigidbody homingMissile;
	float missileVelocity=20f;
	Transform target;
	bool isHoming=true;

	// Use this for initialization
	void Start () {
		homingMissile = this.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		float x = transform.position.x;
		float y = transform.position.y;
		float z = transform.position.z;

		target = GameObject.FindGameObjectWithTag ("My").GetComponent<Transform>();
		float diff = (target.position - transform.position).sqrMagnitude;
		Debug.Log (diff);
		if (isHoming) {
			homingMissile.velocity = transform.forward * missileVelocity;
			var targetRotation = Quaternion.LookRotation (target.position - transform.position);
			homingMissile.MoveRotation (Quaternion.RotateTowards (transform.rotation, targetRotation, 20));
		}
		else
			transform.position = new Vector3 (x, y, z -= 1);

		if(diff<50)  {
			isHoming = false;
		}

		if (z < -10)
			Destroy (this.gameObject);
	
	}
}
