using UnityEngine;
using System.Collections;

public class LobbyHeart : MonoBehaviour {
	public Vector2 Align;
	public bool LockX;
	public bool LockY;

	public Heart[] Hearts;

	int screenH = 0;
	int screenW = 0;



	// Use this for initialization
	void Start () 
	{
		Place();
	}

	public void SetUp (int life)
	{
		int i = 0;
		for(; i < life; i++)
		{
			this.Hearts[i].renderer.enabled = true;
		}

		for(;i<3;i++)
		{
			this.Hearts[i].renderer.enabled = false;
		}
	}

	public void LoseLife (int life)
	{
		this.Hearts[life].GetComponent<Animator>().SetTrigger("Destroy");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Screen.height != this.screenH || Screen.width != this.screenW)
		{
			Place();
		}
	}

	void Place ()
	{
		Vector3 viewport = Camera.main.ViewportToWorldPoint(new Vector3(Align.x,Align.y,0));
		
		Vector3 pos = this.transform.position;
		
		if(!LockX)
		{
			pos.x = viewport.x;
		}
		
		if(!LockY)
		{
			pos.y = viewport.y;
		}
		
		this.transform.position = pos;
		
		this.screenH = Screen.height;
		this.screenW = Screen.width;
	}
}
