using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public Camera cam;
	public float shake;
	public float shakeValue;
	public float shakeAmount;
	public float timeUntilShake;
	public float decreaseFator;
	public AudioSource earthquakeSound;
	public float soundFadeDuration;

	private float zValue;
	private float startTime;
	private float startVolume;

	void Awake () {
		shake = 0;
		startTime = Time.time;
		startVolume = earthquakeSound.volume;
	}
	

	void Update () {
		if (Time.time - startTime >= timeUntilShake) {
			shake = shakeValue;
			earthquakeSound.volume = startVolume;
			earthquakeSound.Play ();
		}
		if (shake > 0) {
			zValue = cam.transform.localPosition.z;
			cam.transform.localPosition = Random.insideUnitSphere * shakeAmount;
			cam.transform.localPosition = new Vector3 (cam.transform.localPosition.x, cam.transform.localPosition.y, zValue);
			shake -= Time.deltaTime * decreaseFator;
			startTime = Time.time;
		} 
		else {
			shake = 0;
			earthquakeSound.volume -= startVolume * Time.deltaTime / soundFadeDuration;
		}
	}

	public static IEnumerator FadeOut (AudioSource audioSource, float FadeTime) {
        
		float startVolume = audioSource.volume;
 
        while (audioSource.volume > 0) {
            
 
            yield return null;
        }
 
        audioSource.Stop ();
        audioSource.volume = startVolume;
    }
}
