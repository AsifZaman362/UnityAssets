using UnityEngine;

public class PlayerController : MessageClient {

    #region Serialized Vars
    [SerializeField]
    private Animator m_animator;
    [SerializeField]
    private bool canWalk = true, canRun = true;
    [SerializeField]
    private float speed_walk = 10.0f, speed_run_multiplier = 2.5f;
    [SerializeField]
    private float jump_force = 10f;
    [SerializeField]
    private int ground_layer = 8;
    [SerializeField]
    private float ground_check_distance = 1f;
    [SerializeField]
    private Transform ground_check_object;
    #endregion

    #region Private Vars
    private float origSpeed_walk;
    private Rigidbody2D m_rigidBody;
    private bool isFacingRight = true, IsGrounded;
    private bool isRunning, isWalking;
    private int playingState_0 = -1;
    private bool canMove = true;
	#endregion


	void Start () {

		origSpeed_walk = speed_walk;
		isFacingRight = transform.localScale.x < 0;
		m_rigidBody = GetComponent<Rigidbody2D>();
        base.Start();
		
	}
	

	
	void Update () {

        if (!canMove)
        {
            m_rigidBody.velocity = new Vector2(0, m_rigidBody.velocity.y);
            isWalking = isRunning = false;
            m_animator.Play("idle");
            playingState_0 = 0;
        }
        else
        {
            isRunning = Input.GetButton("Sprint") && canRun && IsGrounded && Mathf.Abs(m_rigidBody.velocity.x) > 0;
            if (isRunning && speed_walk == origSpeed_walk)
                speed_walk *= speed_run_multiplier;
            else if (!isRunning && speed_walk != origSpeed_walk)
                speed_walk = origSpeed_walk;

            isWalking = Mathf.Abs(Input.GetAxis("Horizontal")) > 0.0f && !isRunning && canWalk;

            if (isWalking && playingState_0 != 1 && IsGrounded)
            {
                playingState_0 = 1;
                m_animator.Play("walk");
            }
            else if (!isRunning && !isWalking && playingState_0 != 0 && IsGrounded)
            {
                playingState_0 = 0;
                m_animator.Play("idle");
            }
            else if (isRunning && playingState_0 != 2 && IsGrounded)
            {
                playingState_0 = 2;
                m_animator.Play("run");
            }

            if (isFacingRight && Input.GetAxis("Horizontal") < 0)
            {
                Flip();
            }
            else if (!isFacingRight && Input.GetAxis("Horizontal") > 0)
            {
                Flip();
            }

            float moveX = Input.GetAxis("Horizontal") * speed_walk;
            bool jump = Input.GetButtonDown("Jump") && IsGrounded;

            if (jump)
            {
                jump = false;
                m_rigidBody.AddForce(new Vector2(0, jump_force), ForceMode2D.Impulse);
            }
            if (canWalk)
            {
                Vector3 velocity = m_rigidBody.velocity;
                velocity.x = moveX;
                m_rigidBody.velocity = velocity;
            }

            if (Mathf.Abs(m_rigidBody.velocity.y) > 0)
            {
                playingState_0 = 3;
                //m_animator.Play("Jump");
            }

            if ((!IsGrounded && m_rigidBody.velocity.y <= 0) && m_rigidBody.gravityScale == 3)
            {
                m_rigidBody.gravityScale = 5;
            }
            else if (IsGrounded && m_rigidBody.gravityScale == 5)
            {
                m_rigidBody.gravityScale = 3;
            }
        }

	}

	void Flip(){

		isFacingRight = !isFacingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;

	}

    void OnTriggerEnter2D(Collider2D m_coll)
    {
        if (m_coll.gameObject.layer == ground_layer)
        {
            IsGrounded = true;
        }
        else if (m_coll.tag == "Character")
        {
            m_coll.gameObject.GetComponent<DialogueSystem>().enabled = true;
        }
    }


    void OnTriggerExit2D(Collider2D m_coll)
    {
        if (m_coll.gameObject.layer == ground_layer)
        {
            IsGrounded = false;
        }
    }

    public override void OnMessageRecieve(string message)
    {
        if(message == "stopController")
        {
            canMove = false;
        }
        else if(message == "startController")
        {
            canMove = true;
        }
    }

}
