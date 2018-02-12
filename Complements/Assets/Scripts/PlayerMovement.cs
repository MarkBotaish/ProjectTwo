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
	}

	void FixedUpdate()
	{
		grounded = Physics2D.OverlapCircle (groundCheck.position, 0.2f, ground);
	}

	void CheckInput()
	{
		if (player.GetAxis ("Move Horizontal") != 0.0f) {
			rb.AddForce (new Vector2 (player.GetAxis ("Move Horizontal") * speed, 0));
		}
		if (player.GetButtonDown ("Jump") && grounded) {
			rb.AddForce (transform.up * jumpSpeed);
			Debug.Log ("Jump");
		}
	}
}
