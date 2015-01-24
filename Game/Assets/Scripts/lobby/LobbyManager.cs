using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour 
{
	public LobbyHeart Hearts;
	public LobbyCharacter Character;

	public Text MainText;
	public void Start ()
	{
		this.Hearts.SetUp(GameManager.Instance.ActivePlayer.Life);

		if(GameManager.Instance.NumberOfPlayer > 1)
		{
			StartCoroutine("PlayEndMulti");
		}
		else
		{
			StartCoroutine("PlayEndSolo");
		}
	}

	IEnumerator PlayEndSolo ()
	{
		Player activePlayer = GameManager.Instance.ActivePlayer;
		bool win = GameManager.Instance.LastLevelWin;
		bool stillLevel = GameManager.Instance.StillLevel();
		bool nextDifficulty = GameManager.Instance.NextDifficulty();

		string nextLevel = string.Empty;

		if(stillLevel)
		{
			nextLevel = GameManager.Instance.FindNextLevel();
		}

		if(win)
		{
			this.MainText.text = "You win";
		}
		else
		{
			this.MainText.text = "You loose";
		}

		yield return new WaitForSeconds(2f);

		if(!win)
		{
			this.MainText.text = "You lose one life";
			activePlayer.Life--;
			this.Hearts.LoseLife(activePlayer.Life);
			yield return new WaitForSeconds(1f);
		}

		if(activePlayer.Life == 0)
		{
			this.MainText.text = "Game Over";

			yield return new WaitForSeconds(1f);

			GameManager.Instance.Restart();
		}
		else
		{
			if(stillLevel)
			{
				if(nextDifficulty)
				{
					this.MainText.text = "Next difficulty";
					yield return new WaitForSeconds(1f);
				}

				this.MainText.text = "Be ready for the next level";
				this.Character.StartMove();
				yield return new WaitForSeconds(2.2f);

				GameManager.Instance.LoadLevel();
			}
			else
			{
				this.MainText.text = "Congratulation!";
				
				yield return new WaitForSeconds(5f);
				
				GameManager.Instance.Restart();
			}
		}
	}

	IEnumerator PlayEndMulti ()
	{
		Player activePlayer = GameManager.Instance.ActivePlayer;
		bool nextDifficulty = GameManager.Instance.NextDifficulty();
		bool win = GameManager.Instance.LastLevelWin;
		int numberOfPlayerAlive = GameManager.Instance.NumberOfPlayerAlive();

		this.MainText.text = string.Format("Player {0} {1}", GameManager.Instance.ActivePlayer.Id,
		                                   GameManager.Instance.LastLevelWin?"Win":"Loose");
		
		yield return new WaitForSeconds(2f);

		if(!win)
		{
			activePlayer.Life--;
			this.MainText.text = string.Format("Player {0} loose 1 life", activePlayer.Id);
			yield return new WaitForSeconds(1f);
		}
	
		if(activePlayer.Life == 0)
		{
			this.MainText.text = string.Format("Player {0} is dead",activePlayer.Id);
			yield return new WaitForSeconds(2f);
		}

		if(numberOfPlayerAlive > 1 )
		{
			if(nextDifficulty)
			{
				this.MainText.text = "Next difficulty";
				yield return new WaitForSeconds(1f);
			}
			
			this.MainText.text = string.Format("Be ready for the next level");			
			yield return new WaitForSeconds(1f);
			GameManager.Instance.FindNextLevel();
			GameManager.Instance.LoadLevel();
		}
		else
		{
			this.MainText.text = string.Format("Congratulation player {0}",
			                                   GameManager.Instance.GetWinner().Id);		
			yield return new WaitForSeconds(5f);		
			GameManager.Instance.Restart();
		}
	}
}
