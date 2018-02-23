using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class RopeScript : MonoBehaviour {


	public int playerId;
	private Player player;
	Rigidbody2D rb;
	bool canClimb = false;

	public float speed;

	// Use this for initialization
	void Start () {
		player = ReInput.players.GetPlayer (playerId);
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(canClimb && player.GetButton("Interact"))
			rb.velocity = new Vector2(rb.velocity.x, speed);

		if(canClimb && player.GetButton("Interact"))
			rb.velocity = new Vector2(rb.velocity.x, -speed);

		if(canClimb && player.GetButton("Interact"))
			rb.velocity = new Vector2(rb.velocity.x, 0.0f);
			
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Rope")
			canClimb = true;
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == "Rope")
			canClimb = false;
	}
}
