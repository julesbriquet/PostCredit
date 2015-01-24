using UnityEngine;
using System.Collections;

public class LobbyCharacter : MonoBehaviour 
{
	bool hasToJump = false;
	bool hasToMove = false;
	bool noMoreMove = false;
	bool hasToGrowth = false;
	bool hasToReduce = false;
	Animator animator;
	
	// Use this for initialization
	void Start () {
		this.animator = this.GetComponent<Animator>();
		this.rigidbody2D.isKinematic = true;
		this.transform.localScale = new Vector3(0f,0f,0f);
	}

	public void StartMove ()
	{
		this.rigidbody2D.isKinematic = false;
		Physics2D.gravity = new Vector2(0f, -20f);
		this.hasToJump = true;
		this.hasToGrowth = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//
	}
	
	void FixedUpdate ()
	{
		if(this.transform.localScale.x < 1f && hasToGrowth)
		{
			Vector3 scale = this.transform.localScale;
			scale.x += 0.05f;
			scale.y += 0.05f;
			this.transform.localScale = scale;
			if(Mathf.Approximately(scale.x, 1f))
				hasToGrowth = false;
		}

		if(this.transform.localScale.x >= 0f && hasToReduce)
		{
			Vector3 scale = this.transform.localScale;
			scale.x -= 0.05f;
			scale.y -= 0.05f;
			this.transform.localScale = scale;
			if(Mathf.Approximately(scale.x, 0f))
				hasToReduce = false;
		}

		if(this.hasToMove && !this.noMoreMove)
		{
			Vector2 velocity = this.rigidbody2D.velocity;
			velocity.x = 2.1f;
			this.rigidbody2D.velocity = velocity;
		}
		
		if(this.hasToJump)
		{
			this.hasToJump = false;
			this.rigidbody2D.AddForce(new Vector2(0f, 5f), ForceMode2D.Impulse);
		}
		
		if(this.rigidbody2D.velocity.y > 0f)
		{
			this.animator.SetBool("Move", false);
			this.animator.SetBool("Jump", true);
		}
		else if(!Mathf.Approximately(0f, this.rigidbody2D.velocity.x) && this.rigidbody2D.velocity.y <= 0f)
		{
			this.animator.SetBool("Move", true);
			this.animator.SetBool("Jump", false);
		}
		else
		{
			this.animator.SetBool("Move", false);
			this.animator.SetBool("Jump", false);
		}
		

	}

	void OnCollisionEnter2D ( Collision2D coll )
	{
		Debug.Log ("colison");
		this.hasToMove = true;
	}

	public void GoNextLevel ()
	{
		this.hasToReduce = true;
		this.rigidbody2D.velocity = Vector2.zero;
		this.noMoreMove = true;
		this.hasToJump = true;
		this.hasToMove = false;
	}
}
