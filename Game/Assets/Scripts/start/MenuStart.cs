using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MenuStart : MonoBehaviour {

	public Image Background;
	public Sprite Normal;
	public Sprite Credit;
	public Button CreditButton;

	// Use this for initialization
	void Start () {
	
	}
	public void SetCredit()
	{
		this.Background.sprite = this.Credit;
	}
	public void SetNormal ()
	{
		this.Background.sprite = this.Normal;
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