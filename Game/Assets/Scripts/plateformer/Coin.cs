using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	public static int CoinCount;
	public static int Collected;
	void Awake ()
	{
		Coin.CoinCount++;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D ( Collider2D other )
	{
		if(other.name == "PlateformerCharacter")
		{
			Coin.Collected++;

			if(Coin.CoinCount == Coin.Collected)
			{
				GameManager.Instance.LevelEnd(true);
			}

			GameObject.Destroy(this.gameObject);
		}
	}
}
