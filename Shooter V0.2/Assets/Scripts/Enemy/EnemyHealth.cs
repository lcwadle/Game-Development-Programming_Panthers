using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public int startingHealth = 100;            // The amount of health the enemy starts the game with.
	public int currentHealth;                   // The current health the enemy has.
	public float sinkSpeed = 2.5f;              // The speed at which the enemy sinks through the floor when dead.
	public int scoreValue = 10;                 // The amount added to the player's score when the enemy dies.
	CapsuleCollider capsuleCollider;            // Reference to the capsule collider.
	bool isDead;                                // Whether the enemy is dead.
	bool isSinking;                             // Whether the enemy has started sinking through the floor.
	
	
	void Awake ()
	{
		capsuleCollider = GetComponent <CapsuleCollider> (); // Setup collider component.
		currentHealth = startingHealth; // Setting the current health when the enemy first spawns.
	}
	
	void Update ()
	{
		if(isDead)
			StartSinking ();
		// If the enemy should be sinking...
		if(isSinking)
			transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime); // ... move the enemy down by the sinkSpeed per second.
	}
	
	
	public void TakeDamage (int amount, Vector3 hitPoint)
	{
		// If the enemy is dead...
		if(isDead)
			return; // ... no need to take damage so exit the function.
			
		currentHealth -= amount; // Reduce the current health by the amount of damage sustained.
		
		// If the current health is less than or equal to zero...
		if(currentHealth <= 0)
		{
			Death (); // ... the enemy is dead.
		}
	}
	
	
	void Death ()
	{
		isDead = true; // The enemy is dead.
		capsuleCollider.isTrigger = true; // Turn the collider into a trigger so shots can pass through it.
	}
	
	
	public void StartSinking ()
	{
		GetComponent <NavMeshAgent> ().enabled = false; // Find and disable the Nav Mesh Agent.
		GetComponent <Rigidbody> ().isKinematic = true; // Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
		isSinking = true; // The enemy should no sink.
		Destroy (gameObject, 2f); // After 2 seconds destroy the enemy.
	}
}
