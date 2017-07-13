using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth2 : MonoBehaviour {

	public GameObject PlayerObject;
	public float playerHP;
	public GameObject p1WIN;

	public GameObject Heart1;
	public GameObject Heart2;
	public GameObject Heart3;

	private float currentTime;
	private bool resetGame;

	void Awake () {
		resetGame = false;
		playerHP = 150;
	}


	void Update () {

		if (playerHP < 150)
			Heart3.SetActive (false);

		if (playerHP < 100)
			Heart2.SetActive (false);

		if (playerHP < 50)
			Heart1.SetActive (false);

		if (playerHP <= 0 && !resetGame) {
			p1WIN.SetActive (true);
			resetGame = true;
			currentTime = Time.time;
		}
		if(resetGame && Time.time>=currentTime+3)
			SceneManager.LoadScene("Menu");

	}

	public void CheckHearts(){
		if (playerHP < 150)
			Heart3.SetActive (false);

		if (playerHP < 100)
			Heart2.SetActive (false);

		if (playerHP < 50)
			Heart1.SetActive (false);
	}
}
