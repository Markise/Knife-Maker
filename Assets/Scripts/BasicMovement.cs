using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{

    private float maxJump;
    private Vector2 startJumpPos;
    private Rigidbody2D rb;
    private bool jumpBypass;
    private bool firstGround;
    private string direction;

    public float speed;
    public float jump;
    public float jumpheight;
    public float jumpAccel;
    public float jumpDecel;
    public float maxDescendSpeed;
    public float minDescendSpeed;
    public float landSlowDown;
    public float groundRadius;
    public bool grounded1;
    public bool grounded2;
    public Transform groundCheck;
    public Transform groundCheck2;
    public LayerMask ground;
    Animator anim;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        maxJump = jump;
        jumpBypass = false;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.localScale = new Vector3(-1, 1f, .1f);
            anim.SetBool("Walking", true);
            direction = "left";
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.localScale = new Vector3(1, 1f, .1f);
            anim.SetBool("Walking", true);
            direction = "right";
        }

        if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.D))
        {
            direction = null;
            anim.SetBool("Walking", false);
            Movement(0f);
        }
        Jump();
    }

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f);
        if (!Grounded() && hit.collider.tag == "Ground")
        {
            rb.velocity = new Vector2(rb.velocity.x - landSlowDown, rb.velocity.y);
        }
/*
        if (!Grounded())
        {
            Decel();
        }
*/
        if (direction == "left"){
            Movement(-speed); 
        }
        else if (direction == "right"){
            Movement(speed); 
        }

        if (jumpBypass)
        {
            Debug.Log("Jumpforce");
            JumpForce();
            jump += jumpAccel;
        }else if(!Grounded())
        { Decel(); }
    }

    void Movement(float speed)
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && Grounded() || Input.GetKeyDown(KeyCode.Space) && Grounded())
        {
            firstGround = false;
            Debug.Log("Jump");
            startJumpPos = transform.position;
            jumpBypass = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W) || transform.position.y >= startJumpPos.y + jumpheight || rb.velocity.y < 0f) 
        {
            Debug.Log("EndJump");
            jumpBypass = false;
            jump = maxJump;            
        }
    }
    void JumpForce()
    {
        Debug.Log("Jumpforce");
        // rb.AddForce(Vector2.up * jump * Time.deltaTime, ForceMode2D.Impulse);
        rb.velocity += -Vector2.up * Physics2D.gravity.y * jump * Time.deltaTime;
    }

    void Decel()
    {
        Debug.Log("Decel");
        rb.velocity += Vector2.up * Physics2D.gravity.y * jumpDecel * Time.deltaTime;
    }
   public bool Grounded()
    {
        
        grounded1 = Physics2D.OverlapCircle(groundCheck.position, groundRadius, ground);
        grounded2 = Physics2D.OverlapCircle(groundCheck2.position, groundRadius, ground);

        if(!firstGround)
        {
            rb.velocity = new Vector2(rb.velocity.x -2, rb.velocity.y+1);
            rb.velocity = Vector2.zero;
            firstGround = true;
        }

        if (grounded1 || grounded2)
        {
            anim.SetBool("inAir", false);
            return true;
        }
        else
        {
            anim.SetBool("inAir", true);
            return false;
        }       
    }

}
