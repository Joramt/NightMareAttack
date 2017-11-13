using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
	public float playerSpeed = 6f;
	Vector3 playerMovement;
	Animator playerAC;
	Rigidbody playerRigidbody;
	int floorMask;
	float camRayLength = 100f;
	public Text pauseText;
	bool isPaused;

	void Update(){
		if (Input.GetAxisRaw ("Cancel") != 0 || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Space) ) {
			togglePause();			
		} 
	}

	void Awake()
	{
		floorMask = LayerMask.GetMask ("Floor");
		playerAC = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
		isPaused = false;

	}

	void FixedUpdate()
	{
		float hAxis = Input.GetAxisRaw ("Horizontal");
		float vAxis = Input.GetAxisRaw ("Vertical");
		PlayerMove (hAxis, vAxis);
		TurnPlayer ();
		AnimatePlayer (hAxis, vAxis);


		if ((Input.GetAxisRaw ("Mine") != 0 || Input.GetKeyDown (KeyCode.X)) && GameMaster.currentMines < GameMaster.nbMaxMines) {
			Instantiate (Resources.Load ("mine2"), transform.position, transform.rotation);
			GameMaster.currentMines++;
			MineManager.nbMine = GameMaster.currentMines;
			Input.ResetInputAxes ();
		}


	}

	void togglePause(){
		if(!isPaused){
			pauseText.color = new Color(1f, 1f, 1f, 1f);
			Time.timeScale = 0f;
			isPaused = true;
			Input.ResetInputAxes();
		}
		else if(isPaused){
			pauseText.color = new Color(1f, 1f, 1f, 0f);
			Time.timeScale = 1f;
			isPaused = false;
			Input.ResetInputAxes();
		}
	}

	void PlayerMove(float hAxis, float vAxis)
	{
		playerMovement.Set (hAxis, 0f, vAxis);
		playerMovement = playerMovement.normalized * playerSpeed * Time.deltaTime;
		playerRigidbody.MovePosition (transform.position + playerMovement);


	}
	
	void TurnPlayer()
	{
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		// Create a RaycastHit variable to store information about what was hit by the ray.
		RaycastHit floorHit;
		float yAxis = Input.GetAxisRaw("Nuu");
		float xAxis = Input.GetAxisRaw("Noo");

		if (Mathf.Abs(xAxis)>0.1 && Mathf.Abs(yAxis)>0.1){
			float angle = Mathf.Atan2(yAxis,xAxis) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0, angle, 0);
		}else

		if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
		{
			// Create a vector from the player to the point on the floor the raycast from the mouse hit.
			Vector3 playerToMouse = floorHit.point - transform.position;
			
			// Ensure the vector is entirely along the floor plane.
			playerToMouse.y = 0f;
			
			// Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			
			// Set the player's rotation to this new rotation.
			playerRigidbody.MoveRotation (newRotation);
		}
		
		
	}
	
	void AnimatePlayer(float hAxis, float vAxis)
	{
		bool walking = hAxis != 0f || vAxis !=0f ; 
		playerAC.SetBool ("IsWalking", walking);

	}
}
