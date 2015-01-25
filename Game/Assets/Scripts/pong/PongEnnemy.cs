using UnityEngine;
using System.Collections;

public class PongEnnemy : MonoBehaviour {

    public float speed;
    private int direction;

	// Use this for initialization
	void Start () {
        direction = 1;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (this.transform.position.y > 2 || this.transform.position.y < -2)
            direction *= -1;

        rigidbody2D.velocity = new Vector2(0, speed * direction * Time.deltaTime);

	}
}
