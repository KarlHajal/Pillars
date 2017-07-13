using UnityEngine;
using System.Collections;

public class FireBallScript : MonoBehaviour {


	public string fireButton;
	public GameObject FireBall_Object;
	public GameObject FireBallProjectile;
	public float timeBetweenFireBalls;
	public float maxFireBallSize;
	public float SizeIncrease_Rate;
	public float speedDecreaseRate;
	public AudioSource FireSound;

	private float instantiateTime;
	private bool canFireBall;
	public bool firingBall;
	private Vector3 initialScale;
	private GameObject thePlayer;
	private PlayerMovement script;
	private float originalSpeed;

	void Awake () {
		canFireBall = true;
		firingBall = false;
		FireBall_Object.SetActive (false);
		thePlayer = GameObject.Find ("Player1");
		script = thePlayer.GetComponent<PlayerMovement> ();
		originalSpeed = script.speed;
		initialScale = new Vector3 (FireBall_Object.transform.localScale.x,FireBall_Object.transform.localScale.y,FireBall_Object.transform.localScale.z);
	}
	
	public void reset_scale(){
		FireBall_Object.transform.localScale = initialScale;
		FireBall_Object.SetActive (false);
		firingBall = false;
	}
	void Update () {
		
		if (Input.GetButtonDown (fireButton) && canFireBall) {
			canFireBall = false;
			firingBall = true;
			FireBall_Object.SetActive (true);
		} 
		else if (Input.GetButton (fireButton) && firingBall) {
			if (FireBall_Object.transform.localScale.x < maxFireBallSize) {
				FireBall_Object.transform.localScale += new Vector3 (SizeIncrease_Rate, SizeIncrease_Rate, 0);
				if(script.speed >= 0)
					script.speed -= speedDecreaseRate;
			}
		}
		else if (Input.GetButtonUp (fireButton) && firingBall) {
			instantiateTime = Time.time;
			firingBall = false;
			Instantiate (FireSound);
			GameObject fball = Instantiate (FireBallProjectile, FireBall_Object.transform.position, FireBall_Object.transform.rotation) as GameObject;
			fball.transform.localScale = FireBall_Object.transform.localScale;
			FireBall_Object.transform.localScale = initialScale;
			FireBall_Object.SetActive (false);
			script.speed = originalSpeed;
		}

		else if (!Input.GetButton(fireButton) && !canFireBall && Time.time >= instantiateTime + timeBetweenFireBalls) {
			canFireBall = true;
		}

	}
		
}
