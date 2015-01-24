using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour 
{
	public Text MainText;
	public void Start ()
	{
		StartCoroutine("PlayEnd");
	}

	IEnumerator PlayEnd ()
	{
		Player activePlayer = GameManager.Instance.ActivePlayer;

		this.MainText.text = string.Format("Player {0} {1}", GameManager.Instance.ActivePlayer.Id,
		                              GameManager.Instance.LastLevelWin?"Win":"Loose");

		yield return new WaitForSeconds(2f);

		activePlayer.Life--;
		if(activePlayer.Life == 0)
		{
			//do something.
		}

		this.MainText.text = string.Format("Be ready for the next level");

		yield return new WaitForSeconds(1f);

		GameManager.Instance.LoadLevel();
	}
}
