using UnityEngine;
using System.Collections;

public class EnemyStatus : MonoBehaviour {

    public int hp;
    public int score;
    public GameObject particleObject;


    GameObject controller;

    bool isParticle = false;

	// Use this for initialization
	void Start () {
        controller = GameObject.Find("GameController");
	}
	
	// Update is called once per frame
	void Update () {
     Debug.Log(hp);

	    if(hp<=0 && isParticle==false)
        {
            isParticle = true;
			DestroyGameObject();
            controller.GetComponent<GameController>().score += score;

        }
	}

    void DestroyGameObject()
    {
        GameObject particle = Instantiate(particleObject);
        particle.GetComponent<Transform>().position = new Vector3(this.gameObject.GetComponent<Transform>().position.x,
            this.gameObject.GetComponent<Transform>().position.y,
            this.gameObject.GetComponent<Transform>().position.z);
        Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision col)
    {
		try{
			if (col.transform.tag == "myMissile") {
            hp -= col.gameObject.GetComponent<FireMyShipMissile>().damage;
				this.GetComponent<Rigidbody>().isKinematic=true;
			}
			else
				this.GetComponent<Rigidbody>().isKinematic=false;
		}
		catch(System.NullReferenceException e) {
			//do Nothing
		}
    }//
}
