using UnityEngine;
using System.Collections;

public class ground1_move : MonoBehaviour
{
    float fireSpeed = 3f;
    private int block_count = 0;
    public static float BLOCK_WIDTH = 6000.0f; // 맵의 z축 방향의 길이
    public static int BLOCK_NUM_IN_SCREEN = 2; // 화면 내에 들어가는 맵의 개수
    public GameObject ground2move; // ground2 생성

    // Use this for initialization
    void Start()
    {

    }

    void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float z = this.GetComponent<Transform>().position.z; //get from this object transformation.z info
        float x = this.GetComponent<Transform>().position.x;
        float y = this.GetComponent<Transform>().position.y;

        this.GetComponent<Transform>().position = new Vector3(this.GetComponent<Transform>().position.x, this.GetComponent<Transform>().position.y, z -= fireSpeed);

        if (z<-2000)
        {
            GameObject ground = GameObject.Find("Terrain-2"); 

            if (ground==null)
            {
                ground = Instantiate(ground2move);
              //  Debug.Log("ground2move been Instantiated");
                ground.name = "Terrain-2";
                ground.GetComponent<Transform>().position = new Vector3(x, y, z + 3000);
            }

            if(z < -6000)
                Destroy(this.gameObject);  
        }
    }
}
