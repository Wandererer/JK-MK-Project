﻿using UnityEngine;
using System.Collections;

public class ChoperHommingMissile : MonoBehaviour {

	Rigidbody homingMissile;
	float missileVelocity=20f;
	Transform target;
	Vector3 targetPosition;
	bool isHoming=false,isStartHoming=false;
	public bool isLock=false;
	float z;
	float homingStartTime=0.3f;

	// Use this for initialization
	void Start () {
		homingMissile = this.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		homingStartTime -= Time.deltaTime;

		//if(homingStartTime>0f)
			//GetComponent<Rigidbody> ().AddForce (this.GetComponent<Transform> ().position,ForceMode.Acceleration);

		z = this.GetComponent<Transform> ().position.z;

		if (isStartHoming == false) {
			if (isLock == true) {
				target = GameObject.FindGameObjectWithTag ("My").GetComponent<Transform> ();
				targetPosition = target.position;
				//Debug.Log ("target Lock");
				isLock = false;
				isHoming = true;
				isStartHoming = true;
			}
		}
	
	//	Debug.Log (targetPosition + " sdfasdfsadf");
		if (isStartHoming == true) {
			if (z > 2.5) {
				float diff = (targetPosition - transform.position).sqrMagnitude;
				//Debug.Log (diff);
				if (isHoming) {
					homingMissile.velocity = transform.forward * missileVelocity;
					var targetRotation = Quaternion.LookRotation (targetPosition - transform.position);
					homingMissile.MoveRotation (Quaternion.RotateTowards (transform.rotation, targetRotation, 20));
				}
			} else {
				GetComponent<Rigidbody> ().AddForce (this.GetComponent<Transform> ().position, ForceMode.Force);
			}
		}

        if (z < -10)
            Destroy(this.gameObject);

	}

	public void setIsFireTrue()
	{
		isLock = true;
	}
}
