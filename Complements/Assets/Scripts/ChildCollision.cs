using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCollision : MonoBehaviour {

    public PlayerMovement player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Shit");
        if (gameObject.tag == "Chest")
            player.setChestCollision(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (gameObject.tag == "Chest")
            player.setChestCollision(false);
    }
}
