using UnityEngine;
using System.Collections;

public class ShooterManager : MonoBehaviour {

    public EnnemyEntity[] ennemyList;
    public Transform SpawnPoint;

    public float[] timerForDifficulty;
    public float timeUntilEndGame;

	// Use this for initialization
	void Start () {
        
        Instantiate(ennemyList[GameManager.Instance.LevelDifficulty - 1], SpawnPoint.position, Quaternion.identity);
        timeUntilEndGame = timerForDifficulty[GameManager.Instance.LevelDifficulty - 1];
	}
	
	// Update is called once per frame
	void Update () {
        timeUntilEndGame -= Time.deltaTime;

        if (timeUntilEndGame <= 0)
            GameManager.Instance.LevelEnd(false);
	}
}
