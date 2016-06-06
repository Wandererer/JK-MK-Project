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
    GameOver,
    Win
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
	float restartTime=5.0f;

	public int myLife=3; //my totla life
	public int score=0;
	public int bomb; //bomb use total count;
    int semiBossCount = 0;
	int pattern=0; 

	bool isInstantiateEnemy=true;
   public bool isDead = false;
    bool isInstantiateMyShip = false;
    bool isSemiBossTiming = false;
    bool isPause = false;
    bool isBossTiming = false;
    bool isBossInstantiate = false;
    public bool isSemiBoss1Die = false;
    public bool isSemiBoss2Die = false;

   public  GameState gameState;
    ThalmicMyo thMyo; 
	// Use this for initialization
	void Start () {
        gameState = GameState.None;
        myo = GameObject.Find("Myo");
        thMyo = myo.GetComponent<ThalmicMyo>();
	}

	void FixedUpdate()
	{
        /*
        if (thMyo.pose == Pose.WaveOut && isPause == false)
        {
            Time.timeScale = 0;
            isPause = true;
            Debug.Log("Pause");
        }


        if (thMyo.pose == Pose.FingersSpread && isPause == true)
        {
            Time.timeScale = 1;
            isPause = false;
            Debug.Log("play");
        }

		//Debug.Log (thMyo.pose);

      */
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
                gameTime += Time.deltaTime;
                break;

            case GameState.GameOver:


                break;

            case GameState.Win:

                break;
            default:

                break;
        }

	}

	void MakeEnemy()
	{
        Debug.Log(enemyInstanCount);

		instantiateTime -= Time.deltaTime;
		if(instantiateTime<0  && isSemiBossTiming==false)
			isInstantiateEnemy=false;

		if (instantiateTime < 0f && isInstantiateEnemy==false && pattern==0) {
			Instantiate (EnemyList [enemyInstanCount++]);
			isInstantiateEnemy = true;
			if (enemyInstanCount == 15) {
				isSemiBossTiming = true;
                enemyInstanCount = 1;
				instantiateTime = 10f;
				pattern++;
			}
            else
            ResetInstantiateTime();
		}

        else if (instantiateTime < 0f && isInstantiateEnemy == false && pattern == 1 && semiBossCount == 1&& isSemiBoss1Die==true)
        {
            Debug.Log("pattern2");
            Instantiate(EnemyList[enemyInstanCount]);
            enemyInstanCount += 2;
            isInstantiateEnemy = true;

            if (enemyInstanCount == 15)
            {
                Debug.Log(isSemiBossTiming);
                instantiateTime = 10f;
                isSemiBossTiming = true;
                enemyInstanCount = 0;
                pattern++;
            }
            else
            ResetInstantiateTime();
        }

        else if (instantiateTime < 0f && isInstantiateEnemy == false && pattern == 2 && semiBossCount == 2  && isSemiBoss2Die==true)
        {
            Instantiate(EnemyList[enemyInstanCount]);
            enemyInstanCount += 2;
            isInstantiateEnemy = true;

            if(enemyInstanCount==16)
            {
                instantiateTime = 10f;
                isBossTiming = true;
                pattern++;
            }
            else
            ResetInstantiateTime();
        }

		else if(isSemiBossTiming==true&&instantiateTime<0f)
        {
			if (semiBossCount == 0) {
				semiBossCount++;
				GameObject semiBoss = Instantiate (SemiBoss1);
                semiBoss.name = "SemiBoss1";
                isSemiBossTiming = false;
			} else if(semiBossCount==1) {
                Debug.Log(instantiateTime);
				GameObject semiBoss = Instantiate (SemiBoss2);
                semiBoss.name = "SemiBoss2";
                semiBossCount++;
                isSemiBossTiming = false;
			}
        }

        if (isBossTiming == true && instantiateTime<0f)
        {
            Debug.Log("asfasdfsadf");
            if (isBossInstantiate == false)
            {
                gameState = GameState.Boss;
                isBossInstantiate = true;
               GameObject boss= Instantiate(Boss);
               boss.name = "Boss";
               boss.GetComponent<Transform>().position = new Vector3(0,-70, 60);
            }
        }

	}

    void ResetInstantiateTime()
    {

        if (enemyInstanCount < 5)
            instantiateTime = 3.5f;

        else if (enemyInstanCount < 12)
            instantiateTime = 5f;
        else if (enemyInstanCount <= 14)
            instantiateTime = 10f;
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
		if( isDead==true )
        {

				isInstantiateMyShip = false;
                gameState = GameState.Die;
          }
    }

    void InstantiateMyShipAgain()
    {
		restartTime -= Time.deltaTime;
		if(isInstantiateMyShip==false && myLife>=1  && restartTime<0f)
        {
     //       isInstantiateMyShip = true;
            isDead = false;
            myLife--;

            GameObject myPlane = Instantiate(myShip);
            myPlane.name = "MyShip";
            gameState = GameState.Play;
			restartTime = 5f;
         
        }
    }

	public int getBombCount()
	{
		return bomb;
	}

	public void MinusBombCount()
	{
		bomb-=1;
	}

    public void setSemiBoss1DieTrue()
    {
        isSemiBoss1Die = true;
    }


}
