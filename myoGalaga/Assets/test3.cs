using UnityEngine;
using System.Collections;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

public class test3 : MonoBehaviour {

    public GameObject myo = null;
    float myoZ,myoX;
    Transform thisTransfrom=null;
    float x, y, z;

	float firerate=0.1f;

    private static float Q = 0.000001f;
    private static float R = 0.01f;
    private static float P = 1, X = 0, K;

	public GameObject missile;




	// Use this for initialization
	void Start () {
        thisTransfrom = this.gameObject.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
       // Debug.Log("rotation "+myo.transform.rotation);
       // Debug.Log("position " + myo.transform.position);
        ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>(); //myo object에 접속
 
       // Vector3 antigravity = Vector3.up;
        //Vector3 m = Vector3.Cross(myo.transform.forward, antigravity);//외적값 구함  myo.transform.forward ( 회전 좌우) y가 0이나옴 외적90이라
       // Vector3 roll = Vector3.Cross(m, myo.transform.forward); //외적값 구함
       //Debug.Log(thalmicMyo.gyroscope + " dd  ");
       
        
        
        
       // float newX = Mathf.Round(newTest(thalmicMyo.gyroscope.x));
     // float newY = Mathf.Round(newTest(thalmicMyo.gyroscope.y));

   

      //newX *= 0.01f;
      //newY *= 0.01f;

        float newX =thalmicMyo.gyroscope.x;
        float newY=thalmicMyo.gyroscope.y;

		
		newX *= 0.05f;

		newY *= 0.05f;

      // Debug.Log(thalmicMyo.gyroscope + " dd  ");
      // Debug.Log(newX + "   " + newY);
        Debug.Log(thalmicMyo.gyroscope.x +"   "+thalmicMyo.gyroscope.y);

     //  Debug.Log(thalmicMyo.accelerometer.x);

       // this.gameObject.GetComponent<Transform>().position = (thalmicMyo.gyroscope/10);
 
        
        x = this.gameObject.GetComponent<Transform>().position.x;
         z = this.gameObject.GetComponent<Transform>().position.z;
         y = this.gameObject.GetComponent<Transform>().position.y;

		firerate -= Time.deltaTime;

         this.gameObject.GetComponent<Transform>().position = new Vector3(x += ((int)newY)*-1, y += (int)newX, 0);

		if (thalmicMyo.pose == Pose.Fist) {
			if(firerate<=0)
			{
			GameObject test=Instantiate(missile);
			test.GetComponent<Transform>().position=new Vector3(x,y,1);
				firerate=0.1f;
			}

		}


       // myoX = myo.transform.rotation.x*10;
        //Debug.Log(myoX);
        /*
        if(myoX<-0.5 && myoX>-2)
            this.gameObject.GetComponent<Transform>().position = new Vector3(x, y+0.1f, z);
        else if(myoX>0.8  && myoX<2)
            this.gameObject.GetComponent<Transform>().position = new Vector3(x, y - 0.1f, z);
        
       
       */

        /*
        Debug.Log(myoZ);
        myoZ = myo.transform.rotation.z * 10;
      
        if (myoZ > 1 && myoZ < 5)
        {
            Debug.Log("ddd");
            this.gameObject.GetComponent<Transform>().position = new Vector3(x - 0.1f, y, z);


        }

        else if  (myoZ < -1 && myoZ > -5)
        {
            Debug.Log("ddd");
            this.gameObject.GetComponent<Transform>().position = new Vector3(x +0.1f, y, z);


        }
   */
         
         
	}

    private static void mesasurmentUpdate()
    {
        K = (P + Q) / (P + Q + R);
        P = R * (P + Q) / (P + Q + R);
    }

    public static float newTest(float measurement)
    {
        mesasurmentUpdate();
        float result = X + (measurement - X) * K;
        X = result;
        return result;
    }
}
