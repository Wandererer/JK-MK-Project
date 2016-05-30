using UnityEngine;
using System.Collections;

public class DestroyMissileParticle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (this.GetComponentInChildren<ParticleSystem>().time > 1.5f)
            Destroy(this.gameObject);
	}
}
