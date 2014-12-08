using UnityEngine;
using System.Collections;

public class PowerupPickup : MonoBehaviour {

    GameObject player;    // Reference to the player GameObject.
    public Texture texture;
    public PowerupIcon powerup;
    public PlayerShooting playerShooting;
    public int powerupOption;

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
            if (powerupOption == 1)
            {
                playerShooting.powerupOne();
            }
            if (powerupOption == 2)
            {
                playerShooting.powerupTwo();
            }
        }
    }
}
