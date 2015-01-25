using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour 
{
	public LobbyHeart Hearts;
	public LobbyCharacter Character;

	public Text MainText;

	public Tv LastScreen;
	public Tv NextScreen;

	public Image LobbyResult;

    public AudioSource winAudio;
    public AudioSource failAudio;
    public AudioSource gameOverAudio;


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

        if (win)
            winAudio.Play();
        else
            failAudio.Play();

		//Set previous screenshot
		{
			string screenshot = GameManager.Instance.LastLevelLobby;
			if(!screenshot.Contains("coin") && !screenshot.Contains("plateformer") && !screenshot.Contains("jump")&& !screenshot.Contains("zelda"))
			{
				int diff = GameManager.Instance.LevelDifficulty;
				screenshot+=("_"+diff);
			}
			Debug.Log("prev :" + screenshot + ".png");
			Debug.Log("prev : " + Resources.Load<Sprite>(screenshot));
			this.LastScreen.Screenshot.sprite = (Sprite) Resources.Load<Sprite>(screenshot);
		}

		if(stillLevel)
		{
			nextLevel = GameManager.Instance.FindNextLevel();
			//Set next screenshot
			{
				string screenshot = nextLevel;
				if(!screenshot.Contains("coin") && !screenshot.Contains("plateformer") && !screenshot.Contains("jump")&& !screenshot.Contains("zelda"))
				{
					int diff = GameManager.Instance.LevelDifficulty;
					if(nextDifficulty)
					{
						diff++;
						diff = Mathf.Clamp(diff,1,3);
					}
					screenshot+=("_"+diff);
				}
				Debug.Log("next :" + screenshot + ".png");
				Debug.Log("next :" + Resources.Load<Sprite>(screenshot));
				this.NextScreen.Screenshot.sprite = (Sprite) Resources.Load<Sprite>(screenshot);

				if(screenshot.Contains("coin"))
				{
					this.NextScreen.Todo.sprite = (Sprite) Resources.Load<Sprite>("txt_collect");
				}
				else if(screenshot.Contains("plateformer"))
				{
					this.NextScreen.Todo.sprite = (Sprite) Resources.Load<Sprite>("txt_jump");
				}
				else if(screenshot.Contains("jump"))
				{
					this.NextScreen.Todo.sprite = (Sprite) Resources.Load<Sprite>("txt_jump");
				}
				else if(screenshot.Contains("finish_him"))
				{
					this.NextScreen.Todo.sprite = (Sprite) Resources.Load<Sprite>("txt_finish_him");
				}
				else if(screenshot.Contains("rpg"))
				{
					this.NextScreen.Todo.sprite = (Sprite) Resources.Load<Sprite>("txt_attack");
				}
				else if(screenshot.Contains("pong"))
				{
					this.NextScreen.Todo.sprite = (Sprite) Resources.Load<Sprite>("txt_survive");
				}
				else if(screenshot.Contains("shooter"))
				{
					this.NextScreen.Todo.sprite = (Sprite) Resources.Load<Sprite>("txt_attack");
				}
				else if(screenshot.Contains("zelda"))
				{
					this.NextScreen.Todo.sprite = (Sprite) Resources.Load<Sprite>("txt_attack");
				}
				else
				{
					Debug.Log("unknow mission");
				}
			}
		}
		else
		{
			this.NextScreen.Screenshot.sprite = (Sprite) Resources.Load<Sprite>("black");
			GameObject.Destroy(this.NextScreen.Todo);
		}

		this.LobbyResult.enabled = true;
		if(win)
		{
//			this.MainText.text = "You win";
			this.LobbyResult.sprite = (Sprite) Resources.Load<Sprite>("txt_you_win");
		}
		else
		{
//			this.MainText.text = "You lose";
			this.LobbyResult.sprite = (Sprite) Resources.Load<Sprite>("txt_you_lose");
		}

		yield return new WaitForSeconds(2f);

		if(!win)
		{
			this.MainText.text = "";
			activePlayer.Life--;
			this.Hearts.LoseLife(activePlayer.Life);
			yield return new WaitForSeconds(1f);
		}

		if(activePlayer.Life == 0)
		{
//			this.MainText.text = "Game Over";
			this.LobbyResult.sprite = (Sprite) Resources.Load<Sprite>("txt_game_over");
            gameOverAudio.Play();
			yield return new WaitForSeconds(2.7f);

            GameManager.Instance.gameMusic.Stop();
			GameManager.Instance.Restart();
		}
		else
		{
			if(stillLevel)
			{
				if(nextDifficulty)
				{
//					this.MainText.text = "Next difficulty";
					this.LobbyResult.sprite = (Sprite) Resources.Load<Sprite>("txt_be_ready");
					yield return new WaitForSeconds(1f);
				}

//				this.MainText.text = "Be ready for the next level";
				this.LobbyResult.sprite = (Sprite) Resources.Load<Sprite>("txt_be_ready");

				this.Character.StartMove();
				yield return new WaitForSeconds(2.6f);

				GameManager.Instance.LoadLevel();
			}
			else
			{
//				this.MainText.text = "Congratulation!";
				this.LobbyResult.sprite = (Sprite) Resources.Load<Sprite>("txt_congrats");
				
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
		                                   GameManager.Instance.LastLevelWin?"Win":"Lose");
		
		yield return new WaitForSeconds(2f);

		if(!win)
		{
			activePlayer.Life--;
			this.MainText.text = string.Format("Player {0} lose 1 life", activePlayer.Id);
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
