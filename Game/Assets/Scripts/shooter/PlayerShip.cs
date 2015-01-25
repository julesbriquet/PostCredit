using UnityEngine;
using System.Collections;


public class PlayerShip : MonoBehaviour
{

    public int playerNumber = 1;
    private string InputPlayerString = "";
    private bool enableControl;

    private float inputY;

    public float speed;
    private Vector2 velocity;

    public float yMax;

    public GunEntity gun;

    public bool slideMovements;

    public bool dead = false;

    public Animator animator;

    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
        //Time.timeScale = 1.3f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!dead)
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

            velocity = new Vector2(0, speed * inputY * Time.deltaTime);

            //Debug.Log("Velocity: " + velocity + ", TimeDelta: " + Time.deltaTime + " timeScale: " + Time.timeScale);

            rigidbody2D.velocity = velocity;
            ScreenLimitControl();

            /*
             * SHOOTING PART
             */
            bool triggerShoot = false;
            triggerShoot = Input.GetButton("Action" + InputPlayerString);

            if (triggerShoot)
                gun.Shoot(playerNumber);
        }

        /*
         * DEAD
         */
        if (dead)
        {
            rigidbody2D.velocity = Vector2.zero;
            animator.SetBool("dead", true);
            GameManager.Instance.LevelEnd(false);
        }
    }


    // AVOID GOING OFF SCREEN
    void ScreenLimitControl()
    {
        float yMin = -yMax;
        rigidbody2D.position = new Vector2(rigidbody2D.position.x,
            Mathf.Clamp(rigidbody2D.position.y, yMin, yMax));
    }

    public void Kill()
    {
        Destroy(this.gameObject);
    }
}
