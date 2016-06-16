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
    void FixedUpdate() { }
	// Update is called once per frame
	void Update () {
		float x = transform.position.x;
		float y = transform.position.y;
		float z = transform.position.z;

        float rotationX = this.GetComponent<Transform>().rotation.eulerAngles.x;
		float rotationY = this.GetComponent<Transform> ().rotation.eulerAngles.y;
		float ratationZ = this.GetComponent<Transform> ().rotation.eulerAngles.z;


		if(z<-6)
			GetComponent<Rigidbody>().AddForce(this.GetComponent<Transform>().position, ForceMode.Force);
			//GetComponent<Transform>().position=new Vector3(x,y,z-=0.2f);
        try {
            target = GameObject.FindGameObjectWithTag("My").GetComponent<Transform>();
            float diff= (target.position - transform.position).sqrMagnitude;
			//Debug.Log(diff);

           if (isHoming)
           {
               transform.LookAt(target.transform);
               homingMissile.velocity = transform.forward * missileVelocity;
				var targetRotation = Quaternion.LookRotation((transform.position-target.position  ) ) ;
			

               homingMissile.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation,0));
			
           }
           else
              GetComponent<Rigidbody>().AddForce(this.GetComponent<Transform>().position, ForceMode.Force);
				//GetComponent<Transform>().position=new Vector3(x,y,z-=0.2f);
			if (diff < 15)
			{
				isHoming = false;
			}

         
        }
        catch(System.NullReferenceException e)
        {
            Destroy(this.gameObject);
        }



		//Debug.Log (diff);
        if (z < -8)
            Destroy(this.gameObject);
	
	}


	void OnCollisionEnter (Collision col)
	{
		if (col.transform.tag == "My")
		{
			Destroy (this.gameObject);
		}

	}
}
