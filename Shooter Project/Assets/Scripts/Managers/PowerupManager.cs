using UnityEngine;
using System.Collections;

public class PowerupManager : MonoBehaviour {

    public PlayerHealth playerHealth;       // Reference to the player's heatlh.
    public PowerupIcon powerupIcon;
    public float spawnTime = 3f;            // How long between each spawn.

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime); // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
    }

    void Spawn()
    {
        // If the player has no health left...
        if (playerHealth.currentHealth <= 0f)
        {
            return; // ... exit the function.
        }
        gameObject.renderer.enabled = true;
        gameObject.collider.enabled = true;
    }
}
