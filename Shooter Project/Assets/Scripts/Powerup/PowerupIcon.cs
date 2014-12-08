using UnityEngine;
using System.Collections;

public class PowerupIcon : MonoBehaviour {
    float timer;
    public float powerupTime;
    bool poweredUp;

	// Use this for initialization
	void Start () 
    {
        poweredUp = false;
	}

	// Update is called once per frame
	void Update () 
    {
        timer += Time.deltaTime;
        if (timer >= powerupTime && poweredUp)
        {
            gameObject.guiTexture.texture = null;
            poweredUp = false;
        }
	}

    public void pickedUp(Texture texture)
    {
        timer = 0f;
        gameObject.guiTexture.texture = texture;
        poweredUp = true;
    }
}
