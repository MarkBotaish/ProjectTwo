using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PickUpRock : MonoBehaviour {

	public int playerId;
	private Player player;
	private Transform arm;
	Rigidbody2D rb;

	bool canHold = false;

	// Use this for initialization
	void Start () {
		player = ReInput.players.GetPlayer (playerId);
		rb = GetComponent<Rigidbody2D> ();
		arm = null;
	}
	
	// Update is called once per frame
	void Update () {
		if((player.GetButtonDown("Right Grab") || player.GetButtonDown("Left Grab")) && arm != null && canHold){
			transform.SetParent (arm);
			rb.bodyType = RigidbodyType2D.Kinematic;
			GetComponent<BoxCollider2D> ().isTrigger = true;
		}

		if (transform.parent.tag == "RGrab" && player.GetButtonDown("Right Grab")) {
			transform.SetParent (null);
			rb.bodyType = RigidbodyType2D.Dynamic;
			GetComponent<BoxCollider2D> ().isTrigger = false;
		}

		if (transform.parent.tag == "LGrab" && player.GetButtonDown("Left Grab")) {
			transform.SetParent (null);
			rb.bodyType = RigidbodyType2D.Dynamic;
			GetComponent<BoxCollider2D> ().isTrigger = false;
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "RGrab" || col.gameObject.tag == "LGrab") {
			canHold = true;
			arm = col.transform;
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag == "RGrab" || col.gameObject.tag == "LGrab") {
			canHold = false;
			arm = null;
		}
	}
}
