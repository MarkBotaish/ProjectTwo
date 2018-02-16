using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class RightArmMove : MonoBehaviour {

	//needs work

	public int playerId;
	private Player player;
	public GameObject target;
    public int speed;

    float angle;

	// Use this for initialization
	void Start () {
		player = ReInput.players.GetPlayer (playerId);
	}

	// Update is called once per frame
	void Update () {
		CheckInput ();
	}

	void CheckInput()
	{
		if (player.GetButton ("MoveRightArm")) {
            target.transform.Rotate(Vector3.forward * speed);
        }
	}
}

