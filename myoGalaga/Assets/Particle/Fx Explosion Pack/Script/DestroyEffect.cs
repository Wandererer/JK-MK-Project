using UnityEngine;
using System.Collections;

public class DestroyEffect : MonoBehaviour {



	void Update ()
	{

  
		if( this.GetComponentInChildren<ParticleSystem>().time>2f)
		   Destroy(this.gameObject);
	
	}
}
