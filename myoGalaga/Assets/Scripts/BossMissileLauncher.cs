using UnityEngine;
using System.Collections;

public class BossMissileLauncher : MonoBehaviour {

	public GameObject simpleMissile;
	public GameObject homingMissile;

	int [,] simpleMissilePattern =new int[,]
	{
		{2,0,1,0,0,0},
		{2,3,4,0,0,0},
		{3,0,1,2,0,0},
		{3,2,3,4,0,0},
		{5,0,1,2,3,4}
	};

	int sPatternNum;
	int previousPattern=-1;

	public GameObject[] simpleMissileLauncherMatrix;
	public GameObject[] hommingMissileLauncherMatrix;

    public GameObject[] laserLauncerMatrix;
    public GameObject[] laserObject;

	bool isFireMissile=true;
	public bool isFireLaser=true;
	bool isHomingFire=false;

    bool isInstantiateLaserObject = false;
    bool isDifficultUp1 = false;
    bool isDifficultUp2 = false;
    bool isStart;

    float fixFireRate = 0.7f;
    float fixHommingFireRate = 0.5f;
    float fixChangePatternTime = 6.0f;
	float firerate=0.7f;
	float homingFireRate=0.5f;
	float changeSimplePatternTime=-1f;
    float laserTime = 6f;
    float maxHp;
    float currentHp;
    float hpRate;



	int homingCount=0;
    int laserObjectCount = 0;
    int fireCount = 0;

    ArrayList destroyArray;

	// Use this for initialization
	void Start () {
		selectSimpleMissilePattern ();
		previousPattern = sPatternNum;
        maxHp = GetComponent<EnemyStatus>().hp;
        destroyArray = new ArrayList();
	}
	
	// Update is called once per frame
	void Update () {
        isStart = this.GetComponent<BossMovement>().isStart;



        currentHp = GetComponent<EnemyStatus>().hp;
        hpRate = currentHp / maxHp;
        if (isStart == false)
        {
            firerate -= Time.deltaTime;
            changeSimplePatternTime -= Time.deltaTime;



            MoveSpeedAndFireRateUpByHP();
            ChangeMissilePatternByCount();


            if (changeSimplePatternTime < 0f)
            {
                previousPattern = sPatternNum;
                selectSimpleMissilePattern();
                ChangePatternIfPastPatternSame();
                changeSimplePatternTime = fixChangePatternTime;
                fireCount++;
            }


            if (isFireMissile == true)
            {
                BossSimpleMissileFire();
            }


            if (isHomingFire == true)
            {

                BossHommingMissileFire();
            }

            if (isFireLaser == true)
            {
                laserTime -= Time.deltaTime;
                CheckLaserTime();
                BossFireLaser();
            }
            else
            {
                DestroyLaser();
            }

        }


	}

    void ChangeMissilePatternByCount()
    {
        if (fireCount == 2 || fireCount == 5)
            isHomingFire = true;

        else if(fireCount==7 && isFireLaser==false)
        {
            isFireLaser = true;
        }
    }

    void CheckLaserTime()
    {
        if(laserTime<=0f)
        {
            isFireLaser = false;
            laserTime = 6f;
            fireCount = 0;
        }
    }
    

    void BossSimpleMissileFire()
    {
        if (firerate < 0f)
        {
            int limitSimpleMatrix = simpleMissilePattern[sPatternNum, 0];
            for (int i = 1; i <= limitSimpleMatrix; i++)
            {
                GameObject simpleFire1 = Instantiate(simpleMissile);
                simpleFire1.GetComponent<Transform>().position = new Vector3(
                    simpleMissileLauncherMatrix[simpleMissilePattern[sPatternNum, i]].GetComponent<Transform>().position.x,
                    simpleMissileLauncherMatrix[simpleMissilePattern[sPatternNum, i]].GetComponent<Transform>().position.y,
                    simpleMissileLauncherMatrix[simpleMissilePattern[sPatternNum, i]].GetComponent<Transform>().position.z
                );


            }

            firerate = fixFireRate;
        }
    }

    void MoveSpeedAndFireRateUpByHP()
    {
        if (hpRate < 0.6 && isDifficultUp1==false)
        {
            isDifficultUp1 = true;
            this.GetComponent<BossMovement>().bossMoveSpeed += 0.1f;
            fixFireRate -= 0.1f;
            fixHommingFireRate -= 0.1f;
            fixChangePatternTime -= 1f;
            laserObjectCount++;
        }

        else if(hpRate<0.3&&isDifficultUp2==false)
        {
            isDifficultUp2 = true;
            this.GetComponent<BossMovement>().bossMoveSpeed += 0.1f;
            fixFireRate -= 0.1f;
            fixChangePatternTime -= 0.1f;
            fixHommingFireRate -= 0.1f;
            laserObjectCount++;
        }
    }

    void BossHommingMissileFire()
    {
        homingFireRate -= Time.deltaTime;

        if (homingFireRate < 0f)
        {

            GameObject homingFire = Instantiate(homingMissile);
            homingFire.GetComponent<Transform>().rotation = new Quaternion(-180, 0, 0, 0);
            homingFire.GetComponent<Transform>().position = new Vector3(
            hommingMissileLauncherMatrix[homingCount].GetComponent<Transform>().position.x,
            hommingMissileLauncherMatrix[homingCount].GetComponent<Transform>().position.y,
            hommingMissileLauncherMatrix[homingCount].GetComponent<Transform>().position.z);
            homingCount++;
            homingFireRate = fixHommingFireRate;


        }

        if (homingCount == 8)
        {
            homingCount = 0;
            isHomingFire = false;
            fireCount++;
        }
       
    }

    void BossFireLaser()
    {
        if(isInstantiateLaserObject==false)
        {
            for(int i=0;i<laserLauncerMatrix.Length;i++)
            {
                GameObject Laser = Instantiate(laserObject[laserObjectCount]);
                Laser.name = "Laser" + (i+1);
                Laser.GetComponent<Transform>().position = new Vector3(
                    laserLauncerMatrix[i].GetComponent<Transform>().position.x,
                    laserLauncerMatrix[i].GetComponent<Transform>().position.y,
                    laserLauncerMatrix[i].GetComponent<Transform>().position.z
               );

                Laser.GetComponent<Transform>().parent = laserLauncerMatrix[i].GetComponent<Transform>();
                destroyArray.Add(Laser.name);

            }

            isInstantiateLaserObject = true;
        }

        
    }


    void DestroyLaser()
    {
        if (isInstantiateLaserObject == true)
        {
            foreach (string obj in destroyArray)
            {
                Destroy(GameObject.Find(obj));
            }

            isInstantiateLaserObject = false;
        }
    }

    void SetIsFireMissileFalse()
    {
        isFireMissile = false;
    }

	void selectSimpleMissilePattern()
	{
		sPatternNum = Random.Range (0, 5);

	}

	void ChangePatternIfPastPatternSame()
	{
		while (previousPattern == sPatternNum) {
			sPatternNum = Random.Range (0, 5);
		}
	}

}

