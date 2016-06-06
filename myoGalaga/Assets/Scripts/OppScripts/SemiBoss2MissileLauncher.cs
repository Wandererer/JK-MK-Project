using UnityEngine;
using System.Collections;

public class SemiBoss2MissileLauncher : MonoBehaviour {

    public GameObject MissileLauncher1;
    public GameObject MissileLauncher2;
    public GameObject HomingissileLauncher1;
    public GameObject HomingissileLauncher2;
    public GameObject LaserLauncher1;

    public GameObject Missile;
    public GameObject HomingMissile;
    public GameObject Laser;

    float laserFire5Seconds = 5f;
    float fireRate = 2.0f;
    int missileCount = 0;
    int fireCount = 0;

    bool isFireMissile = false, isFireHomingMissile = false;
    public bool isFireLaser=false;

    float z;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        z = this.GetComponent<SemiBoss2Move>().z;

        if (z < 23)
        {

            if (isFireLaser == true)
            {
                FireLaser();
                laserFire5Seconds -= Time.deltaTime;
            }

            else
            {
                fireRate -= Time.deltaTime;

                GameObject laser = GameObject.Find("laser");
                if (laser != null)
                {
                    Destroy(laser);
                }
            }

            if (fireCount == 5 &&fireRate<0)
                isFireLaser = true;

            if (fireRate < 0 && missileCount < 2 && isFireLaser==false)
            {
                FireMissile();

                fireRate = 2f;
            }
            else if(missileCount==2 && isFireLaser==false &&fireRate<0)
            {
                missileCount = 0;
                FireHomingMissile();
                fireRate = 2f;
            }



  



            if (laserFire5Seconds < 0f)
            {
                GameObject laser = GameObject.Find("laser");
                Destroy(laser);
                missileCount = 0;
                fireCount = 0;
                laserFire5Seconds = 5f;
                isFireLaser = false;
                fireRate = 2f;
            }
        }
	
	}

    void FireMissile()
    {
        GameObject missile1 = Instantiate(Missile);
        GameObject missile2 = Instantiate(Missile);
        missile1.GetComponent<Transform>().position = new Vector3(
           MissileLauncher1.GetComponent<Transform>().position.x,
           MissileLauncher1.GetComponent<Transform>().position.y,
           MissileLauncher1.GetComponent<Transform>().position.z
            );
        missile2.GetComponent<Transform>().position = new Vector3(
           MissileLauncher2.GetComponent<Transform>().position.x,
           MissileLauncher2.GetComponent<Transform>().position.y,
           MissileLauncher2.GetComponent<Transform>().position.z
            );

        missileCount++;
        fireCount++;

    }

    void FireHomingMissile()
    {
        GameObject homingMissile1 = Instantiate(HomingMissile);
        GameObject homingMissile2 = Instantiate(HomingMissile);

        homingMissile1.GetComponent<Transform>().position = new Vector3(
           HomingissileLauncher1.GetComponent<Transform>().position.x,
           HomingissileLauncher1.GetComponent<Transform>().position.y,
           HomingissileLauncher1.GetComponent<Transform>().position.z
            );
        homingMissile2.GetComponent<Transform>().position = new Vector3(
           HomingissileLauncher2.GetComponent<Transform>().position.x,
           HomingissileLauncher2.GetComponent<Transform>().position.y,
           HomingissileLauncher2.GetComponent<Transform>().position.z
            );

        fireCount++;
    }

    void FireLaser()
    {
        GameObject laser = GameObject.Find("laser");
        if (laser == null)
        {
           laser = Instantiate(Laser);
            laser.name = "laser";
            laser.GetComponent<Transform>().position = new Vector3(
               LaserLauncher1.GetComponent<Transform>().position.x,
               LaserLauncher1.GetComponent<Transform>().position.y,
               LaserLauncher1.GetComponent<Transform>().position.z
                );
            laser.GetComponent<Transform>().parent = LaserLauncher1.GetComponent<Transform>();
        }
    }
}
