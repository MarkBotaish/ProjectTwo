using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PickUpRock : MonoBehaviour {

	public int playerId;
    public PlayerMovement playerObject;
	private Player player;
    Transform arm;
	Rigidbody2D rb;
    float velocity = 0;

	bool canHold = false;
    bool isHolding = false;
    bool right = false;
    bool left = false;

	// Use this for initialization
	void Start () {
		player = ReInput.players.GetPlayer (playerId);
		rb = GetComponent<Rigidbody2D> ();
		arm = null;
	}
	
	// Update is called once per frame
	void Update () {
		if((player.GetButtonDown("Right Object") || player.GetButtonDown("Left Object")) && arm != null && !isHolding){
			GetComponent<BoxCollider2D> ().isTrigger = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            isHolding = true;
            print("WHY");

            if (player.GetButtonDown("Right Object"))
                right = true;
            else
                left = true;
		}
        if (isHolding && player.GetButtonUp("Right Object") && right) {
            resetCube();
		}else if (isHolding && player.GetButtonUp("Left Object") && left) {
            resetCube();
		}
        if (isHolding)
        {
            gameObject.transform.position = arm.position;
            velocity = playerObject.getdeltaLeft();
           // print(velocity);
        }

	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.gameObject.tag == "RGrab" || col.gameObject.tag == "LGrab") && !isHolding) {
			arm = col.transform;
            print("shit");
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if ((col.gameObject.tag == "RGrab" || col.gameObject.tag == "LGrab") && !isHolding) {
			arm = null;
        }
	}

    void resetCube()
    {
        arm.DetachChildren();
        GetComponent<BoxCollider2D>().isTrigger = false;
        rb.constraints = RigidbodyConstraints2D.None;
        
        rb.velocity = Vector3.up * (velocity / 180) * 200;
        if (rb.velocity.magnitude > 15)
            rb.velocity = rb.velocity.normalized * 15;

        print(rb.velocity);
        isHolding = false;
        arm = null;

        if (right)
            right = false;
        else
            left = false;

        print("RESTING");
    }
}
