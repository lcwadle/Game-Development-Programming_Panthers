using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;            // The speed that the player will move at.
	Vector3 movement;                   // The vector to store the direction of the player's movement.
	Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
	int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
	float camRayLength = 100f;          // The length of the ray from the camera into the scene.
	
	void Awake ()
	{
		floorMask = LayerMask.GetMask ("Floor"); // Create a layer mask for the floor layer.
		playerRigidbody = GetComponent <Rigidbody> (); // Setup reference to the Rigidbody component.
	}
	
	
	void FixedUpdate ()
	{
		// Store the input axes.
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		
		Move (h, v); // Move the player around the scene.
		Turning (); // Turn the player to face the mouse cursor.
	}
	
	void Move (float h, float v)
	{
		movement.Set (h, 0f, v); // Set the movement vector based on the axis input.
		movement = movement.normalized * speed * Time.deltaTime; // Normalise the movement vector and make it proportional to the speed per second.
		playerRigidbody.MovePosition (transform.position + movement); // Move the player to it's current position plus the movement.
	}
	
	void Turning ()
	{
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition); // Create a ray from the mouse cursor on screen in the direction of the camera.
		RaycastHit floorHit; // Create a RaycastHit variable to store information about what was hit by the ray.
		
		// Perform the raycast and if it hits something on the floor layer...
		if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
		{
			Vector3 playerToMouse = floorHit.point - transform.position; // Create a vector from the player to the point on the floor the raycast from the mouse hit.
			playerToMouse.y = 0f; // Ensure the vector is entirely along the floor plane.
			Quaternion newRotation = Quaternion.LookRotation (playerToMouse); // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
			playerRigidbody.MoveRotation (newRotation); // Set the player's rotation to this new rotation.
		}
	}
}
