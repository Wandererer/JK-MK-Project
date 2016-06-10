using UnityEngine;
using System.Collections;

public class PowerItemMove : MonoBehaviour {

    float x, y, z;
    float moveToX, moveToY;

    bool isStart=false;
    bool isMove = false;
	// Use this for initialization
	void Start () {
        ChooseStartMove();
	}

    void FixedUpdate() { }

	// Update is called once per frame
	void Update () {
        x = this.GetComponent<Transform>().position.x;
        y = this.GetComponent<Transform>().position.y;

        CheckBorderHit();

        if (isMove == true)
            Moveitem();
        else
            ChooseStartMove();

	}
    void ChooseStartMove()
    {
        isMove = true;
        int number = Random.Range(0, 5);
 
        switch(number)
        {


            case 1:
                moveToX = Random.Range(-7, 7);
                moveToY = 4;
                break;

            case 2:
                moveToX =6;
                moveToY = Random.Range(-2,5);
                break;

            case 3:
                moveToX = Random.Range(-7, 7);
                moveToY =-1;
                break;

            case 4:
                moveToX = -6;
                moveToY = Random.Range(-2, 5);
                break;

            default:
              
                break;
        }

        //Debug.Log(number + " " + moveToX + " " + moveToY);
    }


    void OnCollisionEnter(Collision col)
    {
        if(col.transform.tag=="My")
        {
            this.GetComponent<Rigidbody>().isKinematic = true;
            col.transform.GetComponent<MyShipControl>().power++;
            Destroy(this.gameObject);
        }
    }

    void Moveitem()
    {
        this.GetComponent<Transform>().position = Vector3.MoveTowards(this.GetComponent<Transform>().position, new Vector3(moveToX, moveToY, -3), 0.1f);
    }


    void CheckBorderHit()
    {
        if (x==moveToX && y==moveToY)
            isMove = false;
        else
            isMove = true;
    }

}
