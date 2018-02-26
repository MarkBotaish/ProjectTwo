using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class ObjectInteraction : MonoBehaviour {

	public int playerId;
	private Player player;
	public Transform arm;

	bool canHold = false;
    bool isHolding = false;

	// Use this for initialization
	void Start () {
		player = ReInput.players.GetPlayer (playerId);
		arm = null;
	}
	
	// Update is called once per frame
	void Update () {
		if(player.GetButtonDown("Interact") && arm != null && canHold && !isHolding ){
            transform.position = arm.transform.position;
            gameObject.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;


            transform.SetParent (arm);
            isHolding = true;
		}else if (player.GetButtonDown("Interact") && isHolding)
        {
            gameObject.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            arm.gameObject.transform.DetachChildren();
            arm = null;
            canHold = false;
            isHolding = false;
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
        if ((col.gameObject.tag == "RGrab" || col.gameObject.tag == "LGrab") && !isHolding)
        {
			canHold = false;
			arm = null;
		}
	}
}
