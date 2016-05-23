using UnityEngine;
using System.Collections;

public class SemiBoss1Move : MonoBehaviour {

    float x, y, z;
    float moveSpeedZ = 0.1f, moveSpeedX = 0.4f, moveSpeedY = 0.2f;

    bool isMove=false;

    float moveToX = 0f, moveToY = 0f;
    public float wait1Seconds = 1.0f;
    public float moveSpeed = 0.1f;
    int fourDimension = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        x = this.GetComponent<Transform>().position.x;
        y = this.GetComponent<Transform>().position.y;
        z = this.GetComponent<Transform>().position.z;


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
            }


        }

        if(isMove==true)
        {
            this.GetComponent<Transform>().position = Vector3.MoveTowards(this.GetComponent<Transform>().position, new Vector3(moveToX, moveToY, z), moveSpeed);
            if (x == moveToX && y == moveToY)
            {
                wait1Seconds -= Time.deltaTime;
                if (wait1Seconds < 0f)
                {
                    isMove = false;
                    wait1Seconds = 1.0f;
                }
            } 

        }

        
	}

    void ChooseFourDimension()
    {
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
