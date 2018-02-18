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
        if (player.GetAxis("Right Joystick X") != 0.0f || player.GetAxis("Right Joystick Y") != 0.0f)
        {
            float angle = Mathf.Atan2(-player.GetAxis("Right Joystick Y"), player.GetAxis("Right Joystick X")) * 180 / Mathf.PI;
            target.transform.rotation = Quaternion.Euler(0, 0, -angle);
        }
    }
}

