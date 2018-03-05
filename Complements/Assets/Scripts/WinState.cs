using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Rewired;

public class WinState : MonoBehaviour {

	public int playerId;
	Player player;
	public int numOfObjects = 0;
	public bool canRestart = false;

	// Use this for initialization
	void Start () {
		player = ReInput.players.GetPlayer (playerId);
	}
	
	// Update is called once per frame
	void Update () {
		if (player.GetButtonDown ("Interact") && canRestart)
			SceneManager.LoadScene ("MainMenu");
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "Chest")
		{
			numOfObjects++;
			if(numOfObjects >= 2)
			{
				GameObject.Find ("Timer").GetComponent<Timer> ().enabled = false;
				GameObject.Find ("WinText").GetComponent<Text> ().enabled = true;
				canRestart = true;
			}
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if(col.gameObject.tag == "Chest")
		{
			numOfObjects--;
		}
	}
}
