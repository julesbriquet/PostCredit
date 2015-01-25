using UnityEngine;
using System.Collections;

public class PlateformerCharacter : MonoBehaviour {

	public bool LockMove;

	bool hasToJump = false;
	Animator animator;
	bool dead = false;
	bool win = false;
	public bool HasWin { get { return this.win; } }
	bool collisionStay = false;

	// Use this for initialization
	void Start () {
		this.animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetButtonDown("Action_P1") && Mathf.Approximately(0f, this.rigidbody2D.velocity.y))
		{
			this.hasToJump = true;
		}
	}

	void FixedUpdate ()
	{
		if(!this.dead && !this.win)
		{
			if(!this.LockMove && !this.collisionStay)
			{
				Vector2 velocity = this.rigidbody2D.velocity;
				velocity.x = Input.GetAxis("Horizontal_P1") * PlateformerManager.Instance.GetMoveSpeed();
				this.rigidbody2D.velocity = velocity;
			}

			if(this.hasToJump)
			{
				this.hasToJump = false;
				this.rigidbody2D.AddForce(new Vector2(0f, PlateformerManager.Instance.GetJumpForce()), ForceMode2D.Impulse);
			}
		}

		if(this.win)
		{
			Vector2 velocity = this.rigidbody2D.velocity;
			velocity.x = PlateformerManager.Instance.GetMoveSpeed();
			this.rigidbody2D.velocity = velocity;
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

		if(this.rigidbody2D.velocity.x >= 0f)
		{
			this.transform.localScale = new Vector3(1f,1f,1f);
		}
		else
		{
			this.transform.localScale = new Vector3(-1f,1f,1f);
		}
	}

	void OnCollisionStay2D(Collision2D coll) 
	{
		if(coll.collider.tag == "Plateform")
		{
			if(this.rigidbody2D.collider2D.bounds.min.y < coll.collider.collider2D.bounds.max.y
			   ||
			   this.rigidbody2D.collider2D.bounds.max.x < coll.collider.collider2D.bounds.min.x)
			{
				this.collisionStay = true;
				Vector2 velocity = this.rigidbody2D.velocity;
				velocity.x = 0f;
				this.rigidbody2D.velocity = velocity;

				//this.rigidbody2D.AddForce(new Vector2(0f, PlateformerManager.Instance.GetGravity()));
			}
		}
	}

	void OnCollisionExit2D(Collision2D coll)
	{
		if(coll.collider.tag == "Plateform")
			this.collisionStay = false;
	}

	void DeadSkull ()
	{
		if(this.win)
			return;

		this.GetComponent<SpriteRenderer>().color = Color.black;
		this.dead = true;

		this.rigidbody2D.velocity = Vector2.zero;
		this.rigidbody2D.AddForce(new Vector2(0f, PlateformerManager.Instance.GetJumpForce()), ForceMode2D.Impulse);
	}

	void DeadAttacker ()
	{
		if(this.win)
			return;
		
		this.GetComponent<SpriteRenderer>().color = Color.black;
		this.dead = true;
		
		this.rigidbody2D.velocity = Vector2.zero;
		this.rigidbody2D.AddForce(new Vector2(0f, PlateformerManager.Instance.GetJumpForce()), ForceMode2D.Impulse);
		this.rigidbody2D.collider2D.isTrigger = true;
	}

	void Dead ()
	{
		if(this.win)
			return;

		this.dead = true;
		StartCoroutine("DeadEnding");
	}

	IEnumerator DeadEnding ()
	{
		yield return new WaitForSeconds(0.5f);
		GameManager.Instance.LevelEnd(false);
	}

	void Win ()
	{
		if(!this.dead)
		{
			this.win = true;
			StartCoroutine("WinEnding");
		}
	}

	IEnumerator WinEnding()
	{
		yield return new WaitForSeconds(0.8f);
		GameManager.Instance.LevelEnd(true);
	}
}
