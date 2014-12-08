using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour {

    GameObject player;                          // Reference to the player GameObject.
    PlayerHealth playerHealth;                  // Reference to the player's health.
    public int healthpackValue;                        // Reference to the amount fo health returned

    // Use this for initialization
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    void OnTriggerEnter(Collider other)
    {
        // If the entering collider is the player...
        if (other.gameObject == player)
        {
            gameObject.renderer.enabled = false;
            playerHealth.AddHealth(healthpackValue);
        }
    }
}
