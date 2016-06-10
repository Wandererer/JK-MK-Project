using UnityEngine;
using System.Collections;

public class BossMovement : MonoBehaviour {

	float[] bossMoveX ={-14,0,14}; //boss move X
	float[] bossMoveY = { -6, 0,6};//boss move y

	public int selectX; //bossmoveselect x position
	public int selectY;//boss move select y position

	float tempX;
	float tempY;

	float bossCurrentX;
	float bossCurrentY;
	float bossCurrentZ;

	float findLocationX;
	float findLocationY;

	public float bossMoveSpeed=0.1f;

	float stopMoving=2f;

	bool isMoveFinish = true;
	bool isFindLocation=true;
    bool isFireLaser;
    public bool isStart = true;

    Transform target;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        try
        {


            target = GameObject.FindGameObjectWithTag("My").GetComponent<Transform>();
        }
        catch(System.Exception e)
        {
            isFireLaser = false;
        }
        isFireLaser = this.GetComponent<BossMissileLauncher>().isFireLaser;
		bossCurrentX = GetComponent<Transform> ().position.x;
		bossCurrentY = GetComponent<Transform> ().position.y;
		bossCurrentZ = GetComponent<Transform> ().position.z;
        if(isStart==true)
        {
            this.GetComponent<Transform>().position = Vector3.MoveTowards(this.GetComponent<Transform>().position, new Vector3(0,0, 60), bossMoveSpeed);
            GameObject.Find("MyShip").GetComponent<MyShipControl>().isFireOk = false;

            if (bossCurrentX == 0 && bossCurrentY == 0)
            {
                isStart = false;
                GameObject.Find("MyShip").GetComponent<MyShipControl>().isFireOk = true;
            }
        }

        else
        {

            if (isMoveFinish == true)
            {
                SelectAgainForMove();
                stopMoving -= Time.deltaTime;
            }
            else if(isFireLaser==true)
            {
                try
                {
                    this.GetComponent<Transform>().position = Vector3.MoveTowards(this.GetComponent<Transform>().position, new Vector3(target.position.x, target.position.y, 60), 0.15f);
                }

                catch(System.Exception e)
                {

                }
            }

            else if (isMoveFinish == false)
            {
                BossMoveAndCheck();

            }
        }
	}

    void BossMoveAndCheck()
    {
        this.GetComponent<Transform>().position = Vector3.MoveTowards(this.GetComponent<Transform>().position, new Vector3(bossMoveX[selectX], bossMoveY[selectY], 60), bossMoveSpeed);
        if (bossCurrentX == bossMoveX[selectX] && bossCurrentY == bossMoveY[selectY])
        {
            isMoveFinish = true;
        }
    }

    void SelectAgainForMove()
    {
        if (stopMoving < 0)
        {

            selectMoveXY();

            tempX = selectX;
            tempY = selectY;


            while (tempX == selectX && tempY == selectY)
            {
                tempX = selectX;
                tempY = selectY;
                selectMoveXY(); //don't move same position
            }

            isMoveFinish = false;


            stopMoving = 2f;
        }
    }

	 void selectMoveXY()
	{
		selectX = Random.Range (0, 3);
		selectY = Random.Range (0, 3);
	}




}
