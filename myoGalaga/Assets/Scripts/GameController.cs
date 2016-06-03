using UnityEngine;
using System.Collections;


using Pose = Thalmic.Myo.Pose;

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
	public GameObject[] EnemyList;
	public GameObject SemiBoss1;
	public GameObject SemiBoss2;
	public GameObject Boss;

	int enemyInstanCount=0;

	float instantiateTime=0f;
	float gameTime=0f; //game time
	float restartTime=3.0f;

	public int myLife=3; //my totla life
	public int score=0;
	public int bomb=3; //bomb use total count;
    int semiBossCount = 0;
	int pattern=1; 

	bool isInstantiateEnemy=false;
    bool isDead = true;
    bool isInstantiateMyShip = false;
    bool isSemiBossTiming = false;

    GameState gameState;
 
	// Use this for initialization
	void Start () {
        gameState = GameState.None;

	}

	void FixedUpdate()
	{
		ThalmicMyo thMyo = myo.GetComponent<ThalmicMyo> ();
		Debug.Log (thMyo.pose);
		if (thMyo.pose == Pose.WaveOut) {
			Time.timeScale = 0;
			Debug.Log ("asdfsadfasdf");
		}
	}

	
	// Update is called once per frame
	void Update () {

        switch(gameState)
        {
            case GameState.None:
                CheckMyoIsSynced();
                break;

		case GameState.Play:
			CheckMyoIsSynced ();
			CheckMyPlaneIsDie ();
			MakeEnemy ();
				gameTime += Time.deltaTime;
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

	void MakeEnemy()
	{
		instantiateTime -= Time.deltaTime;
		if(instantiateTime<0  && isSemiBossTiming==false)
			isInstantiateEnemy=false;

		if (instantiateTime < 0f && isInstantiateEnemy==false && pattern==1) {
			Instantiate (EnemyList [enemyInstanCount++]);
			isInstantiateEnemy = true;
			if (enemyInstanCount == 15) {
				isSemiBossTiming = true;
				instantiateTime = 15f;
				pattern++;
			}


			if(enemyInstanCount<5)
			instantiateTime =3.5f;
			
			else if (enemyInstanCount < 12)
				instantiateTime = 5f;
			else if(enemyInstanCount<=14)
				instantiateTime = 10f;
		}

		if(isSemiBossTiming==true&&instantiateTime<0f)
        {
            enemyInstanCount = 0;
			if (semiBossCount == 0) {
				semiBossCount++;
				GameObject semiBoss = Instantiate (SemiBoss1);
				isSemiBossTiming = false;

			} else {
				GameObject semiBoss = Instantiate (SemiBoss2);
				isSemiBossTiming = false;
			}
        }

	}

	void OnGUI()
	{
		GUI.Label (new Rect (Screen.width / 2, Screen.height - 100, 50, 50), gameTime.ToString ());
	}

    void CheckMyoIsSynced()
    {
		if (myo.GetComponent<ThalmicMyo> ().armSynced == true)
			gameState = GameState.Play;
		

        else
        {
       //     gameState = GameState.Pause;
            gameState = GameState.Play;
        }
    }

    void CheckMyPlaneIsDie()
    {
		if(GameObject.Find("My")==false && isDead==false )
        {
            isDead = true;
            if(isDead==true)
            {
				isInstantiateMyShip = false;
                gameState = GameState.Die;
            }

        }
    }

    void InstantiateMyShipAgain()
    {
		restartTime -= Time.deltaTime;
		if(isInstantiateMyShip==false && myLife>=1  && restartTime<0f)
        {
            isInstantiateMyShip = true;
            isDead = false;
            myLife--;

            GameObject myPlane = Instantiate(myShip);
			restartTime = 3f;
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
