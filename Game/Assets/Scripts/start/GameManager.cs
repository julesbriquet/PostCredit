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

	Player[] Players;
	string[] levelNames  = {"shooter","rpg","jump"};
	List<string> levelSelections;

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
			GameObject.Destroy(this);
		}
	}

	public void Restart ()
	{
		Application.LoadLevel("start");
	}

	public void StartGame ()
	{
		LoadLevel();
	}

	void SetUp()
	{
		this.levelSelections = new List<string>(this.levelNames);
		this.LevelDifficulty = 1;

		this.Players = new Player[this.NumberOfPlayer];
		for(int i = 0; i < this.Players.Length; i++)
		{
			this.Players[i] = new Player();
			this.Players[i].Id = i+1;
			this.Players[i].Life = this.BasePlayerLife;
		}
		
		this.ActivePlayer = this.Players[0];
	}

	void LoadLevel ()
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

	public void LevelEnd(bool win)
	{
		Debug.Log ("Player " + this.ActivePlayer.Id + " win = " + win);
		LoadLevel();
	}
}
