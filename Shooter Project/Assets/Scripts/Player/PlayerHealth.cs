using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    public PowerupIcon powerupIcon;
	public int startingHealth = 100;                            // The amount of health the player starts the game with.
	public int currentHealth;                                   // The current health the player has.
	PlayerMovement playerMovement;                              // Reference to the player's movement.
	PlayerShooting playerShooting;                              // Reference to the PlayerShooting script.
	bool isDead;                                                // Whether the player is dead.
	bool damaged;                                               // True when the player gets damaged.
	Animator anim;
	GUIText healthText;
    bool godMode;
    float powerupTimer;
	
	void Awake ()
	{
		anim = GetComponent <Animator> ();
		healthText = GameObject.Find("CurrentHealth").guiText;
		playerMovement = GetComponent <PlayerMovement> ();
		playerShooting = GetComponentInChildren <PlayerShooting> ();
		currentHealth = startingHealth; // Set the initial health of the player.
        godMode = false;
	}
	
	
	void Update ()
	{
		damaged = false; // Reset the damaged flag.
        powerupTimer += Time.deltaTime;

        if(powerupTimer >= powerupIcon.powerupTime)
        {
            godMode = false;
        }
	}
	
	
	public void TakeDamage (int amount)
	{
        if (godMode == false)
        {
            damaged = true; // Set the damaged flag.
            currentHealth -= amount; // Reduce the current health by the damage amount.

            int newHealth = int.Parse(healthText.text) - 10;
            healthText.text = "" + newHealth;

            // If the player has lost all it's health and the death flag hasn't been set yet...
            if (currentHealth <= 0 && !isDead)
            {
                // ... it should die.
                Death();
            }
        }
	}

    public void AddHealth(int amount)
    {
        if (currentHealth < 100)
        {
            currentHealth += amount;  // Add health amount to player
            int newHealth = int.Parse(healthText.text) + 10;
            healthText.text = "" + newHealth;
        }
    }

    public void powerupThree()
    {
        powerupTimer = 0f;
        godMode = true;
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
