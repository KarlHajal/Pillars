using UnityEngine;
using System.Collections;

public class CentralPillarScript : MonoBehaviour {

	public float resetAnimationTime;
	public float earthquakeLength;

	private Animator anim;
	private float currentTime;

	void Awake(){
		anim = GetComponent<Animator> ();
		currentTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Time.time - currentTime >= (resetAnimationTime + earthquakeLength)) {
			anim.SetFloat ("resetPillarAnimation", 1);
			currentTime = Time.time;

		} 
		else {
			anim.SetFloat ("resetPillarAnimation", 0);
		}
	}
}
