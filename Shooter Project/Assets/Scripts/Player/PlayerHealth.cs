using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public int startingHealth = 100;                            // The amount of health the player starts the game with.
	public int currentHealth;                                   // The current health the player has.
	PlayerMovement playerMovement;                              // Reference to the player's movement.
	PlayerShooting playerShooting;                              // Reference to the PlayerShooting script.
	bool isDead;                                                // Whether the player is dead.
	bool damaged;                                               // True when the player gets damaged.
	Animator anim;
	
	
	void Awake ()
	{
		anim = GetComponent <Animator> ();
		playerMovement = GetComponent <PlayerMovement> ();
		playerShooting = GetComponentInChildren <PlayerShooting> ();
		currentHealth = startingHealth; // Set the initial health of the player.
	}
	
	
	void Update ()
	{
		damaged = false; // Reset the damaged flag.
	}
	
	
	public void TakeDamage (int amount)
	{
		damaged = true; // Set the damaged flag.
		currentHealth -= amount; // Reduce the current health by the damage amount.
		
		// If the player has lost all it's health and the death flag hasn't been set yet...
		if(currentHealth <= 0 && !isDead)
		{
			// ... it should die.
			Death ();
		}
	}
	
	
	void Death ()
	{
		isDead = true; // Set the death flag so this function won't be called again.
		playerShooting.DisableEffects (); // Turn off any remaining shooting effects.
		
		// Turn off the movement and shooting scripts.
		playerMovement.enabled = false;
		playerShooting.enabled = false;
		
		GameObject.Destroy(transform.GetChild(0).gameObject);
		
		anim.SetTrigger ("Die");
	} 
}
