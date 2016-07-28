﻿using UnityEngine;
using System.Collections;

public class Upgrades : MonoBehaviour {
	//Name has to be the same 
	public string upgradeType;

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.name == "Char") {
			//Play correct music and animation depending on what switch-option is chosen
			switch (upgradeType) {

			case "Float":
				col.gameObject.GetComponent<CharFloat>().enabled = true;
				break;

			case "HighJump":
				col.gameObject.GetComponent<CharJump> ().jumpSpeed = 15f;
				break;

			case "WallJump":
				col.gameObject.GetComponent<WallJump> ().enabled = true;
				break;

			case "Stomp":
				col.gameObject.GetComponent<CharStomp> ().enabled = true;
				break;
			
			case "Laser":
				col.gameObject.transform.GetChild (8).gameObject.SetActive (true);
				break;

			case "Health":
				col.gameObject.GetComponent<CharHealth> ().IncreaseMaxHealth ();
				break;

			case "Speed":
				col.gameObject.GetComponent<CharMovement> ().moveSpeed++;
				break;
			
			case "Energy":
				col.gameObject.GetComponent<CharEnergy> ().IncreaseMaxEnergy ();
				break;
			}
			Destroy (gameObject);
		}
	}
}