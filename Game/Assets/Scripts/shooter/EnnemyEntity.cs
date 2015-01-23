using UnityEngine;
using System.Collections;

public class EnnemyEntity : MonoBehaviour {

    public int hitPointNumber;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        // WIN GAME
        if (hitPointNumber == 0)
            Debug.Log("WIN");
        
	}
}
