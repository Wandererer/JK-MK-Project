using UnityEngine;
using System.Collections;

public class DestroyEffect : MonoBehaviour {

    float deleteTime = 3f;


	void Update ()
	{
        deleteTime -= Time.deltaTime;

        if (deleteTime < 0)
            Destroy(this.gameObject);
    
		if( this.GetComponentInChildren<ParticleSystem>().time>2f)
		   Destroy(this.gameObject);
	
	}
}
