using UnityEngine;
using System.Collections;

public class ThickLaserScript : MonoBehaviour {
	GameObject beamHitParticles;
	ParticleSystem bhp;

	public float beamRoationSpeed = 400.0f;
	public float beamExtendSpeed = 10.0f;
	public float zScaleFactor = 20.0f;
	public float distanceToHitPoint;

	// Use this for initialization
	void Start () {
		beamHitParticles = transform.parent.GetChild(1).gameObject;
		bhp = beamHitParticles.GetComponent<ParticleSystem>();
	}

	// Update is called once per frame
	void Update () {
		RaycastHit hit;

		if(Physics.Raycast(transform.position,new Vector3(0,0,-1),out hit))
		{


			distanceToHitPoint = Vector3.Distance(transform.position, hit.point);

			transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(20 , 20, (distanceToHitPoint * zScaleFactor)),
				(beamExtendSpeed * Time.deltaTime));

			beamHitParticles.transform.position = hit.point;
		}

		else
		{
			bhp.Stop();
			transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(20, 20, transform.localScale.z),
				(beamExtendSpeed * Time.deltaTime));
		}

		transform.Rotate(0, 0, (Time.deltaTime * beamRoationSpeed));
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log("파티클시작");
		bhp.Play();
	}

}
