using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Pose = Thalmic.Myo.Pose;
 using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour {

    public GameObject Myo;
    public Transform canvas;
    public Transform MyShip;
     public Transform Enemy;
    ThalmicMyo thalmicMyo;

    public bool menu_IsCreated = false;

    public float waitingTime = 10.0f;

    void Update()
    {
        thalmicMyo = Myo.GetComponent<ThalmicMyo>();

        // pause menu 생성

        // F10키를 눌렀을 경우
        if (Input.GetKeyDown(KeyCode.F10))
        {
            if (Time.timeScale == 1)
                Time.timeScale = 0;
            else if(Time.timeScale == 0)
                Time.timeScale = 1;

            Creat_PauseMenu();
        }

    }

    public void Creat_PauseMenu()
    {
       if(canvas.gameObject.activeInHierarchy == false)
       {
            canvas.gameObject.SetActive(true);
       }
       else
       {    
           canvas.gameObject.SetActive(false);
            menu_IsCreated = false;
        }
    }
    public void Quit()
    {
        SceneManager.LoadScene(0);
    }

    public void NewGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        Creat_PauseMenu();
    }
    
}
