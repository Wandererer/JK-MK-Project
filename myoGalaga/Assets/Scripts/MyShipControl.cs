using UnityEngine;
using System.Collections;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

public class MyShipControl : MonoBehaviour {
	public GameObject myo = null;
	
	float x, y, z;
	
	float firerate=0.1f;
	
	public GameObject missile;
	
	
	// Use this for initialization
	void Start () {
		myo = GameObject.Find ("Myo");
	}
	
	// Update is called once per frame
	void Update () {
		ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>(); //myo object에 접속
		
		
		
		float newX =thalmicMyo.gyroscope.x;
		float newY=thalmicMyo.gyroscope.y;
		
		//increase rate for moving spaceship
		newX *= 0.05f;
		
		newY *= 0.05f;
		
		// Debug.Log(thalmicMyo.gyroscope + " dd  ");
		// Debug.Log(newX + "   " + newY);
		//Debug.Log(thalmicMyo.gyroscope.x +"   "+thalmicMyo.gyroscope.y);
		
		//  Debug.Log(thalmicMyo.accelerometer.x);
		
		// this.gameObject.GetComponent<Transform>().position = (thalmicMyo.gyroscope/10);
		

		x = this.gameObject.GetComponent<Transform>().position.x;
		z = this.gameObject.GetComponent<Transform>().position.z;
		y = this.gameObject.GetComponent<Transform>().position.y;
		
		firerate -= Time.deltaTime; //how often do you want fire pistol
		
		this.gameObject.GetComponent<Transform>().position = new Vector3(x += (newY)*-1, y += newX, 0);
		Debug.Log (this.gameObject.GetComponent<Transform> ().position);

		//change transform this space ship

		if (thalmicMyo.pose == Pose.Fist) { //if your gesture is fist then fire
			if(firerate<=0)
			{
				GameObject test=Instantiate(missile); //create missile gameobjet
				test.GetComponent<Transform>().position=new Vector3(x,y,1); //create missile object in front of z+1 from this object
				firerate=0.1f; //firerate initiallization
			}
			
		}



	}

	void OnGUI()
	{
		GUI.Button (new Rect (50, 50, 200, 200), "sdfsadsdaf");
	}

}
