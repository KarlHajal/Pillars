using UnityEngine;
using System.Collections;

public class MapShrinkScript : MonoBehaviour {

	public float activateCollider_Time;
	public float earthquakeDelay;
	public float earthquakeLength;
	public bool removePillars;
	public GameObject[] pillarObjects;
	public GameObject Player1;
	public GameObject HeldBallP1;
	public GameObject Player2;
	public GameObject HeldBallP2;
	public GameObject TopLeft;
	public GameObject BotRight;
	public float x_Offset;
	public float upperY_Offset;
	public float lowerY_Offset;
	public float RespawnTime;
	public GameObject SpawnPosition;

	private float currentTime;
	private float TimeToRespawnP1;
	private float TimeToRespawnP2;
	private Animator anim;
	private bool fallingP1;
	private bool fallingP2;
	private bool check;
	private Vector3 collisionPositionP1;
	private Vector3 collisionPositionP2;
	private PlayerMovement player1Movementscript;
	private PlayerMovement player2Movementscript;
	private PlayerHealth1 player1Healthscript;
	private PlayerHealth2 player2Healthscript;

	void Awake () {
		currentTime = Time.time;
		anim = GetComponent<Animator> ();
		anim.SetFloat ("shrink", 0);
		TopLeft.SetActive (false);
		BotRight.SetActive (false);
		check = false;
		fallingP1 = false;
		fallingP2 = false;
		player1Movementscript = Player1.GetComponent<PlayerMovement> ();
		player2Movementscript = Player2.GetComponent<PlayerMovement> ();
		player1Healthscript = Player1.GetComponent<PlayerHealth1> ();
		player2Healthscript = Player2.GetComponent<PlayerHealth2> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - currentTime >= activateCollider_Time) {
			check = true;
			TopLeft.SetActive (true);
			BotRight.SetActive (true);
		}	

		if (Time.time - currentTime >= earthquakeDelay + earthquakeLength) {
			anim.SetFloat ("shrink", 1);
			check = false;
			TopLeft.SetActive (false);
			BotRight.SetActive (false);
			if (removePillars) {
				for (int i = 0; i < pillarObjects.Length; i++)
					pillarObjects [i].SetActive (false);
			}

		}
		
		if (Time.time - currentTime >= earthquakeDelay + earthquakeLength + 5)
			gameObject.SetActive (false);

		if (Player1.transform.position.x > BotRight.transform.position.x + x_Offset ||
		   Player1.transform.position.x < TopLeft.transform.position.x - x_Offset ||
		   Player1.transform.position.y > TopLeft.transform.position.y + upperY_Offset ||
		   Player1.transform.position.y < BotRight.transform.position.y + lowerY_Offset) {

			if (check) {
				fallingP1 = true;

				collisionPositionP1 = Player1.transform.position;
				//Instantiate Falling Animation
				HeldBallP1.SetActive (false);
				player1Movementscript.reset ();
				player1Healthscript.playerHP -= 50;
				player1Healthscript.CheckHearts ();
				Player1.SetActive (false);
				Player1.transform.position = SpawnPosition.transform.position;
				TimeToRespawnP1 = Time.time + RespawnTime;
			}
		}
			
		if (Player2.transform.position.x > BotRight.transform.position.x + x_Offset ||
		    Player2.transform.position.x < TopLeft.transform.position.x - x_Offset ||
		    Player2.transform.position.y > TopLeft.transform.position.y + upperY_Offset ||
		    Player2.transform.position.y < BotRight.transform.position.y + lowerY_Offset) {

			if (check) {

				fallingP2 = true;

				collisionPositionP2 = Player2.transform.position;
				//Instantiate Falling Animation
				HeldBallP2.SetActive (false);
				player2Movementscript.reset ();
				player2Healthscript.playerHP -= 50;
				player2Healthscript.CheckHearts ();
				Player2.SetActive (false);
				Player2.transform.position = SpawnPosition.transform.position;
				TimeToRespawnP2 = Time.time + RespawnTime;
			}
		}

		if (fallingP1 && Time.time >= TimeToRespawnP1) {
			
			Player1.SetActive (true);
			player1Movementscript.flashingTimelimit = Time.time + player1Movementscript.flashingTime;
			player1Movementscript.Flash ();
			fallingP1 = false;
		}

		if (fallingP2 && Time.time >= TimeToRespawnP2) {
			
			Player2.SetActive (true);
			player2Movementscript.flashingTimelimit = Time.time + player2Movementscript.flashingTime;
			player2Movementscript.Flash ();
			fallingP2 = false;
		}

	}
		
}
