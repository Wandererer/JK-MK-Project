using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	float gameTime=0f; //game time
	int myLife=3; //my totla life
	int score=0;
	int bomb=3; //bomb use total count;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameTime += Time.deltaTime;
	}

	public int getBombCount()
	{
		return bomb;
	}

	public void setBombCount(int num)
	{
		bomb = num;
	}


}
