using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinState : MonoBehaviour {

	//we can rework this as we please

	public int numOfObjects = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "Chest")
		{
			numOfObjects++;
			if(numOfObjects >= 2)
			{
				GameObject.Find ("WinText").GetComponent<Text> ().enabled = true;
			}
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if(col.gameObject.tag == "Chest")
		{
			numOfObjects--;
			if(numOfObjects < 2)
			{
				GameObject.Find ("WinText").GetComponent<Text> ().enabled = false;
			}
		}
	}
}
