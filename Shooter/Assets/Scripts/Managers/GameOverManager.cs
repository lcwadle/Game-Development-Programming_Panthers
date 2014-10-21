using UnityEngine;
using System.Collections;

public class GameOverManager : MonoBehaviour {

	public PlayerHealth playerHealth;       // Reference to the player's health.
	public float restartDelay = 5f;         // Time to wait before restarting the level
	float restartTimer;                     // Timer to count up to restarting the level
	
	
	void Awake ()
	{
		playerHealth = gameObject.GetComponent <PlayerHealth> ();
	}
	
	
	void Update ()
	{
		// If the player has run out of health...
		if(playerHealth.currentHealth <= 0)
		{
			//restartTimer += Time.deltaTime; // .. increment a timer to count up to restarting.
			
			// .. if it reaches the restart delay...
			//if(restartTimer >= restartDelay)
			//{
				Application.LoadLevel(Application.loadedLevel); // .. then reload the currently loaded level.
			//}
		}
	}
}
