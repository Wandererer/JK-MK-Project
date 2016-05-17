using UnityEngine;
using System.Collections;

public class Instantiate70Move : MonoBehaviour {
    float x, y, z;
    float moveSpeedZ = 0.1f, moveSpeedX = 0.2f, moveSpeedY = 0.2f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        x = this.GetComponent<Transform>().position.x;
        y = this.GetComponent<Transform>().position.y;
        z = this.GetComponent<Transform>().position.z;

      //  Debug.Log(x + " " + y + "  " + z);

        if(z>17)
            this.GetComponent<Transform>().position = new Vector3(x, y, z -= moveSpeedZ);
        else if (y < 0.7)
        {
			this.GetComponent<Transform>().position = new Vector3(x +=moveSpeedX, y, z);
            GetComponent<Transform>().Rotate((new Vector3(0, -7f, 0)));
        }
        else
        {
            this.GetComponent<Transform>().position = new Vector3(x += moveSpeedX, y -= moveSpeedY, z -= moveSpeedZ);
           GetComponent<Transform>().Rotate((new Vector3(-0.1f, -0.1f, 0)));
        }
        if (x > 20)
            Destroy(this.gameObject);

	}
}
