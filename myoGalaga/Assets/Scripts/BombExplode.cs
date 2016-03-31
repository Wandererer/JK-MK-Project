using UnityEngine;
using System.Collections;

public class BombExplode : MonoBehaviour {

	float bombTransformZ;
	float bombTransformX;
	float bombTransformY;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		bombTransformZ = this.GetComponent<Transform> ().position.z;
		bombTransformX = this.GetComponent<Transform> ().position.x;
		bombTransformY = this.GetComponent<Transform> ().position.y;

		this.GetComponent<Transform> ().position = new Vector3 (bombTransformX, bombTransformY, bombTransformZ += 0.4f);

		if (bombTransformZ >= 20) {
			//particle effect and sound add later
			Destroy(this.gameObject);
		}
		
	}
}
