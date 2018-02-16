using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class LeftArmMove : MonoBehaviour {

    HingeJoint2D hinge;
    public int playerId = 0;
    private Player player;
    JointMotor2D motor1;

    // Use this for initialization
    void Start () {
        hinge = gameObject.GetComponent<HingeJoint2D>();
        player = ReInput.players.GetPlayer(playerId);
        motor1.motorSpeed = 2000;
        motor1.maxMotorTorque = 1000f;

        hinge.motor = motor1;
    }
	
	// Update is called once per frame
	void Update () {

        if (player.GetButton("MoveLeftArm"))
        {
            hinge.useMotor = true;
        }
        else
        {
            hinge.useMotor = false;
        }
     
	}
}
