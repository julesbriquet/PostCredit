using UnityEngine;
using System.Collections;

public class MenuStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PressStart()
	{
        GameManager.Instance.gameMusic.Play();
		GameManager.Instance.StartGame();
	}

	public void PressQuit ()
	{
		Application.Quit();
	}
}
