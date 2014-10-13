using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {

	public int damagePerShot = 20;                  // The damage inflicted by each bullet.
	public float timeBetweenBullets = 0.15f;        // The time between each shot.
	public float range = 100f;                      // The distance the gun can fire.
	
	float timer;                                    // A timer to determine when to fire.
	Ray shootRay;                                   // A ray from the gun end forwards.
	RaycastHit shootHit;                            // A raycast hit to get information about what was hit.
	int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.
	LineRenderer gunLine;                           // Reference to the line renderer.
	Light gunLight;                                 // Reference to the light component.
	float effectsDisplayTime = 0.2f;                // The proportion of the timeBetweenBullets that the effects will display for.
	
	void Awake ()
	{
		shootableMask = LayerMask.GetMask ("Shootable"); // Create a layer mask for the Shootable layer.
		
		// Set up the references.
		gunLine = GetComponent <LineRenderer> ();
		gunLight = GetComponent<Light> ();
	}
	
	void Update ()
	{
		timer += Time.deltaTime; // Add the time since Update was last called to the timer.
		
		// If the Fire1 button is being press and it's time to fire...
		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets)
		{
			Shoot (); // ... shoot the gun.
		}
		
		// If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
		if(timer >= timeBetweenBullets * effectsDisplayTime)
		{
			DisableEffects (); // ... disable the effects.
		}
	}
	
	public void DisableEffects ()
	{
		// Disable the line renderer and the light.
		gunLine.enabled = false;
		gunLight.enabled = false;
	}
	
	void Shoot ()
	{
		timer = 0f; // Reset the timer.
		gunLight.enabled = true; // Enable the light.
		
		// Enable the line renderer and set it's first position to be the end of the gun.
		// Currently this is set to the player object, so the origin starts at the feet. Will be changed when a new player mdoel is created.
		gunLine.enabled = true;
		gunLine.SetPosition (0, transform.position);
		
		// Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
		shootRay.origin = transform.position;
		shootRay.direction = transform.forward;
		
		// Perform the raycast against gameobjects on the shootable layer and if it hits something...
		if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
		{
			EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> (); // Try and find an EnemyHealth script on the gameobject hit.
			
			// If the EnemyHealth component exist...
			if(enemyHealth != null)
			{
				enemyHealth.TakeDamage (damagePerShot, shootHit.point); // ... the enemy should take damage.
			}
			gunLine.SetPosition (1, shootHit.point); // Set the second position of the line renderer to the point the raycast hit.
		} 
		// If the raycast didn't hit anything on the shootable layer...
		else
		{
			gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range); // ... set the second position of the line renderer to the fullest extent of the gun's range.
		}
	}
}
