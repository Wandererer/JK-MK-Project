using UnityEngine;
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
	float rotationY,rotationZ;

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
                try
                {
                    target = GameObject.FindGameObjectWithTag("My").GetComponent<Transform>();
                }
                catch (System.NullReferenceException e)
                {
					Destroy (this.gameObject);
                }
                if (target != null)
                {
                    targetPosition = target.position;
                    //Debug.Log ("target Lock");
                    isLock = false;
                    isHoming = true;
                    isStartHoming = true;
                }
			}
		}
	
	//	Debug.Log (targetPosition + " sdfasdfsadf");
		else {
			if (z >5) {
				float diff = (targetPosition - transform.position).sqrMagnitude;
				//Debug.Log (diff);
				if (isHoming) {
					homingMissile.velocity = transform.forward * missileVelocity;
					var targetRotation = Quaternion.LookRotation (targetPosition - transform.position);
					homingMissile.MoveRotation (Quaternion.RotateTowards (transform.rotation, targetRotation, 20));
					//transform.rotation=Quaternion.Slerp(transform.rotation,target.rotation,10);
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


	void OnCollisionEnter (Collision col)
	{
		if (col.transform.tag == "My")
		{
			Destroy (this.gameObject);
		}

	}
}
