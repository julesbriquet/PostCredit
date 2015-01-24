using UnityEngine;
using System.Collections;

public class PlateformerManager : MonoBehaviour {

	public float MoveSpeed;
	public float JumpForce;
	public float Gravity;

	public static PlateformerManager Instance;

	void Awake ()
	{
		PlateformerManager.Instance = this;
	}

	// Use this for initialization
	void Start () 
	{
		Physics2D.gravity = new Vector2(0f, this.Gravity);
	}
	
	// Update is called once per frame
	void Update () {
	
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
