using UnityEngine;
using System.Collections;

public class FireMyShipMissile : MonoBehaviour {

    public GameObject DestroyParticle;
	float fireSpeed=0.5f; //missile speed
    public int damage;
    bool isParticle = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float z = this.GetComponent<Transform> ().position.z; //get from this object transformation.z info
		
		this.GetComponent<Transform> ().position = new Vector3 (this.GetComponent<Transform> ().position.x, 
		                                                        this.GetComponent<Transform> ().position.y
		                                                        , z += fireSpeed);
		//this is moving script

        if (z >= 20)
        {
            Destroy(this.gameObject);//if z>50 destroy this object 
        }
	}

    void OnCollisionEnter(Collision col)
    {
    //    Debug.Log(col.transform.name);

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
