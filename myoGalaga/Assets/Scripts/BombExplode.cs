using UnityEngine;
using System.Collections;

public class BombExplode : MonoBehaviour {

    public GameObject bombEffect;
	float bombTransformZ;
	float bombTransformX;
	float bombTransformY;
    bool isParticle = false;
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
           
            GameObject[] getAllEnemy=GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < getAllEnemy.Length;i++ )
            {
                getAllEnemy[i].GetComponent<EnemyStatus>().hp -= 100;
            }

                Destroy(this.gameObject);

            if (isParticle == false)
            {
                isParticle = true;
                GameObject particle = Instantiate(bombEffect);
                particle.GetComponent<Transform>().position =
                    new Vector3(this.GetComponent<Transform>().position.x,
                        this.GetComponent<Transform>().position.y,
                        this.GetComponent<Transform>().position.z);
            }
		}
		
	}
}
