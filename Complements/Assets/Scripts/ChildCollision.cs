using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCollision : MonoBehaviour {

    public PlayerMovement player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Climbable")
        {

            if (gameObject.tag == "Chest")
                player.setChestCollision(true);

            if (gameObject.tag == "RGrab")
                player.setRightGrab(true);

            if (gameObject.tag == "LGrab")
                player.setLeftGrab(true);

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Climbable")
        {

            if (gameObject.tag == "Chest")
                player.setChestCollision(false);

            if (gameObject.tag == "RGrab")
                player.setRightGrab(false);

            if (gameObject.tag == "LGrab")
                player.setLeftGrab(false);
        }
    }
}
