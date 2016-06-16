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
	void FixedUpdate()
	{

	}
	// Update is called once per frame
	void Update () {
		float z = this.GetComponent<Transform> ().position.z; //get from this object transformation.z info
		
		this.GetComponent<Transform> ().position = new Vector3 (this.GetComponent<Transform> ().position.x, 
		                                                        this.GetComponent<Transform> ().position.y
		                                                        , z += fireSpeed);
		//this is moving script
        if (GameObject.Find("GameController").GetComponent<GameController>().gameState == GameState.Boss)
        {

            if (z >= 70)
            {
                Destroy(this.gameObject);//if z>50 destroy this object 
            }
        }
        else
        {
            if (z >= 16)
            {
                Destroy(this.gameObject);//if z>50 destroy this object 
            }
        }
	}

    void OnCollisionEnter(Collision col)
    {
    //    Debug.Log(col.transform.name);
		if (col.transform.tag == "Enemy" )
			this.GetComponent<Rigidbody> ().isKinematic = true;

		if (col.transform.name == "beam") {
			this.GetComponent<Rigidbody> ().isKinematic = true;
			Destroy (this.gameObject);
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
