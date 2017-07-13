using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public string horizontalAxisName;
	public string verticalAxisName;
	public string lockDirection_Button;
	public float speed;
	public float y_layeringLimit;
	public float centralpillarZValue;

	private float move_vertical;
	private float move_horizontal;
	private Rigidbody2D rb2d;
	private Animator anim;
	private bool lockDirection = false;

	public Color color1;
	public Color color2;
	public Color color3;
	public Color redFlashColor = new Color(1f, 0f, 0f, 0.1f);
	public float redFlashSpeed = 0.1f;
	public float delay;

	private float counter;
	private bool toggleFlash;

	private bool keepFlashing;
	public float flashingTime;
	public float flashingTimelimit;

	void Awake () {
		
		rb2d = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		keepFlashing = false;
	}
	
	public void reset(){
		anim.SetFloat ("DirectionLock", 0);
		anim.SetFloat ("MoveVert", -1);
		anim.SetFloat ("MoveHori", 0);
	}
	void Update () {
		lockDirection = Input.GetButton (lockDirection_Button); //Set Direction Lock
		Move ();

		if (Time.time >= flashingTimelimit) {
			GetComponent<SpriteRenderer>().color = color1;
			keepFlashing = false;
		}
		if (keepFlashing)
			Flash ();
	
	}

	void FixedUpdate(){
		move_vertical = Input.GetAxisRaw(verticalAxisName);
		move_horizontal = Input.GetAxisRaw (horizontalAxisName);
	}

	void Move(){
		if (lockDirection)
			anim.SetFloat ("DirectionLock", 1);
		else
			anim.SetFloat ("DirectionLock", 0);

		anim.SetFloat ("MoveHori", move_horizontal);
		anim.SetFloat ("MoveVert", move_vertical);

		if (rb2d.transform.position.y > y_layeringLimit)
			transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.y + centralpillarZValue);
		else
			transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.y+20f);

		Vector2 movement = new Vector2 (move_horizontal, move_vertical);
		rb2d.MovePosition (rb2d.position + movement * speed * Time.deltaTime);



	}

	public void Flash()
	{
		keepFlashing = true;
		if(counter >= delay)
		{
			counter = 0;
			toggleFlash = !toggleFlash;

			if(toggleFlash)
			{
				GetComponent<SpriteRenderer>().color = color3;
			}
			else
			{
				GetComponent<SpriteRenderer>().color = color2;
			}
		}
		else
		{
			counter++;
		}

	}
}
