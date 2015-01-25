using UnityEngine;
using System.Collections;

public class TimerManager : MonoBehaviour {

	public Transform Left;
	public Transform Right;
	public Transform Ghost;

	public static TimerManager Instance;

	float startPosition;
	float finalPosition;

	float startTime;
	float delayTimer;

	bool started = false;

	bool isFall = false;

	void Awake ()
	{
		TimerManager.Instance = this;
	}

	// Use this for initialization
	void Start () 
	{
		this.startPosition = this.Left.localPosition.x;
		this.finalPosition = this.Right.localPosition.x;
	}

	public void StartTimer (float delay)
	{
		this.delayTimer = delay - 0.4f;

		if(this.delayTimer <= 0f)
			this.delayTimer = 0f;

		this.startTime = Time.time;
		this.started = true;
	}
	bool shoot = false;
	// Update is called once per frame
	void Update () 
	{
		if(!this.started)
			return;

		if(!this.isFall)
		{
			// Time since we start progress to next point
			float tP = Time.time - this.startTime;
			
			// Lerp t
			float t = 1f;
			if(this.delayTimer > 0f)
			{	
				t =	Mathf.Clamp(tP / this.delayTimer,0f,1f);
			}

			if(!shoot && t > 0.2f)
			{
				Debug.Log("SHOOT");
				shoot = true;
				string name = Application.loadedLevelName + "_" + GameManager.Instance.LevelDifficulty;
				Application.CaptureScreenshot(name);
			}

			float x = Mathf.Lerp(this.startPosition, this.finalPosition, t);

			Vector3 position = this.Ghost.localPosition;
			position.x  = x;
			this.Ghost.localPosition = position;

			if(Mathf.Approximately(t,1f))
			{
				this.Ghost.rigidbody2D.isKinematic = false;
				this.isFall = true;
			}
		}

		if(this.isFall)
		{
			this.Ghost.transform.Rotate(new Vector3(0f,0f,-2f));
		}
	}
}
