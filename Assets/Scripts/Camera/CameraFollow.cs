using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform player;
	public float smoothing = 5f;
	
	Vector3 gameOffset;
	
	void Start(){
		gameOffset = transform.position - player.position; 
	}
	
	void FixedUpdate(){
		Vector3 cameraPos = player.position + gameOffset;
		transform.position = Vector3.Lerp (transform.position, cameraPos, smoothing * Time.deltaTime);
	}
}

