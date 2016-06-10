using UnityEngine;
using System.Collections;

public class ScreenAdjust : MonoBehaviour {

    public float aspectWH = 1.6f;
    public float aspectAdd = 0.05f;

    public bool StartScreenAdjust = true;
    public bool UpdateScreenAdjust = false;

    Vector3 localScale;

	// Use this for initialization
	void Start () {
        localScale = transform.localScale;
        if (StartScreenAdjust)
        {
            _ScreenAdjust();
        }	
	}
	
	// Update is called once per frame
	void Update () {
                if (UpdateScreenAdjust) { 
            _ScreenAdjust();
	}
    }

    void _ScreenAdjust()
    {
        float wh = (float)Screen.width / (float)Screen.height;
         if(wh < aspectWH)
         {
           transform.localScale = new Vector3(localScale.x - (aspectWH - wh) + aspectAdd, localScale.y, localScale.z);

            } else   {
              transform.localScale = localScale;
            }
        }
}