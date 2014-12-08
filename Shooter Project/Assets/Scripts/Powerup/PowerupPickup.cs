using UnityEngine;
using System.Collections;

public class PowerupPickup : MonoBehaviour {

    GameObject player;    // Reference to the player GameObject.
    public Texture texture;
    public PowerupIcon powerup;

    // Use this for initialization
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter(Collider other)
    {
        // If the entering collider is the player...
        if (other.gameObject == player)
        {
            gameObject.renderer.enabled = false;
            gameObject.collider.enabled = false;
            powerup.pickedUp(texture);
            // powerup
        }
    }
}
