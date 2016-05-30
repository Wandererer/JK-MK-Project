using UnityEngine;
using System.Collections;

public enum GameState
{
    None=0,
    Play,
    Pause,
    Boss,
    Die,
}


public class GameController : MonoBehaviour {

    public GameObject myo;
    public GameObject myShip;


	float gameTime=0f; //game time
	public int myLife=3; //my totla life
	public int score=0;
	public int bomb=3; //bomb use total count;

    bool isDead = true;
    bool isInstantiateMyShip = false;

    GameState gameState;
 

	// Use this for initialization
	void Start () {
        gameState = GameState.None;
	}
	
	// Update is called once per frame
	void Update () {
		gameTime += Time.deltaTime;

        switch(gameState)
        {
            case GameState.None:
                CheckMyoIsSynced();
                break;

            case GameState.Play:
                CheckMyoIsSynced();
                CheckMyPlaneIsDie();
                break;

            case GameState.Pause:
                CheckMyoIsSynced();
                break;
            case GameState.Die:
                InstantiateMyShipAgain();

                break;


            default:

                break;
        }

	}

    void CheckMyoIsSynced()
    {
        if (myo.GetComponent<ThalmicMyo>().armSynced == true)
            gameState = GameState.Play;
        else
        {
            gameState = GameState.Pause;
        }
    }

    void CheckMyPlaneIsDie()
    {
        if(myShip==null && isDead==false )
        {
            isDead = true;
            if(isDead==true)
            {
                gameState = GameState.Die;
            }

        }
    }

    void InstantiateMyShipAgain()
    {
        if(isInstantiateMyShip==false && myLife>=1)
        {
            isInstantiateMyShip = true;
            isDead = false;
            myLife--;

            GameObject myPlane = Instantiate(myShip);
            gameState = GameState.Play;
        }
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
