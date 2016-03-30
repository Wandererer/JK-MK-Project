using UnityEngine;
using System.Collections;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

public class MyShipControl : MonoBehaviour {
	public GameObject myo = null;
	
	float plainLocalX, plainLocalY;
	float transparentLocalX,transparentLocalY;
	float limitPlainX, limitPlainY; 
	float firerate=0.1f;

	bool isMyControl=true; //if exceed resolution ismycontorl=false;

	float myoGryoX;
	float myoGryoY;
	public GameObject missile;
	public GameObject transparentPlain;

	
	// Use this for initialization
	void Start () {
		myo = GameObject.Find ("Myo");
	
	}
	
	// Update is called once per frame
	void Update () {
		ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>(); //myo object에 접속
		
		
		
		 myoGryoX =thalmicMyo.gyroscope.x;
         myoGryoY= thalmicMyo.gyroscope.y;
		
		//increase rate for moving spaceship
        myoGryoX *= 0.009f;
        myoGryoY *= 0.009f;

		Vector3 screenPosition = Camera.main.WorldToViewportPoint (this.GetComponent<Transform> ().position); 
		//find screen limit
		if (isMyControl == true) {
			limitPlainX = screenPosition.x;
			limitPlainY = screenPosition.y;
		}
		//Vector3 screenPosition=Camera.main.vi
		//Debug.Log (screenPosition);

		// Debug.Log(thalmicMyo.gyroscope + " dd  ");
		// Debug.Log(newX + "   " + newY);
		//Debug.Log(thalmicMyo.gyroscope.x +"   "+thalmicMyo.gyroscope.y);
		
		//  Debug.Log(thalmicMyo.accelerometer.x);
		
		// this.gameObject.GetComponent<Transform>().position = (thalmicMyo.gyroscope/10);
		

		plainLocalX = this.gameObject.GetComponent<Transform>().position.x;
		plainLocalY = this.gameObject.GetComponent<Transform>().position.y;
		
		firerate -= Time.deltaTime; //how often do you want fire pistol

		if (limitPlainX >= 0.1f && limitPlainX <= 0.9f && limitPlainY >= 0.1f && limitPlainY <= 0.9f)
			this.gameObject.GetComponent<Transform> ().position = new Vector3 (plainLocalX += (myoGryoY) * -1, plainLocalY += myoGryoX, 0);
		else 
		{
			isMyControl = false;
			this.gameObject.GetComponent<Transform> ().position = new Vector3 (plainLocalX, plainLocalY, 0);
			AddTransparentShipObjectForPostionLimitExeed ();
		}


		//Debug.Log (this.gameObject.GetComponent<Transform> ().position);

		//change transform this space ship

		if (thalmicMyo.pose == Pose.Fist) { //if your gesture is fist then fire
			if(firerate<=0)
			{
				GameObject test=Instantiate(missile); //create missile gameobjet
				test.GetComponent<Transform>().position=new Vector3(plainLocalX,plainLocalY,1); //create missile object in front of z+1 from this object
				firerate=0.1f; //firerate initiallization
			}
			
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

			if (objLimitX >= 0.1f && objLimitX <= 0.9f && objLimitY >= 0.1f && objLimitY <= 0.9f) {
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

}
