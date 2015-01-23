using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class RpgManager : MonoBehaviour {

	RpgAction[] buttonRpgActions;

	public Button[] SelectionButtons;

	// Use this for initialization
	void Start () 
	{
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

	
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void PressSelection(int choose)
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
}
