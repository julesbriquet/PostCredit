using UnityEngine;
using System.Collections;

public class ZeldaPlayerController : MonoBehaviour {

    public enum FaceDirection
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    public int playerNumber = 1;

    public float speed;
    public Vector2 velocity;

    public FaceDirection directionFacing;
    private bool Attacking;

    private Animator anim;

    public Collider2D colliderUp;
    public Collider2D colliderDown;
    public Collider2D colliderLeft;

	// Use this for initialization
	void Start () {
        anim = this.GetComponent<Animator>();
        directionFacing = FaceDirection.DOWN;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        string InputPlayerString = "";
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

        float inputY = Input.GetAxisRaw("Vertical" + InputPlayerString);
        float inputX = Input.GetAxisRaw("Horizontal" + InputPlayerString);
        bool triggerAction = false;
        triggerAction = Input.GetButton("Action" + InputPlayerString);

        

        if (triggerAction)
        {
            velocity = Vector2.zero;
            Attacking = true;
            anim.SetBool("Attacking", Attacking);

            if (directionFacing == FaceDirection.UP)
                colliderUp.gameObject.SetActive(true);
            else if (directionFacing == FaceDirection.DOWN)
                colliderDown.gameObject.SetActive(true);
            else if (directionFacing == FaceDirection.LEFT || directionFacing == FaceDirection.RIGHT)
                colliderLeft.gameObject.SetActive(true);
        }

        if (!Attacking)
        {
            velocity = (new Vector2(inputX, inputY).normalized) * speed * Time.deltaTime;
            /*
             * HANDLING DIRECTIONS
             */
            if (inputY > 0)
            {
                directionFacing = FaceDirection.UP;
            }
            else if (inputY < 0)
            {
                directionFacing = FaceDirection.DOWN;
            }
            else if (inputX < 0)
            {
                if (directionFacing != FaceDirection.LEFT)
                {
                    float xScale = Mathf.Abs(transform.localScale.x);
                    transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
                }
                directionFacing = FaceDirection.LEFT;
            }
            else if (inputX > 0)
            {

                if (directionFacing != FaceDirection.RIGHT)
                {
                    float xScale = Mathf.Abs(transform.localScale.x);
                    transform.localScale = new Vector3(-xScale, transform.localScale.y, transform.localScale.z);
                }
                directionFacing = FaceDirection.RIGHT;
            }

            setDirectionInAnimation(directionFacing);
        }


        rigidbody2D.velocity = velocity;
        anim.SetFloat("velocity", Mathf.Abs(velocity.x) + Mathf.Abs(velocity.y));
	}

    void setDirectionInAnimation(FaceDirection direction)
    {
        if (direction == FaceDirection.UP)
        {
            anim.SetBool("FacingUp", true);
            anim.SetBool("FacingDown", false);
            anim.SetBool("FacingLeft", false);
        }
        else if (direction == FaceDirection.DOWN)
        {
            anim.SetBool("FacingDown", true);
            anim.SetBool("FacingUp", false);
            anim.SetBool("FacingLeft", false);
        }
        else if (direction == FaceDirection.LEFT || direction == FaceDirection.RIGHT)
        {
            anim.SetBool("FacingLeft", true);
            anim.SetBool("FacingUp", false);
            anim.SetBool("FacingDown", false);
        }
    }

    void AttackFinished()
    {
        this.Attacking = false;
        anim.SetBool("Attacking", Attacking);

        colliderUp.gameObject.SetActive(false);
        colliderDown.gameObject.SetActive(false);
        colliderLeft.gameObject.SetActive(false);
    }
}
