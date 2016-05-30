using UnityEngine;
using System.Collections;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

public class MyShipControl : MonoBehaviour {
	public GameObject myo;
	public GameObject bomb;
	public GameObject controller;
    public GameObject DestroyParticle;

	float planeLocalX, planeLocalY,planeLocalZ;

	float transparentLocalX,transparentLocalY;
	float limitPlainX, limitPlainY; 
	float firerate=0.2f;

	bool isMyControl=true; //if exceed resolution ismycontorl=false;
    bool isParticle = false;

	float myoGryoX;
	float myoGryoY;

	public GameObject missile;
    public GameObject missile2;
	public GameObject transparentPlain;

    Transform startRotation;
    Transform destinationRotation;

	int bombCount;
    int direction;
    int power=1;
   
    [Range(0, 180)]
    public float angle;

    Transform test;

	// Use this for initialization
	void Start () {
		bombCount=controller.GetComponent<GameController>().getBombCount();
	}
	
	// Update is called once per frame
	void Update () {
		ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>(); //myo object에 접속
	

		 myoGryoX =thalmicMyo.gyroscope.x;
         myoGryoY= thalmicMyo.gyroscope.y;
        myoGryoX *= 0.009f;
        myoGryoY *= 0.009f;




		Vector3 screenPosition = Camera.main.WorldToViewportPoint (this.GetComponent<Transform> ().position); 

		if (isMyControl == true) {
			limitPlainX = screenPosition.x;
			limitPlainY = screenPosition.y;
		}

        planeLocalX = this.gameObject.GetComponent<Transform>().position.x;
        planeLocalY = this.gameObject.GetComponent<Transform>().position.y;
        planeLocalZ = this.gameObject.GetComponent<Transform>().position.z;


		firerate -= Time.deltaTime; //how often do you want fire pistol


        DontExceedCameraSight();
		MovelikeRealPlain ();


		//change transform this space ship

		if (thalmicMyo.pose == Pose.Fist) { //if your gesture is fist then fire
			if(firerate<=0 && power==1)
			{
				//add fire sound later
                FireMissileP1();
			}

            else if(firerate<=0 && power==2)
            {
                FireMissileP2();
            }
			
		}

		if (thalmicMyo.pose == Pose.WaveIn) {
			CreateBomb ();
		}


	}

	void CreateBomb()
	{
		GameObject obj = GameObject.Find ("Nuclear");
		if (obj == null && bombCount>0) {
			obj = Instantiate (bomb);
			obj.name = "Nuclear";
            obj.GetComponent<Transform>().position = new Vector3(planeLocalX, planeLocalY - 1, 0);
			controller.GetComponent<GameController> ().setBombCount (bombCount--);
		}
	}

    void FireMissileP1()
    {
        GameObject Missile = Instantiate(missile); //create missile gameobjet
        Missile.GetComponent<Transform>().position = new Vector3(planeLocalX, planeLocalY, planeLocalZ+1); //create missile object in front of z+1 from this object
        firerate = 0.2f; //firerate initiallization
    }
    void FireMissileP2()
    {
        GameObject Missile1 = Instantiate(missile2); //create missile gameobjet
        GameObject Missile2 = Instantiate(missile2); //create missile gameobjet
        Missile1.GetComponent<Transform>().position = new Vector3(planeLocalX - 0.5f, planeLocalY, planeLocalZ+1); //create missile object in front of z+1 from this object
        Missile2.GetComponent<Transform>().position = new Vector3(planeLocalX + 0.5f, planeLocalY, planeLocalZ + 1); //create missile object in front of z+1 from this object
        firerate = 0.2f; //firerate initiallization
    }

    void DontExceedCameraSight()
    {
        if (limitPlainX >= 0.05f && limitPlainX <= 0.95f && limitPlainY >= 0.05f && limitPlainY <= 0.95f)
            this.gameObject.GetComponent<Transform>().position = new Vector3(planeLocalX += (myoGryoY) * -1, planeLocalY += myoGryoX, 0);
        else
        {
            isMyControl = false;
            this.gameObject.GetComponent<Transform>().position = new Vector3(planeLocalX, planeLocalY, 0);
            AddTransparentShipObjectForPostionLimitExeed();
        }

    }

	void AddTransparentShipObjectForPostionLimitExeed()
	{
		float objLimitX, objLimitY;
		GameObject obj = GameObject.Find ("TransparentPlain");
		if (obj == null) {
			obj = Instantiate (transparentPlain);
            transparentLocalX = planeLocalX;
            transparentLocalY = planeLocalY;
			obj.GetComponent<Transform> ().position = new Vector3 (transparentLocalX, transparentLocalY, 0);
			obj.name = "TransparentPlain";

		} else {
			obj.GetComponent<Transform> ().position = new Vector3 (transparentLocalX += (myoGryoY) * -1, transparentLocalY += myoGryoX, 0);
			Vector3 screenPositionTransparent = Camera.main.WorldToViewportPoint (obj.GetComponent<Transform> ().position); 
			objLimitX = screenPositionTransparent.x;
			objLimitY = screenPositionTransparent.y;
			//Debug.Log (objLimitX + "   " + objLimitY);

			if (objLimitX >= 0.05f && objLimitX <= 0.95f && objLimitY >= 0.05f && objLimitY <= 0.95f) {
				//Debug.Log ("범위안에 들어와");
                planeLocalX = transparentLocalX;
                planeLocalY = transparentLocalY;
                this.gameObject.GetComponent<Transform>().position = new Vector3(planeLocalX += (myoGryoY) * -1, planeLocalY += myoGryoX, 0);
				//Debug.Log (limitPlainY + "  " + limitPlainY);

				isMyControl = true;
				Destroy (obj);
			}

		}
	}

	void MovelikeRealPlain()
	{
		//Debug.Log (myoGryoX * 1000);

		//Debug.Log (myoGryoY * 1000);
		if (myoGryoY * 1000 > 120) {//왼쪽


			GetComponent<Transform> ().Rotate ((new Vector3 (0, -1, 0)) * Time.deltaTime * 200f);
			

		} else if (myoGryoY * 1000 < -120) { //오른쪽
			

			GetComponent<Transform> ().Rotate ((new Vector3 (0, 1, 0)) * Time.deltaTime * 200f);
		
		} 

		if ((myoGryoY * 1000 <= 120 && myoGryoY * 1000 >= -120)) {//중앙  
			float limitX = this.GetComponent<Transform> ().rotation.eulerAngles.x;
			float limitY = this.GetComponent<Transform> ().rotation.eulerAngles.y;
			float limitZ = this.GetComponent<Transform> ().rotation.eulerAngles.z;

			limitY = Mathf.Round (limitY);
			limitZ = Mathf.Round (limitZ);
			limitX = Mathf.Round (limitX);

			if ( limitY==270&& limitZ==90 &&!(limitX>268 && limitX<272)) {

					GetComponent<Transform> ().Rotate ((new Vector3 (0, 1, 0)) * Time.deltaTime * 200f);
				
			}

			else if (limitZ ==270 && limitY==90&&!(limitX>268 && limitX<272)){

			
				GetComponent<Transform> ().Rotate ((new Vector3 (0, -1, 0)) * Time.deltaTime * 200f);
				
			}

		

		
		} 



	}

	void OnCollisionEnter (Collision col)
	{
        if (col.transform.tag == "EnemyMissile")
        {
            Destroy(this.gameObject);
            if (isParticle == false)
            {
                isParticle = true;
                GameObject particle = Instantiate(DestroyParticle);
                particle.GetComponent<Transform>().position =
                    new Vector3(this.GetComponent<Transform>().position.x,
                        this.GetComponent<Transform>().position.y,
                        this.GetComponent<Transform>().position.z);
            }
        }
        else if (col.transform.tag == "Bomb")
            bombCount++;
        else if (col.transform.tag == "Power")
            power++;
	}

}
