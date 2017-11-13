using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	GameObject player;
	Transform playerT;
	PlayerHealth playerHealth;

	public int heal = 20;
	public float healthLifeTime = 20.0f;


	void Awake () {
		playerT = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = playerT.GetComponent<PlayerHealth> ();
		player = GameObject.FindGameObjectWithTag ("Player");

		Object.Destroy(this.gameObject, healthLifeTime);
		
	}



	// Update is called once per frame
	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject == player && playerHealth.currentHealth < playerHealth.startingHealth )
		{
			playerHealth.GetHeal(heal);
			Destroy(this.gameObject);
		}
	}
}
