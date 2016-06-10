using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

using Pose = Thalmic.Myo.Pose;

public enum GameState
{
    Play=0,
    Pause,
    Boss,
    Die,
    GameOver,
    Win
}


public class GameController : MonoBehaviour {

    public GameObject myo; //Get Myo
    public GameObject myShip; //Player
	public GameObject[] EnemyList;  //Enemy Patern List
	public GameObject SemiBoss1; 
	public GameObject SemiBoss2;
	public GameObject Boss;
    public GameObject BossHpBar; //Boss Hp Bar

    public AudioClip Normal;  //Normal Sound
    public AudioClip BossStart; //When Boss Came out Sound
    public AudioClip BossStage; //When Fight with Boss Sound

	int enemyInstanCount=0; //Enemy Instant number for this

    float sceneChangeTime = 4.0f;  //When Die or Win Change Scene time;
	float instantiateTime=0f; //current enemy and next enemy gap
	public float gameTime=0f; //game time
	float restartTime=5.0f; //Player restart Time;

    float baseWidth = 854f;  //for GUI
    float baseHeight = 480f; //for GUI

	public int myLife=3; //my total life
	public int score=0; //my total score
	public int bomb; //bomb use total count;
    int semiBossCount = 0; //semi boss number 
	int pattern=0;  //enemy pattern different pattern before semiboss1 and semiboss2 and boss;

	bool isInstantiateEnemy=true;
    bool isInstantiateMyShip = false;
    bool isSemiBossTiming = false;
    bool isPause = false;
    bool isBossTiming = false;
    bool isBossInstantiate = false;
    bool isBossModeStart = false;
    bool isChangeAudioClip = false;
    public bool isSemiBoss1Die = false;
    public bool isSemiBoss2Die = false;
    public bool isDead = false;

    public  GameState gameState;
    GameState preGameState;

    public Texture2D bombTexture;
    public Texture2D myShipTexture;

    public GUIStyle BombTextureStyle;
    public GUIStyle myShipTextureStyle;

    AudioSource audioSource;

    ThalmicMyo thMyo; 
	// Use this for initialization
	void Start () {
        gameState = GameState.Play;
        myo = GameObject.Find("Myo");
        thMyo = myo.GetComponent<ThalmicMyo>();
        audioSource = this.GetComponent<AudioSource>();
        audioSource.clip = Normal;
        audioSource.loop = true;
        audioSource.Play();
	}

	void FixedUpdate()
	{


	}
	
	// Update is called once per frame
	void Update () {

        Debug.Log(gameState);
        switch (gameState)
        {
		case GameState.Play:
			CheckMyoIsSynced ();
			CheckMyPlaneIsDie ();
			MakeEnemy ();
				gameTime += Time.deltaTime;
                break;

            case GameState.Pause:
                CheckMyoIsSynced();
                break;

            case GameState.Boss:
                gameTime += Time.deltaTime;
                CheckMyPlaneIsDie();
                ChangeBossBGM();
                MakeBossHpBar();
              //  CheckMyoIsSynced();
                break;

            case GameState.Die:
                InstantiateMyShipAgain();
                gameTime += Time.deltaTime;
                break;

            case GameState.GameOver:
                sceneChangeTime -= Time.deltaTime;
                SceneChange();
                break;

            case GameState.Win:
  
                sceneChangeTime -= Time.deltaTime;
                SceneChange();
                break;
            default:

                break;
        }

	}


    void OnGUI()
    {
        // 스코어, 시간 표시(좌측 하단)
        GUI.Box(new Rect(0, Screen.height - 50, 140, 50), "Total Score : " + score + "\n" + "Time :          " + (int)gameTime);

        // 해상도 대응
        GUI.matrix = Matrix4x4.TRS(
            Vector3.zero,
            Quaternion.identity,
            new Vector3(Screen.width / baseWidth, Screen.height / baseHeight, 1f));

        // 폭탄 개수 표시(좌측 상단)
        GUI.Label(
            new Rect(8f, 8f, 128f, 48f),
            new GUIContent(" x" + bomb.ToString(), bombTexture),
            BombTextureStyle);

        // 목숨 개수 표시(좌측 상단)
        GUI.Label(
             new Rect(8f, 8f + 48f, 128f, 48f),
             new GUIContent(" x" + myLife.ToString(), myShipTexture),
             myShipTextureStyle);
    }
    

    void MakeEnemy()
    {
      //  Debug.Log(enemyInstanCount);

        instantiateTime -= Time.deltaTime;
        if (instantiateTime < 0 && isSemiBossTiming == false)
            isInstantiateEnemy = false;

        if (instantiateTime < 0f && isInstantiateEnemy == false && pattern == 0)
        {
            Instantiate(EnemyList[enemyInstanCount++]);
            isInstantiateEnemy = true;
            if (enemyInstanCount == 15)
            {
                isSemiBossTiming = true;
                enemyInstanCount = 1;
                instantiateTime = 20f;
                pattern++;
            }
            else
                ResetInstantiateTime();
        }

        else if (instantiateTime < 0f && isInstantiateEnemy == false && pattern == 1 && semiBossCount == 1 && isSemiBoss1Die == true)
        {
           // Debug.Log("pattern2");
            Instantiate(EnemyList[enemyInstanCount]);
            enemyInstanCount += 2;
            isInstantiateEnemy = true;

            if (enemyInstanCount == 15)
            {
              //  Debug.Log(isSemiBossTiming);
                instantiateTime = 10f;
                isSemiBossTiming = true;
                enemyInstanCount = 0;
                pattern++;
            }
            else
                ResetInstantiateTime();
        }

        else if (instantiateTime < 0f && isInstantiateEnemy == false && pattern == 2 && semiBossCount == 2 && isSemiBoss2Die == true)
        {
            Instantiate(EnemyList[enemyInstanCount]);
            enemyInstanCount += 2;
            isInstantiateEnemy = true;

            if (enemyInstanCount == 16)
            {
                instantiateTime = 10f;
                isBossTiming = true;
                pattern++;
            }
            else
                ResetInstantiateTime();
        }

        else if (isSemiBossTiming == true && instantiateTime < 0f)
        {
            if (semiBossCount == 0)
            {
                semiBossCount++;
                GameObject semiBoss = Instantiate(SemiBoss1);
                semiBoss.name = "SemiBoss1";
                isSemiBossTiming = false;
            }
            else if (semiBossCount == 1)
            {
               // Debug.Log(instantiateTime);
                GameObject semiBoss = Instantiate(SemiBoss2);
                semiBoss.name = "SemiBoss2";
                semiBossCount++;
                isSemiBossTiming = false;
            }
        }

        if (isBossTiming == true && instantiateTime < 0f)
        {
            if (isBossInstantiate == false)
            {
              
                gameState = GameState.Boss;
                preGameState = gameState;
                isBossInstantiate = true;
                GameObject boss = Instantiate(Boss);
                boss.name = "Boss";
                audioSource.clip = BossStart;
                audioSource.Play();
                boss.GetComponent<Transform>().position = new Vector3(0, -120, 60);
            }


        }

    }

    void ChangeBossBGM()
    {
        if (GameObject.Find("Boss").GetComponent<BossMovement>().isStart == false && isChangeAudioClip==false)
        {
            isChangeAudioClip = true;
            audioSource.clip = BossStage;
            audioSource.Play();
        }
    }

    void ResetInstantiateTime()
    {

        if (enemyInstanCount < 5)
            instantiateTime = 4f;

        else if (enemyInstanCount < 12)
            instantiateTime = 5f;
        else if (enemyInstanCount <= 14)
            instantiateTime = 10f;
    }
    

    void MakeBossHpBar()
    {
        if(isBossModeStart==false)
        {
            GameObject hpBar = Instantiate(BossHpBar);
            hpBar.name = "HPBAR";
            isBossModeStart = true;
        }
    }
    
    void CheckMyoIsSynced()
    {
		if (myo.GetComponent<ThalmicMyo> ().armSynced == true)
			gameState = preGameState;
		

        else
        {
            preGameState = gameState;
            gameState = GameState.Pause;
        }
    }

    void CheckMyPlaneIsDie()
    {
		if( isDead==true )
        {
         //   Debug.Log("Make New");
       
            myLife--;
                preGameState = gameState;
				isInstantiateMyShip = false;
                gameState = GameState.Die;
                isDead = false;
          }
    }

    void InstantiateMyShipAgain()
    {

        Debug.Log("Make New");

        restartTime -= Time.deltaTime;

        if (myLife < 1)
        {
            gameState = GameState.GameOver;

            return;
        }


        else if (isInstantiateMyShip==false && myLife>=1  && restartTime<0f)
        {
     //       isInstantiateMyShip = true;
            isDead = false;
            GameObject myPlane = Instantiate(myShip);
            myPlane.name = "MyShip";
            gameState = preGameState;
			restartTime = 5f;
         
        }
    }

    void SceneChange()
    {
        if(sceneChangeTime<=0)
        {
            SceneManager.LoadScene(0);
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
