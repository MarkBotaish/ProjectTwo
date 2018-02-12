using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerMovement : MonoBehaviour {

	public int playerId;
	private Player player;
	Rigidbody2D rb;
	public Transform groundCheck;

	public float speed, jumpSpeed;
	public bool grounded = false;
	public LayerMask ground;

	// Use this for initialization
	void Start () {
		player = ReInput.players.GetPlayer (playerId);
		rb = GetComponent<Rigidbody2D> ();
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
	}
}
