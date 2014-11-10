using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;            // The speed that the player will move at.
	
	Vector3 movement;                   // The vector to store the direction of the player's movement.
	Animator anim;                      // Reference to the animator component.
	Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
	int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
	float camRayLength = 100f;          // The length of the ray from the camera into the scene.
	
	void Awake ()
	{
		// Create a layer mask for the floor layer.
		floorMask = LayerMask.GetMask ("Floor");
		
		// Set up references.
		anim = GetComponent <Animator> ();
		playerRigidbody = GetComponent <Rigidbody> ();
	}
	
	
	void FixedUpdate ()
	{
		// Store the input axes.
		bool a = Input.GetKey("a");
		bool d = Input.GetKey("d");
		bool w = Input.GetKey("w");
		bool s = Input.GetKey("s");
		
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		
		// Move the player around the scene.
		Move (h, v);
		
		// Turn the player to face the mouse cursor.
		Turning ();
		
		// Animate the player.
		Animating (a, d, w, s);
	}
	
	void Move (float h, float v)
	{
		// Set the movement vector based on the axis input.
		movement.Set (h, 0f, v);
		
		// Normalise the movement vector and make it proportional to the speed per second.
		movement = movement.normalized * speed * Time.deltaTime;
		
		// Move the player to it's current position plus the movement.
		playerRigidbody.MovePosition (transform.position + movement);
	}
	
	void Turning ()
	{
		// Create a ray from the mouse cursor on screen in the direction of the camera.
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		// Create a RaycastHit variable to store information about what was hit by the ray.
		RaycastHit floorHit;
		
		// Perform the raycast and if it hits something on the floor layer...
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
	
	void Animating (bool a, bool d, bool w, bool s)
	{
		bool strafingL = a;
		bool strafingR = d;
		bool running = w;
		bool backing = s;
		
		if(transform.eulerAngles.y >= 315 || transform.eulerAngles.y <= 45)
		{
			strafingL = a;
			strafingR = d;
			running = w;
			backing = s;
		}
		if(transform.eulerAngles.y >= 45 && transform.eulerAngles.y <= 135)
		{
			strafingL = w;
			strafingR = s;
			running = d;
			backing = a;
		}
		if(transform.eulerAngles.y >= 135 && transform.eulerAngles.y <= 225)
		{
			strafingL = d;
			strafingR = a;
			running = s;
			backing = w;
		}
		if(transform.eulerAngles.y >= 225 && transform.eulerAngles.y <= 315)
		{
			strafingL = s;
			strafingR = w;
			running = a;
			backing = d;
		}
		
		anim.SetBool ("IsLeft", strafingL);
		anim.SetBool ("IsRight", strafingR);
		anim.SetBool ("IsRunning", running);
		anim.SetBool ("IsBacking", backing);
	}
}
