using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;

	public float GameSpeed;
	public int LevelDifficulty;

	string[] levelNames  = {"shoot","rpg","jump"};
	List<string> levelSelections;

	void Awake ()
	{
		if(null == GameManager.Instance)
		{
			GameObject.DontDestroyOnLoad(this);
			GameManager.Instance = this;
		}
		else if(this != GameManager.Instance)
		{
			GameObject.Destroy(this);
		}
	}

	public void Restart ()
	{
		Application.LoadLevel("start");
	}

	public void StartGame ()
	{
		this.levelSelections = new List<string>(this.levelNames);
		this.LevelDifficulty = 1;

		LoadLevel();
	}

	public void LoadLevel ()
	{
		if(this.levelSelections.Count == 0)
		{
			this.LevelDifficulty++;
			this.levelSelections = new List<string>(this.levelNames);
		}

		int rand = Random.Range(0,this.levelSelections.Count);
		string levelName = this.levelSelections[rand];
		this.levelSelections.RemoveAt(rand);

		Application.LoadLevel(levelName);
	}
}
