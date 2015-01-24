using UnityEngine;
using System.Collections;

public class PongManager : MonoBehaviour {

    public float[] timerForDifficulty;
    private float timeUntilEndGame;

    public PongBall ballToSpawn;

    public Transform background;

	// Use this for initialization
	void Start () {

        ResizeBackGround();

        for (int i = 0; i < GameManager.Instance.LevelDifficulty; i++) {
            PongBall ball = Instantiate(ballToSpawn, Vector3.zero, Quaternion.identity) as PongBall;
            ball.secondsToWaitBeforeStart = ((float)((i + 1) * 1.1f));
            ball.speed = new Vector2(ball.speed.x, (ball.speed.y) * Mathf.Pow(-1, i));
        }
        

        timeUntilEndGame = timerForDifficulty[GameManager.Instance.LevelDifficulty - 1];
		TimerManager.Instance.StartTimer( timerForDifficulty[GameManager.Instance.LevelDifficulty - 1] );
	}
	
	// Update is called once per frame
	void Update () {
        timeUntilEndGame -= Time.deltaTime;

        if (timeUntilEndGame <= 0)
            GameManager.Instance.LevelEnd(true);
	}


    void ResizeBackGround()
    {
        SpriteRenderer sr = background.GetComponent<SpriteRenderer>();
        if (sr == null) return;

        background.localScale = new Vector3(1, 1, 1);

        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;


        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        Vector3 xWidth = background.localScale;
        xWidth.x = worldScreenWidth / width;
        background.localScale = xWidth;
        //transform.localScale.x = worldScreenWidth / width;
        Vector3 yHeight = background.localScale;
        yHeight.y = worldScreenHeight / height;
        background.localScale = yHeight;
        //transform.localScale.y = worldScreenHeight / height;

    }
}
