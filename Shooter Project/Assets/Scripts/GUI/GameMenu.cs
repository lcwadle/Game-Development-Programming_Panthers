using UnityEngine;
using System.Collections;

public class GameMenu : MonoBehaviour
{
	public bool isQuit = false;
	
	void OnMouseEnter()
	{
		transform.guiText.color = Color.yellow;
	}

	void OnMouseExit()
	{
		transform.guiText.color = Color.blue;
	}
	
	void OnMouseUp()
	{
		if(isQuit == true)
			Application.LoadLevel("Menu");
		else
			Application.LoadLevel("Game");
	}
}
