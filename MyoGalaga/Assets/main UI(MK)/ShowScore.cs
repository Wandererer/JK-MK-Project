using UnityEngine;
using System.Collections;

public class ShowScore : MonoBehaviour {


    int score;
    GameObject gm;
	// Use this for initialization
	void Start () {
        gm = GameObject.Find("GameController");
	}
	
    void OnGUI()
    { 
    }
}
