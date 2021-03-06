﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;

	public float GameSpeed;
	public int LevelDifficulty;
	public Player ActivePlayer;
	public int NumberOfPlayer = 1;
	public int BasePlayerLife = 3;
	public bool LastLevelWin = false;
	public int LevelCount;



	Player[] Players;
	string[] levelNames  = {"shooter","rpg","plateformer","jumper","coin","finish_him","pong","zelda"};
	List<string> levelSelections;
	string nextLevel;
	public string LastLevelLobby { get { return this.nextLevel; }}

    public TransitionAnimations animationUI;

    public bool levelEnding;

    public AudioSource gameMusic;

	void Awake ()
	{
		if(null == GameManager.Instance)
		{
			GameObject.DontDestroyOnLoad(this);
			SetUp();
			GameManager.Instance = this;
		}
		else if(this != GameManager.Instance)
		{
			GameObject.Destroy(this.gameObject);
		}
	}

	public void Restart ()
	{
		Debug.Log("restart");
		this.LevelCount = 0;
		this.LevelDifficulty = 1;
		this.levelSelections = new List<string>(this.levelNames);
		SetUp();
		Application.LoadLevel("start");
	}

	public void StartGame ()
	{
		this.LevelCount =0;
		FindNextLevel();
		LoadLevel();
	}

	void SetUp()
	{
		this.LevelCount = 0;
		this.levelSelections = new List<string>(this.levelNames);

		if( 0 == this.LevelDifficulty )
		{
			this.LevelDifficulty = 1;
		}

		this.Players = new Player[this.NumberOfPlayer];
		for(int i = 0; i < this.Players.Length; i++)
		{
			this.Players[i] = new Player();
			this.Players[i].Id = i+1;
			this.Players[i].Life = this.BasePlayerLife;
		}
		
		this.ActivePlayer = this.Players[0];
	}

	public string FindNextLevel ()
	{
		if(this.levelSelections.Count == 0)
		{
			this.LevelDifficulty++;
			this.levelSelections = new List<string>(this.levelNames);
		}
		
		int rand = Random.Range(0,this.levelSelections.Count);
		string levelName = this.levelSelections[rand];
		this.levelSelections.RemoveAt(rand);
		
		if(levelName == "coin" || levelName == "plateformer" || levelName == "jumper" || levelName == "zelda")
		{
			int levelIndex = Mathf.Clamp(this.LevelDifficulty,1,3);
			levelName = levelName + "_" + levelIndex;
		}
		
		Debug.Log("Next level will be " + levelName + " // " + this.LevelDifficulty);
		this.nextLevel = levelName;
		return levelName;
	}

	public void LoadLevel ()
	{
        levelEnding = false;
		Debug.Log("LoadLevel " + this.nextLevel);
		Coin.CoinCount = 0;
		Coin.Collected = 0;
		this.LevelCount++;
		Application.LoadLevel(this.nextLevel);
	}

	public void LevelEnd(bool win)
	{
        if (!levelEnding)
        {
            levelEnding = true;
            this.LastLevelWin = win;
            if (animationUI)
                animationUI.LaunchEndGameAnimation();
        }
	}

    public void ClapEnd()
    {
        Application.LoadLevel("lobby");
    }

	public bool StillLevel ()
	{
		return !(this.LevelDifficulty == 3 && this.levelSelections.Count == 0);
	}

	public bool NextDifficulty ()
	{
		return (this.levelSelections.Count == 0);
	}

	public int NumberOfPlayerAlive ()
	{
		int count = 0;
		foreach(Player p in this.Players)
		{
			if(p.Life > 0)
			{
				count ++;
			}
		}

		return count;
	}

	public Player GetWinner ()
	{
		foreach(Player p in this.Players)
		{
			if(p.Life > 0)
			{
				return p;
			}
		}

		throw new System.Exception("All Player Dead Exception");
	}

    void OnLevelWasLoaded(int level)
    {
        GameObject animationTmp = GameObject.FindGameObjectWithTag("TransitionAnimator");
        if (animationTmp)
        {
            animationUI = animationTmp.GetComponent<TransitionAnimations>();
            animationUI.LaunchStartGameAnimation();
        }
    }
}
