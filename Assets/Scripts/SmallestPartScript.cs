using UnityEngine;
using System.Collections;

public class SmallestPartScript : MonoBehaviour {

	public GameObject Player1;
	public GameObject HeldBallP1;
	public GameObject Player2;
	public GameObject HeldBallP2;
	public GameObject TopLeft;
	public GameObject BotRight;
	public float activateCollider_Time;
	public float x_Offset;
	public float upperY_Offset;
	public float lowerY_Offset;
	public float RespawnTime;
	public GameObject SpawnPosition;

	private bool check;
	private bool fallingP1;
	private bool fallingP2;
	private float currentTime;
	private float TimeToRespawnP1;
	private float TimeToRespawnP2;
	private Vector3 collisionPositionP1;
	private Vector3 collisionPositionP2;

	void Start () {
		check = false;
		currentTime = Time.time;
	}
	

	void Update () {
		if (Time.time - currentTime >= activateCollider_Time) {
			check = true;
			TopLeft.SetActive (true);
			BotRight.SetActive (true);
		}	

		if (Player1.transform.position.x > BotRight.transform.position.x + x_Offset ||
			Player1.transform.position.x < TopLeft.transform.position.x - x_Offset ||
			Player1.transform.position.y > TopLeft.transform.position.y + upperY_Offset ||
			Player1.transform.position.y < BotRight.transform.position.y + lowerY_Offset) {

			if (check) {
				fallingP1 = true;

				collisionPositionP1 = Player1.transform.position;
				//Instantiate Falling Animation
				HeldBallP1.SetActive (false);
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
				Player2.SetActive (false);
				Player2.transform.position = SpawnPosition.transform.position;
				TimeToRespawnP2 = Time.time + RespawnTime;
			}
		}

		if (fallingP1 && Time.time >= TimeToRespawnP1) {

			Player1.SetActive (true);
			fallingP1 = false;
		}

		if (fallingP2 && Time.time >= TimeToRespawnP2) {

			Player2.SetActive (true);
			fallingP2 = false;
		}

	}
}
