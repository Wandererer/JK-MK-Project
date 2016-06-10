using UnityEngine;
using System.Collections;

public class SemiBoss2Move : MonoBehaviour {
    float x, y;
    public float z;
    float moveSpeedZ = 0.2f;
    float moveSpeed = 0.3f;
    float rotationX;

    bool isFireLaser, isMove = false, isRotateWhileMoving=true;

    int dimensionNumber=0, previousDimension = -1;

    float moveToX, moveToY;
    float wait1Seconds = 1f;

    Transform target;
    public GameObject PowerItem;
    public GameObject BombItem;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


        x = this.GetComponent<Transform>().position.x;
        y = this.GetComponent<Transform>().position.y;
        z = this.GetComponent<Transform>().position.z;
        rotationX = this.GetComponent<Transform>().eulerAngles.x;
        rotationX = Mathf.Round(rotationX);
        isFireLaser = this.GetComponent<SemiBoss2MissileLauncher>().isFireLaser;

        try
        {
            target = GameObject.FindGameObjectWithTag("My").GetComponent<Transform>();
        }
        catch (System.Exception e)
        {
            this.GetComponent<SemiBoss2MissileLauncher>().isFireLaser = false;
        }
    

        if(isMove==false && wait1Seconds>0f)
        {
            ChooseDimension();
            switch (dimensionNumber)
            {
                case 0: FirstDimension(); break;
                case 1: SecondDimension(); break;
                case 2: ThirdDimension(); break;
                case 3: FourthDimension(); break;
                case 4: FifthDimension(); break;
                case 5: SixthDimension(); break;
                case 6: SeventhDimension(); break;
                case 7: EighthDimension(); break;
            }

            isMove = true;
            
        }

        if(z>23)
            this.GetComponent<Transform>().position = new Vector3(x, y, z -= moveSpeedZ);
        else
        {
            if(isFireLaser==false)
            {
                if(isMove==true)
                {
                    this.GetComponent<Transform>().position = Vector3.MoveTowards(this.GetComponent<Transform>().position, new Vector3(moveToX, moveToY, z), moveSpeed);
                   
                    if (isRotateWhileMoving == true)
                    {
                        if (x < moveToX)
                            GetComponent<Transform>().Rotate((new Vector3(-0.2f, 0, 0)));
                        else
                            GetComponent<Transform>().Rotate((new Vector3(0.2f, 0, 0)));
                    }

                    if (x == moveToX && y == moveToY)
                    {
                        if (isRotateWhileMoving == true)
                            isRotateWhileMoving = false;

                        if (rotationX > 0 && rotationX < 50)
                            GetComponent<Transform>().Rotate((new Vector3(-0.9f, 0, 0)));
                        else if (rotationX < 360 && rotationX > 310)
                            GetComponent<Transform>().Rotate((new Vector3( 0.9f, 0,0)));

                        wait1Seconds -= Time.deltaTime;
                        if (wait1Seconds < 0f)
                        {

                            isRotateWhileMoving = true;
                            isMove = false;

                            wait1Seconds = 1.0f;
                        }
                    } 
                }
            }
            else
            {
                this.GetComponent<Transform>().position = Vector3.MoveTowards(new Vector3(x,y,z), new Vector3(target.position.x,target.position.y-0.3f,z), 0.1f);
            }

        }
       
	}

    void ChooseDimension()
    {
        dimensionNumber = Random.Range(0, 7);
        CheckPreviousNumber();
        previousDimension = dimensionNumber;
    }

    void CheckPreviousNumber()
    {
        while(dimensionNumber==previousDimension)
            dimensionNumber = Random.Range(0, 7);
    }


    void FirstDimension()
    {
        moveToX = Random.Range(-9, -5);
        moveToY = Random.Range(0, 5);
    }

    void SecondDimension()
    {
        moveToX = Random.Range(-4, 0);
        moveToY = Random.Range(0, 5);
    }

    void ThirdDimension()
    {
        moveToX = Random.Range(0, 4);
        moveToY = Random.Range(0, 5);
    }

    void FourthDimension()
    {
        moveToX = Random.Range(5, 9);
        moveToY = Random.Range(0, 5);
    }

    void FifthDimension()
    {
        moveToX = Random.Range(-9, -5);
        moveToY = Random.Range(-5,0);
    }

    void SixthDimension()
    {
        moveToX = Random.Range(-4, 0);
        moveToY = Random.Range(-5,0);
    }

    void SeventhDimension()
    {
        moveToX = Random.Range(0, 4);
        moveToY = Random.Range(-5,0);
    }

    void EighthDimension()
    {
        moveToX = Random.Range(5, 9);
        moveToY = Random.Range(-5,0);
    }



}
