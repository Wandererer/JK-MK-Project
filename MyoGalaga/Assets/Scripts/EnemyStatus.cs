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
   // Debug.Log(hp);

	    if(hp<=0 && isParticle==false)
        {
            isParticle = true;
			DestroyGameObject();
            controller.GetComponent<GameController>().score += score;

        }
	}

    void DestroyGameObject()
    {
        if (this.transform.name== "SemiBoss1")
        {
            GameObject Poweritem = this.GetComponent<SemiBoss1Move>().PowerItem;
            GameObject.Find("GameController").GetComponent<GameController>().isSemiBoss1Die = true;
            Instantiate(Poweritem);
            Poweritem.name = "Power";
            Poweritem.GetComponent<Transform>().position = new Vector3(
                Random.Range(-6, 6), Random.Range(-2, 5), -3
            );   
        }
        else if (this.transform.name == "SemiBoss2")
        {

            GameObject Poweritem = this.GetComponent<SemiBoss2Move>().PowerItem;
            controller.GetComponent<GameController>().isSemiBoss2Die = true;
            Instantiate(Poweritem);
            Poweritem.name = "Power";
            Poweritem.GetComponent<Transform>().position = new Vector3(
                Random.Range(-6, 6), Random.Range(-2, 5), -3
            );

            GameObject BombItem = this.GetComponent<SemiBoss2Move>().BombItem;
            Instantiate(BombItem);
            BombItem.name = "Bomb";
            BombItem.GetComponent<Transform>().position = new Vector3(
                Random.Range(-6, 6), Random.Range(-2, 5), -3
            );
        }
        else if(this.transform.name=="Boss")
        {
            GameObject.Find("GameController").GetComponent<GameController>().gameState = GameState.Win;
        }

        GameObject particle = Instantiate(particleObject);
        particle.GetComponent<Transform>().position = new Vector3(this.gameObject.GetComponent<Transform>().position.x,
            this.gameObject.GetComponent<Transform>().position.y,
            this.gameObject.GetComponent<Transform>().position.z);
        Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision col)
    {
		try{
            if (col.transform.tag == "myMissile" || col.transform.tag == "myHomming")
            {
                this.GetComponent<Rigidbody>().isKinematic = true;

                if (col.transform.tag == "myMissile")
                         hp -= col.gameObject.GetComponent<FireMyShipMissile>().damage;
                else if (col.transform.tag == "myHomming")
                {
                    hp -= col.gameObject.GetComponent<MyHommingMissile>().damage;
                }


	
			}
	//		else
			//	this.GetComponent<Rigidbody>().isKinematic=false;
		}
		catch(System.NullReferenceException e) {
			//do Nothing
		}
    }//
}
