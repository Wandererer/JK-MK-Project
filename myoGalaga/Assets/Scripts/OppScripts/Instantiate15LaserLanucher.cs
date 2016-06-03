using UnityEngine;
using System.Collections;

public class Instantiate15LaserLanucher : MonoBehaviour {

	public GameObject LaserLauner;
	public GameObject LaserObject;
	Renderer color;
	float red;
	float z;
	bool isInstantiateLaser=false;


	// Use this for initialization
	void Start () {
		color = this.GetComponentInChildren<MeshRenderer> ();

	}


	void FixedUpdate()
	{

	}

	// Update is called once per frame
	void Update () {
		z = this.GetComponent<Transform> ().position.z;

		if (z <= 16) {
			if (red < 40) {
				red = color.material.color.r;
				this.GetComponentInChildren<MeshRenderer> ().material.color = new Color (red += 1f, color.material.color.g, color.material.color.b);
			//	Debug.Log (red);
			} else {
				if (isInstantiateLaser == false) {
					isInstantiateLaser = true;
					GameObject laser = Instantiate (LaserObject);
					laser.GetComponent<Transform> ().position = new Vector3 (LaserLauner.GetComponent<Transform> ().position.x,
						LaserLauner.GetComponent<Transform> ().position.y,
						LaserLauner.GetComponent<Transform> ().position.z);

					laser.GetComponent<Transform> ().parent = LaserLauner.GetComponent<Transform> ();
				}
			}
		}
	}
}
