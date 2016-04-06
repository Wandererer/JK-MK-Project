using UnityEngine;
using System.Collections;

public class ShieldEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<Renderer> ().material.color=new Color(0.0f,0.0f,0.3f,100.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
