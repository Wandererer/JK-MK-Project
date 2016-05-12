using UnityEngine;
using System.Collections;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

public class MyShipControl : MonoBehaviour {
	public GameObject myo = null;
	public GameObject bomb=null;
	public GameObject controller;
	
	float plainLocalX, plainLocalY;
	float plainTransform;

	float transparentLocalX,transparentLocalY;
	float limitPlainX, limitPlainY; 
	float firerate=0.1f;

	bool isMyControl=true; //if exceed resolution ismycontorl=false;

	float myoGryoX;
	float myoGryoY;

	public GameObject missile;
	public GameObject transparentPlain;

    Transform startRotation;
    Transform destinationRotation;

	int bombCount;
    int direction;

   
    [Range(0, 180)]
    public float angle;

    Transform test;

	// Use this for initialization
	void Start () {
		//myo = GameObject.Find ("Myo");
		bombCount=controller.GetComponent<GameController>().getBombCount();
	}
	
	// Update is called once per frame
	void Update () {
		ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>(); //myo object에 접속
	

		 myoGryoX =thalmicMyo.gyroscope.x;
         myoGryoY= thalmicMyo.gyroscope.y;
		//increase rate for moving spaceship
        myoGryoX *= 0.009f;
        myoGryoY *= 0.009f;


	//Debug.Log (myoGryoX*1000 + "  " + myoGryoY*1000);
		//Debug.Log(myoAccelX +"   "+myoAccelY);
		//Debug.Log(myoAccelX);


		Vector3 screenPosition = Camera.main.WorldToViewportPoint (this.GetComponent<Transform> ().position); 
		//find screen limit
		if (isMyControl == true) {
			limitPlainX = screenPosition.x;
			limitPlainY = screenPosition.y;
		}

		//Debug.Log (screenPosition);

		// Debug.Log(thalmicMyo.gyroscope + " dd  ");
		// Debug.Log(newX + "   " + newY);
		//Debug.Log(thalmicMyo.gyroscope.x +"   "+thalmicMyo.gyroscope.y);
		
		//  Debug.Log(thalmicMyo.accelerometer.x);
		
		// this.gameObject.GetComponent<Transform>().position = (thalmicMyo.gyroscope/10);
		
		//plainTransform = this.gameObject.GetComponent<Transform> ().rotation;
		plainLocalX = this.gameObject.GetComponent<Transform>().position.x;
		plainLocalY = this.gameObject.GetComponent<Transform>().position.y;

     //   test = this.gameObject.GetComponent<Transform>();
  


	//	Debug.Log (plainQuaternionX +"  "+plainQuaternionY + "  " + plainQuaternionZ);
       // Debug.Log(Quaternion.Euler(270, 310, 90));


		//plainRotationX = this.gameObject.GetComponent<Transform> ().rotation.x;
		//plainRotationY = this.gameObject.GetComponent<Transform> ().eulerAngles.y;
		//plainRotationZ = this.gameObject.GetComponent<Transform> ().eulerAngles.z;

		firerate -= Time.deltaTime; //how often do you want fire pistol

		if (limitPlainX >= 0.05f && limitPlainX <= 0.95f && limitPlainY >= 0.05f && limitPlainY <= 0.95f)
			this.gameObject.GetComponent<Transform> ().position = new Vector3 (plainLocalX += (myoGryoY) * -1, plainLocalY += myoGryoX, 0);
		else 
		{
			isMyControl = false;
			this.gameObject.GetComponent<Transform> ().position = new Vector3 (plainLocalX, plainLocalY, 0);
			AddTransparentShipObjectForPostionLimitExeed ();
		}


		MovelikeRealPlain ();

		//Debug.Log (this.gameObject.GetComponent<Transform> ().position);

		//change transform this space ship

		if (thalmicMyo.pose == Pose.Fist) { //if your gesture is fist then fire
			if(firerate<=0)
			{
				//add fire sound later
				GameObject test=Instantiate(missile); //create missile gameobjet
				test.GetComponent<Transform>().position=new Vector3(plainLocalX,plainLocalY,1); //create missile object in front of z+1 from this object
				firerate=0.1f; //firerate initiallization
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
			obj.GetComponent<Transform>().position=new Vector3(plainLocalX,plainLocalY-1,0);
			controller.GetComponent<GameController> ().setBombCount (bombCount--);
		}
	}



	void AddTransparentShipObjectForPostionLimitExeed()
	{
		float objLimitX, objLimitY;
		GameObject obj = GameObject.Find ("TransparentPlain");
		if (obj == null) {
			obj = Instantiate (transparentPlain);
			transparentLocalX = plainLocalX;
			transparentLocalY = plainLocalY;
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
				plainLocalX = transparentLocalX;
				plainLocalY = transparentLocalY;
				this.gameObject.GetComponent<Transform> ().position = new Vector3 (plainLocalX += (myoGryoY) * -1, plainLocalY += myoGryoX, 0);
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




		/*
		 //버그그그버버그그   
		if (myoGryoX * 1000 < -120) {//아래  
			GetComponent<Transform> ().Rotate ((new Vector3 (1, 0, 0)) * Time.deltaTime * 200f);

		} 

		else if (myoGryoX * 1000 > 120) { //위로  
			GetComponent<Transform> ().Rotate ((new Vector3 (-1, 0, 0)) * Time.deltaTime * 200f);
		}



		if (myoGryoX * 1000 >= -120 && myoGryoX*1000 <= 120) {
			float limitX = this.GetComponent<Transform> ().rotation.eulerAngles.x;
			float limitY = this.GetComponent<Transform> ().rotation.eulerAngles.y;
			float limitZ = this.GetComponent<Transform> ().rotation.eulerAngles.z;

			limitY = Mathf.Round (limitY);
			limitZ = Mathf.Round (limitZ);
			limitX = Mathf.Round (limitX);
		
			if (limitX > 270 && limitY==180 && limitZ==180) {
				GetComponent<Transform> ().Rotate ((new Vector3 (1, 0, 0)) * Time.deltaTime * 200f);
			}
			else
				GetComponent<Transform> ().Rotate ((new Vector3 (-1, 0, 0)) * Time.deltaTime * 200f);
		}

*/

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




		/*
		 else{
			this.GetComponent<Transform> ().rotation = new Quaternion (180, 0 , 0 , -180);
		}


if (myoGryoX * 1000 > 120) {
			this.GetComponent<Transform> ().rotation = new Quaternion (270, 0, 0, -180);
		} else if(myoGryoX * 1000 < -120) {
			this.GetComponent<Transform> ().rotation = new Quaternion (90, 0, 0, -180);

		}
		 
		 */ 
	}


	/*
	 Add later
	void OnCollisionEnter (Collision col)
	{
		if (col.transform.tag == "EnemyMissile")
			Destroy (this.gameObject);
	}
*/
}
