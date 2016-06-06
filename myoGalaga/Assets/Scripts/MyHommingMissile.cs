using UnityEngine;
using System.Collections;

public class MyHommingMissile : MonoBehaviour {

    public GameObject DestroyParticle;
    Rigidbody homingMissile;
    Transform target;
    GameObject[] enemyTargets;


    bool isParticle = false;

   public int damage = 1;
    float missileVelocity = 40;
    float z;

    bool isHomming=true;

	// Use this for initialization
	void Start () {
        homingMissile = this.GetComponent<Rigidbody>();
         enemyTargets = GameObject.FindGameObjectsWithTag("Enemy");
  
	}
	
	// Update is called once per frame
	void Update () {
     z = this.GetComponent<Transform>().position.z;

        try
        {
            target = enemyTargets[0].GetComponent<Transform>();

            if (isHomming)
            {
                transform.LookAt(target.transform);
                homingMissile.velocity = transform.forward * missileVelocity;
                var targetRotation = Quaternion.LookRotation((transform.position - target.position));


                homingMissile.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, 100));

            }

        }
        catch(System.Exception e)
        {
            Destroy(this.gameObject);
        }


    

	}

    void OnCollisionEnter(Collision col)
    {


        //    Debug.Log(col.transform.name);
        if (col.transform.tag == "Enemy")
        {
            this.GetComponent<Rigidbody>().isKinematic = true;
           Destroy(this.gameObject);
        }

        else if (col.transform.name == "beam")
        {
            this.GetComponent<Rigidbody>().isKinematic = true;
            Destroy(this.gameObject);
        }

        if (isParticle == false)
        {
            isParticle = true;
            GameObject particle = Instantiate(DestroyParticle);
            particle.GetComponent<Transform>().position =
                new Vector3(this.GetComponent<Transform>().position.x,
                    this.GetComponent<Transform>().position.y,
                    this.GetComponent<Transform>().position.z);
            Destroy(this.gameObject);
        }
    }
}
