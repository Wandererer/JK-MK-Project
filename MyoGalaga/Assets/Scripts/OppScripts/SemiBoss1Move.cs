using UnityEngine;
using System.Collections;

public class SemiBoss1Move : MonoBehaviour {

    float x, y, z;
	float moveSpeedZ = 0.1f;
	float rotationZ,rotationX;
    bool isMove=false,isRotateWhileMoving=true;

	public float distanceOnce;
	public float distanceContinue;
    float moveToX = 0f, moveToY = 0f;
    public float wait1Seconds = 1.0f;
    public float moveSpeed = 0.3f;
    int fourDimension = 0;
	int previousDimension=-1;

    int hp;

    public GameObject PowerItem;

	// Use this for initialization
	void Start () {
	
	}
    void FixedUpdate() { }
	// Update is called once per frame
	void Update () {
		distanceContinue=Vector3.Distance (new Vector3 (x, y, z), new Vector3 (moveToX, moveToY, z));
		//Debug.Log (distanceOnce);
        x = this.GetComponent<Transform>().position.x;
        y = this.GetComponent<Transform>().position.y;
        z = this.GetComponent<Transform>().position.z;
		rotationZ = this.GetComponent<Transform> ().eulerAngles.z;
		rotationX = this.GetComponent<Transform> ().eulerAngles.x;
		rotationZ=Mathf.Round (rotationZ);
		rotationX=Mathf.Round (rotationX);

        if(z>17)
            this.GetComponent<Transform>().position = new Vector3(x, y, z -= moveSpeedZ);


        else
        {

            if (isMove == false && wait1Seconds > 0f)
            {
               
                    ChooseFourDimension();

                if(fourDimension==0)
                {
                    FirstDimensionSelectCoordinate();
                    isMove = true;
                }
                else if(fourDimension==1)
                {
                    SecondDimensionSelectCoordinate();
                    isMove = true;
                }
                else if(fourDimension==2)
                {
                    ThirdDimensionSelectCoordinate();
                    isMove = true;
                }

                else
                {
                    FourthDimensionSelectCoordinate();
                    isMove = true;
                }

				distanceOnce = Vector3.Distance (new Vector3 (x, y, z), new Vector3 (moveToX, moveToY, z));
			}


        }

        if(isMove==true)
        {
			//Debug.Log (rotationZ);
            this.GetComponent<Transform>().position = Vector3.MoveTowards(this.GetComponent<Transform>().position, new Vector3(moveToX, moveToY, z), moveSpeed);
			if (isRotateWhileMoving == true) {
				if (x < moveToX)
					GetComponent<Transform> ().Rotate ((new Vector3 (0, 0, 0.2f)));
				else
					GetComponent<Transform> ().Rotate ((new Vector3 (0, 0, -0.2f)));
			}

			if (x == moveToX && y == moveToY)
            {
				if(isRotateWhileMoving==true)
					isRotateWhileMoving = false;

				if(rotationZ>0 && rotationZ<100)
					GetComponent<Transform> ().Rotate ((new Vector3 (0, 0, -0.9f)));
				else if(rotationZ<360 && rotationZ>260)
					GetComponent<Transform> ().Rotate ((new Vector3 (0, 0, 0.9f)));

                wait1Seconds -= Time.deltaTime;
                if (wait1Seconds < 0f)
                {
					this.GetComponent<SemiBoss1MissileLauncher> ().isFireMiddleFirst = false;
					this.GetComponent<SemiBoss1MissileLauncher> ().isInstantiate = false;
					this.GetComponent<SemiBoss1MissileLauncher> ().isFireMiddle = false;
					isRotateWhileMoving = true;
                    isMove = false;
			
                    wait1Seconds = 1.0f;
                }
            } 

        }

        
	}


    void ChooseFourDimension()
    {
        fourDimension = Random.Range(0, 3);
		CheckPrevious ();
		previousDimension = fourDimension;
    }

	void CheckPrevious()
	{
		while(fourDimension==previousDimension)
			fourDimension = Random.Range(0, 3);
	}

    void FirstDimensionSelectCoordinate()
    {
        moveToX = Random.Range(2, 15);
        moveToY = Random.Range(2, 7);
    }

    void SecondDimensionSelectCoordinate()
    {
        moveToX = Random.Range(-2, -15);
        moveToY = Random.Range(2, 7);
    }
    void ThirdDimensionSelectCoordinate()
    {
        moveToX = Random.Range(-2, -15);
        moveToY = Random.Range(-2, -7);
    }
    void FourthDimensionSelectCoordinate()
    {
        moveToX = Random.Range(2, 15);
        moveToY = Random.Range(-2, -7);
    }



}
