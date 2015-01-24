using UnityEngine;
using System.Collections;

public class PongManager : MonoBehaviour {

    public float[] timerForDifficulty;
    private float timeUntilEndGame;

    public PongBall ballToSpawn;

	// Use this for initialization
	void Start () {

        for (int i = 0; i < GameManager.Instance.LevelDifficulty; i++) {
            PongBall ball = Instantiate(ballToSpawn, Vector3.zero, Quaternion.identity) as PongBall;
            ball.secondsToWaitBeforeStart = ((float)((i + 1) * 1.1f));
            ball.speed = new Vector2(ball.speed.x, (ball.speed.y) * Mathf.Pow(-1, i));
        }
        

        timeUntilEndGame = timerForDifficulty[GameManager.Instance.LevelDifficulty - 1];
	}
	
	// Update is called once per frame
	void Update () {
        timeUntilEndGame -= Time.deltaTime;

        if (timeUntilEndGame <= 0)
            GameManager.Instance.LevelEnd(true);
	}
}
