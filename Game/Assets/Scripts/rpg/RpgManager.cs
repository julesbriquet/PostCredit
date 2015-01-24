using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RpgManager : MonoBehaviour {

	RpgAction[] buttonRpgActions;

	public Button[] SelectionButtons;

	public EventSystem EventSystemObj;

	public Button[] SelectionButtons2;

	public Button[] SelectionButtons3;

	public float[] TimerForDifficulty;
	float timeUntilEndGame;


	public GameObject Pannel2;
	public GameObject Pannel3;
	// Use this for initialization
	void Start () 
	{
		this.Pannel2.SetActive(false);
		this.Pannel3.SetActive(false);

		List<int> randoms = new List<int>(new int[]{0,1,2,3});
		List<int> selections = new List<int>();
		this.buttonRpgActions = new RpgAction[4];

		for(int i = 0; i < 4; i++)
		{
			int index = Random.Range(0, randoms.Count);
			selections.Add(randoms[index]);
			randoms.RemoveAt(index);
		}

		for(int i = 0; i < 4; i++)
		{
			switch(selections[i])
			{
			case 0:
				this.buttonRpgActions[i] = RpgAction.escape;
				break;
			case 1:
				this.buttonRpgActions[i] = RpgAction.wait;
				break;
			case 2:
				this.buttonRpgActions[i] = RpgAction.super;
				break;
			case 3:
				this.buttonRpgActions[i] = RpgAction.loose;
				break;
			}
		}

		for(int i = 0 ; i < 4 ; i++)
		{
			switch(this.buttonRpgActions[i])
			{
			case RpgAction.escape:
				this.SelectionButtons[i].transform.GetChild(0).GetComponent<Text>().text = "Escape";
				break;
			case RpgAction.wait:
				this.SelectionButtons[i].transform.GetChild(0).GetComponent<Text>().text = "Wait";
				break;
			case RpgAction.loose:
				this.SelectionButtons[i].transform.GetChild(0).GetComponent<Text>().text = "Loose";
				break;
			case RpgAction.super:
				this.SelectionButtons[i].transform.GetChild(0).GetComponent<Text>().text = "Super Attack!";
				break;
			}
		}

		int difficulty = GameManager.Instance.LevelDifficulty;
		difficulty--;
		difficulty = Mathf.Clamp(difficulty,0,3);

		this.timeUntilEndGame = Time.time + this.TimerForDifficulty[difficulty];

		TimerManager.Instance.StartTimer(this.timeUntilEndGame);
	}

	// Update is called once per frame
	void Update () 
	{
		if(Time.time > this.timeUntilEndGame)
		{
			GameManager.Instance.LevelEnd(false);
		}
	}

	public void PressSelection(int choose)
	{
		switch(GameManager.Instance.LevelDifficulty)
		{
		case 1 :
			CheckDifficulty1Step1(choose);
			break;

		case 2:
		case 3:
			CheckDifficulty2Step1(choose);
			break;
		}
	}

	public void PressSelection2(int choose)
	{
		switch(GameManager.Instance.LevelDifficulty)
		{
		case 2:
			CheckDifficulty2Step2(choose);
			break;

		case 3:
			CheckDifficulty3Step1(choose);
			break;
		}
	}

	public void PressSelection3(int choose)
	{
		CheckDifficulty3Step2(choose);
	}

	void CheckDifficulty1Step1(int choose)
	{
		if(this.buttonRpgActions[choose] == RpgAction.super)
		{
			GameManager.Instance.LevelEnd(true);
		}
		else
		{
			GameManager.Instance.LevelEnd(false);
		}
	}

	void CheckDifficulty2Step1(int choose)
	{
		if(this.buttonRpgActions[choose] == RpgAction.super)
		{
			foreach(Button b in this.SelectionButtons)
			{
				b.enabled = false;
				b.gameObject.SetActive(false);
			}
			this.buttonRpgActions = new RpgAction[2];
			int rand = Random.Range(0,2);
			if(rand==0)
			{
				this.buttonRpgActions[0] = RpgAction.win;
				this.buttonRpgActions[1] = RpgAction.loose;
				this.SelectionButtons2[0].transform.GetChild(0).GetComponent<Text>().text = "Win";
				this.SelectionButtons2[1].transform.GetChild(0).GetComponent<Text>().text = "Loose";
			}
			else
			{
				this.buttonRpgActions[0] = RpgAction.loose;
				this.buttonRpgActions[1] = RpgAction.win;
				this.SelectionButtons2[0].transform.GetChild(0).GetComponent<Text>().text = "Loose";
				this.SelectionButtons2[1].transform.GetChild(0).GetComponent<Text>().text = "Win";
			}

			this.Pannel2.SetActive(true);

			this.EventSystemObj.SetSelectedGameObject(this.SelectionButtons2[0].gameObject);
		}
		else
		{
			GameManager.Instance.LevelEnd(false);
		}
	}

	void CheckDifficulty2Step2( int choose )
	{
		if(this.buttonRpgActions[choose] == RpgAction.win)
		{
			GameManager.Instance.LevelEnd(true);
		}
		else
		{
			GameManager.Instance.LevelEnd(false);
		}
	}

	void CheckDifficulty3Step1 (int choose )
	{
		if(this.buttonRpgActions[choose] == RpgAction.win)
		{
			this.Pannel2.gameObject.SetActive(false);

			this.buttonRpgActions = new RpgAction[2];
			int rand = Random.Range(0,2);
			if(rand==0)
			{
				this.buttonRpgActions[0] = RpgAction.yes;
				this.buttonRpgActions[1] = RpgAction.no;
				this.SelectionButtons3[0].transform.GetChild(0).GetComponent<Text>().text = "Yes";
				this.SelectionButtons3[1].transform.GetChild(0).GetComponent<Text>().text = "No";
			}
			else
			{
				this.buttonRpgActions[0] = RpgAction.no;
				this.buttonRpgActions[1] = RpgAction.yes;
				this.SelectionButtons3[0].transform.GetChild(0).GetComponent<Text>().text = "No";
				this.SelectionButtons3[1].transform.GetChild(0).GetComponent<Text>().text = "Yes";
			}
			
			this.Pannel3.SetActive(true);
			
			this.EventSystemObj.SetSelectedGameObject(this.SelectionButtons3[0].gameObject);
		}
		else
		{
			GameManager.Instance.LevelEnd(false);
		}
	}

	void CheckDifficulty3Step2 (int choose )
	{
		if(this.buttonRpgActions[choose] == RpgAction.yes)
		{
			GameManager.Instance.LevelEnd(true);
		}
		else
		{
			GameManager.Instance.LevelEnd(false);
		}
	}
}
