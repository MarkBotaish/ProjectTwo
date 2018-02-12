using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class RightArmMove : MonoBehaviour {

	//needs work

	public int playerId;
	private Player player;
	public Transform target;

	public float speed;

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
		Vector3 zAxis = new Vector3 (0, 0, 1);
		if (player.GetButton ("MoveRightArm")) {
			transform.RotateAround (target.position, zAxis, speed);
		}
	}
}

