﻿using UnityEngine;
using System.Collections;

public class EnemyFighter2Missile : MonoBehaviour {
	public GameObject missile;
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
