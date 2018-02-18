using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerMovement : MonoBehaviour {

	public int playerId;
	private Player player;
	Rigidbody2D rb;
	public Transform groundCheck;
    public GameObject playerObject;
    public GameObject leftArm;
    public GameObject rightArm;
    public GameObject rightGrab;

	public float speed, jumpSpeed, climpSpeed;
	public bool grounded = false;
	public LayerMask ground;
    Vector3 climbR = Vector3.zero;

	// Use this for initialization
	void Start () {
		player = ReInput.players.GetPlayer (playerId);
        rb = playerObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		CheckInput ();
        grounded = Physics2D.OverlapCircle (groundCheck.position, 0.2f, ground);
	}

	void FixedUpdate()
	{
		
	}

	void CheckInput()
	{
		if (player.GetAxis ("Move Horizontal") > 0.0f) 
            rb.velocity = new Vector2(speed, rb.velocity.y);
		

        if (player.GetAxis("Move Horizontal") < 0.0f)
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        

        if (player.GetAxis("Move Horizontal") == 0.0f)
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
         
        
        if (player.GetButtonDown ("Jump") && grounded) {
            rb.velocity = transform.up * jumpSpeed;
			Debug.Log ("Jump");
		}

        if (player.GetButton("Right Grab"))
        {
            
            if (climbR == Vector3.zero)
            {
                rb.velocity = Vector3.zero;
                rb.gravityScale = (0.0f);
                climbR = rightGrab.transform.position;
               
            }
            if(Vector2.Distance(climbR, rightGrab.transform.position) < 5.0f && !(rightArm.transform.rotation.eulerAngles.z > 240 && rightArm.transform.rotation.eulerAngles.z < 300)) 
            {
                //playerObject.transform.position += new Vector3(1.2f, 0.4f,0.0f);
                playerObject.transform.RotateAround(climbR, -Vector3.forward, climpSpeed * Time.deltaTime);
                //playerObject.transform.position -= new Vector3(1.2f, 0.4f,0.0f);
                float rightAngle = Mathf.Atan2((rightArm.transform.position.y - climbR.y), -1.0f * (rightArm.transform.position.x - climbR.x)) * 180 / Mathf.PI;
                rightArm.transform.rotation = Quaternion.Euler(0, 0, -rightAngle);
                playerObject.transform.rotation = Quaternion.Euler (0.0f,0.0f,0.0f);

            }
           
        }
        else
        {
            climbR = Vector3.zero;
            rb.gravityScale = (30.0f);
        }
        if ((player.GetAxis("Right Joystick X") != 0.0f || player.GetAxis("Right Joystick Y") != 0.0f) && !player.GetButton("Right Grab"))
        {
            float rightAngle = Mathf.Atan2(-player.GetAxis("Right Joystick Y"), player.GetAxis("Right Joystick X")) * 180 / Mathf.PI;
            float leftAngle = Mathf.Atan2(player.GetAxis("Right Joystick Y"), -player.GetAxis("Right Joystick X")) * 180 / Mathf.PI;
            rightArm.transform.rotation = Quaternion.Euler(0, 0, -rightAngle);
            leftArm.transform.rotation = Quaternion.Euler(0, 0, -leftAngle);
        }

    }
}
