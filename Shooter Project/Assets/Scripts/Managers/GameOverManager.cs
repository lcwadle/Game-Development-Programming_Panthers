using UnityEngine;
using System.Collections;

public class GameOverManager : MonoBehaviour {

	public PlayerHealth playerHealth;       // Reference to the player's health.
	public float restartDelay = 5f;         // Time to wait before restarting the level.
	public float gameOverDelay = 1f;		// Time to wait before displaying GAME OVER.
	float restartTimer;                     // Timer to count up to restarting the level.
	GUITexture gameOver;					// Reference to the GAME OVER texture.
	GUIText playagain, quit;				// References to the PLAY AGAIN and QUIT buttons.
	
	
	void Awake ()
	{
		playerHealth = gameObject.GetComponent <PlayerHealth> ();
		gameOver = GameObject.Find("GameOver").guiTexture;
		playagain = GameObject.Find("PlayAgain").guiText;
		quit = GameObject.Find("Quit").guiText;
		gameOver.enabled = false;
		playagain.enabled = false;
		quit.enabled = false;
	}
	
	
	void Update ()
	{
		// If the player has run out of health...
		if(playerHealth.currentHealth <= 0)
		{
			restartTimer += Time.deltaTime; // .. increment a timer to count up to restarting.
			
			// .. if it reaches the restart delay...
			if(restartTimer >= gameOverDelay)
			{
				gameOver.enabled = true;		// Enable GAME OVER texture.
				playagain.enabled = true;		// Enable PLAY AGAIN button.
				quit.enabled = true;			// Enable QUIT button.
			}
		}
	}
}
