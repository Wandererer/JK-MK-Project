using UnityEngine;
using System.Collections;

public class HomigMissile : MonoBehaviour {

	Rigidbody homingMissile;
	float missileVelocity=20f;
	Transform target;
	bool isHoming=true;
    
	// Use this for initialization
	void Start () {
        homingMissile = this.GetComponent<Rigidbody>();
     
	}
	
	// Update is called once per frame
	void Update () {
		float x = transform.position.x;
		float y = transform.position.y;
		float z = transform.position.z;

        float limitX = this.GetComponent<Transform>().rotation.eulerAngles.x;
        Debug.Log(limitX);

        this.GetComponent<Transform>().rotation = new Quaternion(-180, 0, 0, 0);
        Debug.Log(limitX);

        try {
            target = GameObject.FindGameObjectWithTag("My").GetComponent<Transform>();
            float diff= (target.position - transform.position).sqrMagnitude;
           if (isHoming)
           {
               transform.LookAt(target.transform);
               homingMissile.velocity = transform.forward * missileVelocity;
               var targetRotation = Quaternion.LookRotation((target.position - transform.position) ) ;

               homingMissile.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation,100));


           }
           else
               GetComponent<Rigidbody>().AddForce(this.GetComponent<Transform>().position, ForceMode.Force);

           if (diff < 5)
           {
               isHoming = false;
           }

          
        }
        catch(System.NullReferenceException e)
        {
            Destroy(this.gameObject);
        }

		//Debug.Log (diff);
        if (z < -10)
            Destroy(this.gameObject);
	
	}
}
