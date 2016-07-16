using UnityEngine;
using System.Collections;

public class LaserShooter : MonoBehaviour {
	private LineRenderer lineRenderer;
	public Transform laserHit;
	CharEnergy charEnergy;
	CharStatus charStatus;
	int wayOfTurn;
	bool holdShoot;

	void Start() {
		//Change player sprite and display tutorial
		lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.useWorldSpace = true;
		charEnergy = GameObject.Find("char").GetComponent<CharEnergy> ();
		charStatus = GameObject.Find("char").GetComponent<CharStatus> ();
	}

	void Update() {
		if (!Input.GetButton ("Shoot")) {
			holdShoot = false;
		}

		if (charEnergy.HasJuice (2) && Input.GetButton ("Shoot") && !holdShoot) {
			Debug.Log ("shoot");
			holdShoot = true;
			charEnergy.UseEnergy (2);
			ShootGun ();
			if (charStatus.isMirrored) {
				wayOfTurn = -1;
			} else {
				wayOfTurn = 1;
			}
		}
	}

	void ShootGun() {
		//Should later on try to shoot the player instead of just downwards.
		lineRenderer.enabled = true;
		//RaycastHit2D hit = Physics2D.Raycast (transform.position, wayOfTurn * transform.right);

		//Debug.DrawLine (transform.position, hit.point);
		//laserHit.position = hit.point;
		lineRenderer.SetPosition (0, transform.position);
		lineRenderer.SetPosition (1, laserHit.position * 10 * wayOfTurn);
		//HitByLaser (hit);
		Invoke ("KillLaser", 1f);
	}

	void  KillLaser() {
		lineRenderer.enabled = false;
	}

	void HitByLaser(RaycastHit2D victim) {
		switch(victim.transform.gameObject.tag) {
			//Add more cases as more types of enemies are added to the game
		case "softEnemy":
			Debug.Log ("Enemy hit by laser!!");
			victim.transform.gameObject.GetComponent<SmallCritter> ().GetHurt (3);
			victim.transform.gameObject.GetComponent<Knockback>().Knock(this.gameObject, 3f);
			//Take away some of the player health
			break;
		}
	}
}