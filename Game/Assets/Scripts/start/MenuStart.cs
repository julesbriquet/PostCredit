using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MenuStart : MonoBehaviour {

	public Image Background;
	public Sprite Normal;
	public Sprite Credit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
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
