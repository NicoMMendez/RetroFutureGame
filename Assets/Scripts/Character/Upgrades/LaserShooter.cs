using UnityEngine;
using System.Collections;

public class LaserShooter : MonoBehaviour {
	private LineRenderer lineRenderer;
	public Transform laserHit;
	CharEnergy CharEnergy;
	bool holdShoot, canShoot = true;

	void Start() {
		//Change player sprite and display tutorial
		lineRenderer = GetComponent<LineRenderer> ();
		//lineRenderer.useWorldSpace = true;
		CharEnergy = GameObject.Find("Char").GetComponent<CharEnergy> ();
	}

	void Update() {
		if (!Input.GetButton ("Shoot")) {
			holdShoot = false;
		}
		if (Input.GetButton ("Shoot") && !holdShoot && canShoot) {
			holdShoot = true;
			if (CharEnergy.UseEnergy (2)) {
				canShoot = false;
				//CharEnergy.UseEnergy (2);
				ActivateLaser ();
			}
		}
	}

	void ActivateLaser() {
		lineRenderer.enabled = true;
		RaycastHit2D hit = Physics2D.Raycast (transform.position, transform.right);
		Debug.DrawLine (transform.position, hit.point);
		laserHit.position = new Vector3(hit.point.x, hit.point.y, -5);
		lineRenderer.SetPosition (0, transform.position);
		lineRenderer.SetPosition (1, laserHit.position);
		StartCoroutine (ShrinkLaser ());
		HitByLaser (hit);
	}

	void HitByLaser(RaycastHit2D victim) {
		switch(victim.transform.gameObject.tag) {
			//Add more cases as more types of enemies are added to the game
		case "SoftEnemy":
			Debug.Log ("Enemy hit by laser!!");
			victim.transform.gameObject.GetComponent<SmallCritter> ().TakeDamage (3);
			victim.transform.gameObject.GetComponent<Knockback>().Knock(this.gameObject, 3f);
			break;

		case "BirdBossWeakSpot":
			Debug.Log ("Hit Bird in the Mouth!");
			victim.transform.parent.gameObject.GetComponent<BigBadBird> ().GetHurt ();
			break;

		case "BigEyeGuyWeakSpot":
			Debug.Log ("Hit EyeGuy in the Eye!");
			victim.transform.parent.gameObject.GetComponent<BigEyeGuy> ().GetHurt (2);
			break;
		}
	}

	IEnumerator ShrinkLaser() {
		Vector3 target = transform.position;
		Invoke ("CanShoot", 1);
		if (transform.parent.gameObject.GetComponent<CharStatus> ().isMirrored) {
			while (lineRenderer.enabled && Mathf.Abs (target.x) > Mathf.Abs (laserHit.position.x)) {
				lineRenderer.SetPosition (0, target);
				target -= transform.right * -1;
				yield return new WaitForSeconds (0.01f);
			}
		}
		else {
			while (lineRenderer.enabled && Mathf.Abs (target.x) < Mathf.Abs (laserHit.position.x)) {
				lineRenderer.SetPosition (0, target);
				target -= transform.right * -1;
				yield return new WaitForSeconds (0.01f);
			}
		}
		lineRenderer.enabled = false;
	}

	void CanShoot() {
		canShoot = true;
	}
}