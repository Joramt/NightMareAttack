using UnityEngine;
using System.Collections;

public class Mine : MonoBehaviour {
	
	Transform playerT;
	GameObject itself;
	
	// Use this for initialization
	void  Awake() {
		playerT = GameObject.FindGameObjectWithTag ("Player").transform;
	}


	void OnTriggerEnter(Collider other) 
	{

		int nbExplosion = 0;

		if ( other.gameObject.tag == "Monster" && nbExplosion == 0) {

			EnemyHealth enemyHeatlh = other.GetComponent<EnemyHealth> ();
			enemyHeatlh.Death ();

			PyroExplode Pyro = this.gameObject.AddComponent("PyroExplode") as PyroExplode;

			Pyro.setTransform(transform);
			Pyro.Generate ();
			Pyro.gameObject.tag = "PyroClone";

			Destroy (this.gameObject);


		}



	}
}

