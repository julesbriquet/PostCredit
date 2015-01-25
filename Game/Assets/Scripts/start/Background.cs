using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		ResizeBackGround();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ResizeBackGround()
	{
		Transform background = this.transform;
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
