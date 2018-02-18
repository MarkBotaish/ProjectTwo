﻿using System.Collections;
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
    bool canRightGrab = true;
    bool canLeftGrab = true;
    bool hasPressedRight = false;
    bool hasPressedLeft = false;

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
        if (player.GetAxis("Move Horizontal") > 0.0f)
            rb.velocity = new Vector2(speed, rb.velocity.y);


        if (player.GetAxis("Move Horizontal") < 0.0f)
            rb.velocity = new Vector2(-speed, rb.velocity.y);


        if (player.GetAxis("Move Horizontal") == 0.0f)
            rb.velocity = new Vector2(0.0f, rb.velocity.y);


        if (player.GetButtonDown("Jump") && grounded)
        {
            rb.velocity = transform.up * jumpSpeed;
            Debug.Log("Jump");
        }

        if (player.GetButton("Right Grab") && canRightGrab)
        {

            if (!hasPressedRight)
            {
                hasPressedRight = true;
                rb.velocity = Vector3.zero;
                rb.gravityScale = (0.0f);
                climbR = rightGrab.transform.position;

            }
            if (!(chestCollide && rightArm.transform.rotation.eulerAngles.z > 270) && !(rightArm.transform.rotation.eulerAngles.z > 240 && rightArm.transform.rotation.eulerAngles.z < 300))
            {
                //playerObject.transform.position += new Vector3(1.2f, 0.4f,0.0f);
                playerObject.transform.RotateAround(climbR, -Vector3.forward, climpSpeed * Time.deltaTime);
                //playerObject.transform.position -= new Vector3(1.2f, 0.4f,0.0f);
                float rightAngle = Mathf.Atan2((rightArm.transform.position.y - climbR.y), -1.0f * (rightArm.transform.position.x - climbR.x)) * 180 / Mathf.PI;
                rightArm.transform.rotation = Quaternion.Euler(0, 0, -rightAngle);
                playerObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

            }

        }
        else if (player.GetButton("Left Grab") && canLeftGrab)
        {
            if (!hasPressedLeft)
            {
                hasPressedLeft = true;
                rb.velocity = Vector3.zero;
                rb.gravityScale = (0.0f);
                climbR = leftGrab.transform.position;

            }
            if (!(chestCollide && leftArm.transform.rotation.eulerAngles.z < 180) && !(leftArm.transform.rotation.eulerAngles.z < 135 && leftArm.transform.rotation.eulerAngles.z > 75))
            {
                playerObject.transform.RotateAround(climbR, -Vector3.forward, climpSpeed * Time.deltaTime);
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
            print(leftArm.transform.rotation.eulerAngles.z);
        }
    }
    public void setChestCollision(bool tof) {
        chestCollide = tof;
    }
}
