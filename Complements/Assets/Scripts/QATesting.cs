using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QATesting : MonoBehaviour {

    public GameObject playerOne;
    public GameObject playerTwo;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerOne.transform.position = new Vector3(93,-4,0);
            playerTwo.transform.position = new Vector3(98, -4, 0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerOne.transform.position = new Vector3(196, -6, 0);
            playerTwo.transform.position = new Vector3(202, -6, 0);
        }

    }
}
