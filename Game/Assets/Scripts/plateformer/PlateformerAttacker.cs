using UnityEngine;
using System.Collections;

public class PlateformerAttacker : MonoBehaviour {

	public float Speed;
	

	
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		Vector2 velocity = this.rigidbody2D.velocity;

		velocity.x = Speed;

		this.rigidbody2D.velocity = velocity;
	}
	
	void OnCollisionEnter2D ( Collision2D coll )
	{
		if(coll.collider.tag == "Player")
		{
			coll.collider.gameObject.SendMessage("DeadAttacker");
			Physics2D.IgnoreCollision(this.collider2D, coll.collider.collider2D,true);
		}
	}
}
