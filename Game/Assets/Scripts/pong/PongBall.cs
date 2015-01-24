using UnityEngine;
using System.Collections;

public class PongBall : MonoBehaviour {

    public Vector2 speed;
    private Vector3 velocity;

    public float secondsToWaitBeforeStart = 2f;
    public float startingTime = 0f;

	// Use this for initialization
	void Start () {
        //Debug.Log(Screen.height);
        startingTime = Time.time + secondsToWaitBeforeStart;
	}
	
	// Update is called once per frame
	void Update () {

        if (Time.time > startingTime)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(this.transform.position);

            if ((screenPosition.y > Screen.height && speed.y > 0) || (screenPosition.y < 0 && speed.y < 0))
            {
                speed = new Vector2(speed.x, -speed.y);
            }


            //Debug.Log(Camera.main.WorldToScreenPoint(this.transform.position));

            velocity = new Vector3(speed.x, speed.y, 0) * Time.deltaTime;

            this.transform.position += velocity;


            if (screenPosition.x < -50)
                GameManager.Instance.LevelEnd(false);
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        this.speed = new Vector2(-speed.x, speed.y);
    }

    public void SetTimeUntilStart(float secondsUntilStart)
    {
        Debug.Log("Kikoo, seconds: " + secondsUntilStart);
        this.startingTime = Time.time + secondsUntilStart;
    }
}
