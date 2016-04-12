using UnityEngine;
using System.Collections;

public class BossMovement : MonoBehaviour {

	int[] bossMoveX ={-32,0,32}; //boss move X
	int[] bossMoveY = { 15, 0,-15};//boss move y

	public int selectX; //bossmoveselect x position
	public int selectY;//boss move select y position

	int tempX;
	int tempY;

	float bossCurrentX;
	float bossCurrentY;
	float bossCurrentZ;

	float findLocationX;
	float findLocationY;

	float bossMoveSpeed=3.1f;

	float moveMountX=3.1f;
	float moveMountY=3.1f;

	float stopMoving=2f;

	bool isMoveFinish = true;
	bool isFindLocation=true;

	// Use this for initialization
	void Start () {
		bossCurrentX = GetComponent<Transform> ().position.x;
		bossCurrentY = GetComponent<Transform> ().position.y;
		bossCurrentZ = GetComponent<Transform> ().position.z;
	}
	
	// Update is called once per frame
	void Update () {

		bossCurrentX = GetComponent<Transform> ().position.x;
		bossCurrentY = GetComponent<Transform> ().position.y;
		bossCurrentZ = GetComponent<Transform> ().position.z;
		
		if (isMoveFinish == true) {
			if (stopMoving < 0) {
				setMoveMountAgain ();

				selectMoveXY ();

				tempX = selectX;
				tempY = selectY;
		

				while (tempX == selectX && tempY == selectY) {
					tempX = selectX;
					tempY = selectY;
					selectMoveXY (); //don't move same position
				}

				isMoveFinish = false;

				if (isFindLocation) {
					findLocationX = bossMoveX [selectX] - bossCurrentX;
					findLocationY = bossMoveY [selectY] - bossCurrentY;
					Debug.Log (findLocationX + " " + findLocationY + "test");
					isFindLocation = false;
				}

				if (bossMoveX [selectX] < 0)
					moveMountX *= -1;
				if (bossMoveY [selectY] < 0)
					moveMountY *= -1; 
				Debug.Log (bossMoveX [selectX] + "  " + bossMoveY [selectY] + " 갈곳");
				stopMoving = 2f;
			}
			stopMoving -= Time.deltaTime;
		}

		//float compareX = bossMoveX [selectX] - bossCurrentX;
		//float compareY = bossMoveY [selectY] - bossCurrentY;

		//float LimitMoveX = Mathf.Abs (bossMoveX [selectX]);
		//float LimitMoveY = Mathf.Abs (bossMoveY [selectY]);


	
		if (isMoveFinish == false) {

		

		


			/*			

			if (findLocationX > 0) {
				checkPositiveXLimit ();
			} 
			else if(findLocationX==0)
			{
				ifXisZero();
			}

					else {
				checkNegativeXLimit ();
			}

			if (findLocationY > 0) {
				checkPositiveYLimit ();
			} 
			else if(findLocationY==0)
			{
				ifYisZero();
			}
			else {
				checkNegativeYLimit ();
			}
*/
			checkXMove ();
			checkYMove ();

				this.GetComponent<Transform> ().position = 
				new Vector3 (bossCurrentX += moveMountX, 
					bossCurrentY += moveMountY,
					bossCurrentZ);
			
			if (moveMountX == 0 && moveMountY == 0) {
				isMoveFinish = true;
				isFindLocation = true;
			}
		}
	}

	void checkPositiveXLimit()
	{
		if (bossMoveX [selectX] < bossCurrentX){
			moveMountX = 0;
		}
	}
	
	void ifXisZero()
	{
		moveMountX = 0;			
	}

	void checkNegativeXLimit()
	{
		if (bossMoveX [selectX] > bossCurrentX)
		{
				moveMountX = 0;
		} 
	}

				void ifYisZero()
				{
					moveMountY = 0;			
				}


	void checkPositiveYLimit()
	{
		if (bossMoveY [selectY] < bossCurrentY) {

			moveMountY = 0;
		}
	}

	void checkNegativeYLimit()
	{
		if (bossMoveY [selectY] > bossCurrentY) {
				moveMountY = 0;

		}
	}

	void checkXMove()
	{
		if (bossMoveX [selectX] == Mathf.Round (bossCurrentX)) {
			moveMountX = 0;

			return;
		}
		if (bossMoveX [selectX] > bossCurrentX) {
			if (moveMountX < 0)
				moveMountX *= -1;
		} else {
			if (moveMountX > 0)
				moveMountX *= -1;
		}


	}

	void checkYMove()
	{
		if (bossMoveY [selectY] == Mathf.Round (bossCurrentY)) {
			moveMountY = 0;

			return;
		}
		if (bossMoveY [selectY] > bossCurrentY) {
			if (moveMountY < 0)
				moveMountY *= -1;
		} else {
			if (moveMountY > 0)
				moveMountY *= -1;
		}
	}

	public void selectMoveXY()
	{
		selectX = Random.Range (0, 3);
		selectY = Random.Range (0, 3);
	}

	public void setIsMoveFinish(bool isTrue)
	{
		isMoveFinish = isTrue;
	}

	public void setBossMoveSpeed(float speed)
	{
		bossMoveSpeed = speed;

	}

	public void setMoveMountAgain()
	{
		moveMountX = bossMoveSpeed;
		moveMountY = bossMoveSpeed;
	}

}
