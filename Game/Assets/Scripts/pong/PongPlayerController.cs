using UnityEngine;
using System.Collections;


public class PongPlayerController : MonoBehaviour
{

    public int playerNumber = 1;
    private string InputPlayerString = "";
    private bool enableControl;

    private float inputY;

    public float speed;
    private Vector2 velocity;

    public float yMax;

    public bool slideMovements;


    // Use this for initialization
    void Start()
    {
        //Time.timeScale = 1.3f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        /*
         *  HANDLING MULTIPLAYER
         */
        if (playerNumber == 1)
        {
            InputPlayerString = "_P1";
        }
        if (playerNumber == 2)
        {
            InputPlayerString = "_P2";
        }


        /*
         *  MOVING PART
         */
        float inputY = 0;

        if (slideMovements)
            inputY = Input.GetAxis("Vertical" + InputPlayerString);
        else
            inputY = Input.GetAxisRaw("Vertical" + InputPlayerString);

        velocity = new Vector2(0, speed * inputY) * Time.deltaTime;

        rigidbody2D.velocity = velocity;
        ScreenLimitControl();

    }


    // AVOID GOING OFF SCREEN
    void ScreenLimitControl()
    {
        float yMin = -yMax;
        rigidbody2D.position = new Vector2(rigidbody2D.position.x,
            Mathf.Clamp(rigidbody2D.position.y, yMin, yMax));
    }
}
