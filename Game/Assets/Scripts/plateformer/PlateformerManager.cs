using UnityEngine;
using System.Collections;

public class PlateformerManager : MonoBehaviour {

	public float MoveSpeed;
	public float JumpForce;
	public float Gravity;
	public float TimerForDifficulty;
	float timeUntilEndGame;

	public static PlateformerManager Instance;

	void Awake ()
	{
		PlateformerManager.Instance = this;
	}

	// Use this for initialization
	void Start () 
	{
		this.timeUntilEndGame = Time.time + TimerForDifficulty;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Time.time > this.timeUntilEndGame)
		{
			if(GameObject.FindGameObjectWithTag("Player").GetComponent<PlateformerCharacter>().LockMove)
			{
				GameManager.Instance.LevelEnd(true);
			}
			else
			{
				GameManager.Instance.LevelEnd(false);
			}
		}
	}

	void FixedUpdate ()
	{
		Physics2D.gravity = new Vector2(0f, this.Gravity);
	}

	public float GetJumpForce ()
	{
		return this.JumpForce;
	}

	public float GetMoveSpeed ()
	{
		return this.MoveSpeed;
	}

	public float GetGravity ()
	{
		return this.Gravity;
	}
}
