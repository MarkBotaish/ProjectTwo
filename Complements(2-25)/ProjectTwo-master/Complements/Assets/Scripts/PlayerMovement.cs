using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerMovement : MonoBehaviour {

    public int playerId;
    public float speed, jumpSpeed, climpSpeed;
    public Transform groundCheck;
    public GameObject playerObject;
    public GameObject leftArm;
    public GameObject rightArm;
    public GameObject rightGrab;
    public GameObject leftGrab;
	public LayerMask ground;

    Vector3 climbR = Vector3.zero;
    Player player;
    Rigidbody2D rb;

    bool grounded = false;
    bool chestCollide = false;
    bool canRightGrab = false;
    bool canLeftGrab = false;
    bool hasPressedRight = false;
    bool hasPressedLeft = false;


    float mulitplier = 1.0f;
    public bool canJump = false;

	//Particle Effects - Andy
	public GameObject grabEffect;


    // Use this for initialization
    void Start () {
		player = ReInput.players.GetPlayer (playerId);
        rb = playerObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		CheckInput ();
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, ground);
	}

	void FixedUpdate()
	{
		
	}

    void CheckInput()
    {
        if (player.GetAxis("Move Horizontal") > 0.0f && !(hasPressedLeft || hasPressedRight))
            rb.velocity = new Vector2(speed, rb.velocity.y);


        if (player.GetAxis("Move Horizontal") < 0.0f && !(hasPressedLeft || hasPressedRight))
            rb.velocity = new Vector2(-speed, rb.velocity.y);


        if (player.GetAxis("Move Horizontal") == 0.0f && !(hasPressedLeft || hasPressedRight))
            rb.velocity = new Vector2(0.0f, rb.velocity.y);

        if (grounded)
        {
            canJump = false;
        }
         

        if (player.GetButtonDown("Jump") && (grounded || canJump))
        {
          
            rb.velocity = transform.up * jumpSpeed;
            Debug.Log("Jump");
            if (canJump)
            {
                hasPressedLeft = false;
                hasPressedRight = false;
                canLeftGrab = false;
                canRightGrab = false;
                rb.gravityScale = (30.0f);
                rb.velocity = new Vector3(5,0,0) * jumpSpeed;
            }
            rb.velocity = transform.up * jumpSpeed;
            canJump = false;
        }

        if (player.GetButton("Right Grab") && canRightGrab)
        {
            if (!hasPressedRight)
			{
				canJump = true;
                hasPressedRight = true;
                rb.velocity = Vector3.zero;
                rb.gravityScale = (0.0f);
                climbR = rightGrab.transform.position;
				GrabEffect(climbR);
				if (rightGrab.transform.position.x - gameObject.transform.position.x <= 0)
                    mulitplier = 1;
                else
                    mulitplier = -1;
            }
            if (!(chestCollide && rightArm.transform.rotation.eulerAngles.z > 270) && !(rightArm.transform.rotation.eulerAngles.z > 240 && rightArm.transform.rotation.eulerAngles.z < 300))
            {
                playerObject.transform.RotateAround(climbR, mulitplier * Vector3.forward, climpSpeed * Time.deltaTime);

                float rightAngle = Mathf.Atan2((rightArm.transform.position.y - climbR.y), -1.0f * (rightArm.transform.position.x - climbR.x)) * 180 / Mathf.PI;
                rightArm.transform.rotation = Quaternion.Euler(0, 0, -rightAngle);
                playerObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

            }

        }
        if (player.GetButton("Left Grab") && canLeftGrab)
        {
            if (!hasPressedLeft)
			{
				canJump = true;
                hasPressedLeft = true;
                rb.velocity = Vector3.zero;
                rb.gravityScale = (0.0f);
                climbR = leftGrab.transform.position;
				GrabEffect(climbR);

                if (leftGrab.transform.position.x - gameObject.transform.position.x <= 0)
                    mulitplier = 1;
                else
                    mulitplier = -1;

            }
            if (!(chestCollide && leftArm.transform.rotation.eulerAngles.z < 180) && !(leftArm.transform.rotation.eulerAngles.z < 135 && leftArm.transform.rotation.eulerAngles.z > 75))
            {
                playerObject.transform.RotateAround(climbR, mulitplier * Vector3.forward, climpSpeed * Time.deltaTime);
                float rightAngle = Mathf.Atan2(-1.0f * (leftArm.transform.position.y - climbR.y), (leftArm.transform.position.x - climbR.x)) * 180 / Mathf.PI;
                leftArm.transform.rotation = Quaternion.Euler(0, 0, -rightAngle);
                playerObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            }
        }


        if(!player.GetButton("Left Grab"))
        {
            hasPressedLeft = false;
        }
        if (!player.GetButton("Right Grab"))
        {
            hasPressedRight = false;
        }

        if(!hasPressedLeft && !hasPressedRight)
        {
            rb.gravityScale = (30.0f);
        }

        if ((player.GetAxis("Right Joystick X") != 0.0f || player.GetAxis("Right Joystick Y") != 0.0f) && !player.GetButton("Right Grab"))
        {
            float rightAngle = Mathf.Atan2(-player.GetAxis("Right Joystick Y"), player.GetAxis("Right Joystick X")) * 180 / Mathf.PI;
            rightArm.transform.rotation = Quaternion.Euler(0, 0, -rightAngle);
        }

        if ((player.GetAxis("Right Joystick X") != 0.0f || player.GetAxis("Right Joystick Y") != 0.0f) && !player.GetButton("Left Grab"))
        {
            float leftAngle = Mathf.Atan2(player.GetAxis("Right Joystick Y"), -player.GetAxis("Right Joystick X")) * 180 / Mathf.PI;
            leftArm.transform.rotation = Quaternion.Euler(0, 0, -leftAngle);
        }
    }
    public void setChestCollision(bool tof) {
        chestCollide = tof;
    }

    public void setRightGrab(bool tof)
    {
        canRightGrab = tof;
    }

    public void setLeftGrab(bool tof)
    {
        canLeftGrab = tof;
    }

	//Spawns particles at location of player's hand
	private void GrabEffect(Vector2 _spawnPos)
	{
		GameObject grabParticles = Instantiate(grabEffect);
		Vector2 spawnPos = _spawnPos;
		grabParticles.transform.position = spawnPos;
		SoundManager.code.PlayGrab();
	}

}
