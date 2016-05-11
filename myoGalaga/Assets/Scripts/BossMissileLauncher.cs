using UnityEngine;
using System.Collections;

public class BossMissileLauncher : MonoBehaviour {

	public GameObject simpleMissileLauncher1;
	public GameObject simpleMissileLauncher2;

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

	bool isFireMissile=false;
	bool isFireLaser=false;
	bool isHomingFire=true;

    bool isInstantiateLaserObject = false;
   

	float firerate=0.7f;
	float homingFireRate=0.5f;
	float changeSimplePatternTime=-1f;

	int homingCount=0;
    int laserObjectCount = 0;

	// Use this for initialization
	void Start () {
		selectSimpleMissilePattern ();
		previousPattern = sPatternNum;
	}
	
	// Update is called once per frame
	void Update () {
		firerate -= Time.deltaTime;
		changeSimplePatternTime -= Time.deltaTime;

		if (changeSimplePatternTime < 0f) {
			previousPattern = sPatternNum;
			selectSimpleMissilePattern ();
			ChangePatternIfPastPatternSame ();
			changeSimplePatternTime = 6f;
		}


		if (isFireMissile == true) {
            BossSimpleMissileFire();
		}


		if (isHomingFire == true)
		{

            BossHommingMissileFire();
		}

        if(isFireLaser ==true)
        {
            BossFireLaser();
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

            firerate = 0.7f;
        }
    }

    void BossHommingMissileFire()
    {
        homingFireRate -= Time.deltaTime;

        if (homingFireRate < 0f)
        {

            GameObject homingFire = Instantiate(homingMissile);
            homingFire.GetComponent<Transform>().position = new Vector3(
            hommingMissileLauncherMatrix[homingCount].GetComponent<Transform>().position.x,
            hommingMissileLauncherMatrix[homingCount].GetComponent<Transform>().position.y,
            hommingMissileLauncherMatrix[homingCount].GetComponent<Transform>().position.z);
            homingCount++;
            homingFireRate = 0.5f;
        }



        if (homingCount == 8)
        {

            homingCount = 0;
            isHomingFire = false;

        }
    }

    void BossFireLaser()
    {
        if(isInstantiateLaserObject==false)
        {
            for(int i=0;i<laserLauncerMatrix.Length;i++)
            {
                GameObject Laser = Instantiate(laserObject[i]);
                Laser.name = "Laser" + (i+1);
                Laser.GetComponent<Transform>().position = new Vector3(
                    laserLauncerMatrix[i].GetComponent<Transform>().position.x,
                    laserLauncerMatrix[i].GetComponent<Transform>().position.y,
                    laserLauncerMatrix[i].GetComponent<Transform>().position.z
               );

                Laser.GetComponent<Transform>().parent = laserLauncerMatrix[i].GetComponent<Transform>();
                
                
                laserObjectCount++;

            }

            if (laserObjectCount == 2)
                isInstantiateLaserObject = true;
        }


        
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

