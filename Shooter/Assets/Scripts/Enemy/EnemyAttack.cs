using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

	public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
	public int attackDamage = 10;               // The amount of health taken away per attack.
	GameObject player;                          // Reference to the player GameObject.
	PlayerHealth playerHealth;                  // Reference to the player's health.
	EnemyHealth enemyHealth;                    // Reference to this enemy's health.
	bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
	float timer;                                // Timer for counting up to the next attack.
	
	
	void Awake ()
	{
		// Setting up the references.
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();
		enemyHealth = GetComponent<EnemyHealth>();
	}
	
	
	void OnTriggerEnter (Collider other)
	{
		// If the entering collider is the player...
		if(other.gameObject == player)
		{
			playerInRange = true; // ... the player is in range.
		}
	}
	
	
	void OnTriggerExit (Collider other)
	{
		// If the exiting collider is the player...
		if(other.gameObject == player)
		{
			playerInRange = false; // ... the player is no longer in range.
		}
	}
	
	
	void Update ()
	{
		timer += Time.deltaTime; // Add the time since Update was last called to the timer.
		
		// If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
		if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
		{
			Attack (); // ... attack.
		}
	}
	
	
	void Attack ()
	{
		timer = 0f; // Reset the timer.
		
		// If the player has health to lose...
		if(playerHealth.currentHealth > 0)
		{
			playerHealth.TakeDamage (attackDamage); // ... damage the player.
		}
	}
}
