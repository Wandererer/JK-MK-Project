using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
   
    public float healthPercentage;

    public Texture2D frame;
    public Rect framePosition;

    public float horizontalDistance;
    public float verticalDistance;
    public float width;
    public float height;

    public Texture2D healthBar;
    public Rect healthBarPosition;

    public float HP;
    public float MaxHP;
    GameObject Boss;
    void Start()
    {
        Boss = GameObject.Find("Boss");

        

        MaxHP = Boss.GetComponent<EnemyStatus>().hp;
        HP = MaxHP;
    }


    void Update()
    {
        Boss = GameObject.Find("Boss");

        HP = Boss.GetComponent<EnemyStatus>().hp;
        healthPercentage = (float)HP / MaxHP;
      
    }

    void OnGUI()
    {
        drawFrame();
        drawBar();
    }


    void drawFrame()
    {       
        framePosition.x = (Screen.width - framePosition.width) / 2;
        framePosition.width = Screen.width * 0.7f;
        framePosition.height = Screen.height * 0.09f;

        GUI.DrawTexture(framePosition, frame);
    }

    void drawBar()
    {
        if(healthPercentage < 0)
        {
            healthPercentage = 0;
        }

        healthBarPosition.x = framePosition.x + framePosition.width * horizontalDistance;
        healthBarPosition.y = framePosition.y + framePosition.height * verticalDistance;
        healthBarPosition.width = framePosition.width * width * healthPercentage;
        healthBarPosition.height = framePosition.height*height;

        GUI.DrawTexture(healthBarPosition, healthBar);
    }
}
