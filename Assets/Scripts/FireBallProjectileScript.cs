using UnityEngine;
using System.Collections;

public class FireBallProjectileScript : MonoBehaviour {

	public float projectileSpeed;
	public GameObject EnergyBubble;

	private Rigidbody2D rb2D;
	private Vector2 direction;
	private float currentTime;

	void Awake () {

		if (transform.rotation.eulerAngles.z == 0)
			direction = new Vector2 (1, 0);
		else if (transform.rotation.eulerAngles.z == 90)
			direction = new Vector2 (0, 1);
		else if (transform.rotation.eulerAngles.z == 180)
			direction = new Vector2 (-1, 0);
		else if (transform.rotation.eulerAngles.z == 270)
			direction = new Vector2 (0, -1);
		
		rb2D = GetComponent<Rigidbody2D>();
		currentTime = Time.time;
		Destroy (gameObject, 10);
	}
	

	void FixedUpdate () {
		rb2D.MovePosition(rb2D.position + direction * projectileSpeed * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D other){
		

		if ((other.CompareTag ("Player1") && this.CompareTag("Fireball2")) || (other.CompareTag("Player2") && this.CompareTag("Fireball1"))) {
			rb2D.velocity = Vector3.zero;

			if (other.CompareTag ("Player1")) {
				GameObject player1 = GameObject.Find ("Player1");
				FireBallScript script = player1.GetComponent<FireBallScript> ();
				if (script.firingBall) {
					script.reset_scale ();
				}
			}

			if (other.CompareTag ("Player2")) {
				GameObject player2 = GameObject.Find ("Player2");
				FireBallScript1 script = player2.GetComponent<FireBallScript1> ();
				if (script.firingBall) {
					script.reset_scale ();
				}
			}

			GameObject eball = Instantiate (EnergyBubble, transform.position, transform.rotation) as GameObject;
			eball.transform.localScale = new Vector3 (0.08f*transform.localScale.x,0.08f*transform.localScale.y);
			Rigidbody2D rig = other.gameObject.GetComponent<Rigidbody2D> ();

			Vector2 force = new Vector2 (-this.transform.position.x + other.transform.position.x, -this.transform.position.y + other.transform.position.y);
			rig.AddForce (force * 5000f * transform.localScale.x);

			Destroy (gameObject, 0);
		}

		if (other.CompareTag ("Obstacle")) {
			rb2D.velocity = Vector3.zero;
			GameObject eball = Instantiate (EnergyBubble, transform.position, transform.rotation) as GameObject;
			eball.transform.localScale = new Vector3 (0.08f*transform.localScale.x,0.08f*transform.localScale.y);
			Destroy (gameObject, 0);
		}
	}
}
