using UnityEngine;
using System.Collections;

public class PlateformerCarnivor : MonoBehaviour {
	
	public float Height;
	public float Speed;
	
	float top;
	float bottom;
	
	int direction = 1;
	
	// Use this for initialization
	void Start () 
	{
		this.top = this.transform.position.y + this.Height;
		this.bottom = this.transform.position.y;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		this.rigidbody2D.velocity = new Vector2(0f, Speed * this.direction);
		
		if(direction > 0)
		{
			if(this.transform.position.y >= this.top)
			{
				direction *= -1;
			}
		}
		else
		{
			if(this.transform.position.y <= this.bottom)
			{
				direction *= -1;
			}
		}
	}
	
	void OnTriggerEnter2D ( Collider2D other )
	{
		if(other.tag == "Player")
		{
			other.gameObject.SendMessage("DeadSkull");
			this.collider2D.enabled = false;
		}
	}
	
	
}
