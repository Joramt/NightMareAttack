using UnityEngine;
using System.Collections;


public class Bullet : MonoBehaviour {
	
	GameObject player;
	Transform playerT;
	public float bulletLifeTime = 20.0f;

	
	
	void Awake () {
		playerT = GameObject.FindGameObjectWithTag ("Player").transform;
		player = GameObject.FindGameObjectWithTag ("Player");
		Destroy(this.gameObject, bulletLifeTime);	
	}
	

	// Update is called once per frame
	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject == player )
		{   
			PlayerShooting gunBarrel = (PlayerShooting) playerT.GetChild(1).gameObject.GetComponent<PlayerShooting>();
			Light gunRay =  playerT.GetChild(1).gameObject.GetComponent<Light>();
			gunBarrel.damagePerShot = 50;
			gunBarrel.timeBetweenBullets = 0.05f;
			gunBarrel.bulletPicked = true;
			gunBarrel.resetInitial(gunRay);
			Destroy(this.gameObject);
		}
	}
}
