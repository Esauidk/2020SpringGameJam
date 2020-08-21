using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDrive : MonoBehaviour
{
	public AudioSource engine;
	public AudioSource breaks;
	Rigidbody rb;
	public float speed;
	public float reverseSpeed;
	public float maxSpeed;
	public float roationChangeScale;
	private Vector3 rotation;
	private Vector3 direction;
	public float grav;
	private float timePressingA;
	private float timePressingD;
	public float rotationAccel;
	public Transform wheel;
	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		rotation = transform.eulerAngles;
		
	}

	// Update is called once per frame
	void Update()
	{
		decidingDirection();
		if (Input.GetKeyDown(KeyCode.W))
		{
			if (!engine.isPlaying)
			{
				engine.Play();
				engine.volume = 0;
			}
			StartCoroutine(FadeAudioSource.StartFade(engine, 1, 0.4f));
		}
		if (!Input.GetKey(KeyCode.W))
		{
			StopAllCoroutines();
			StartCoroutine(FadeAudioSource.StartFade(engine, 0.3f, 0));
		}
		if (Input.GetKeyDown(KeyCode.S))
		{
			breaks.Play();
			breaks.volume = 0;
		}
		Debug.Log(Vector3.Angle(rb.velocity, transform.forward));
		if (Vector3.Angle(rb.velocity, transform.forward) > 30 && Vector3.Angle(rb.velocity, transform.forward) < 140 && rb.velocity.magnitude > 100)
		{
			breaks.volume = (Vector3.Angle(rb.velocity, transform.forward) - 30) / 50;
		}
		else
		{
			breaks.volume = 0;
		}
	}

	private void FixedUpdate()
	{
		rb.AddForce(grav * Vector3.down);
		if (Input.GetKey(KeyCode.W) && !GameMaster.dead)
		{
			rb.AddForce(speed * direction * Time.deltaTime);
			changeRotation();
		}
		else if (Input.GetKey(KeyCode.S) && !GameMaster.dead)
		{
			rb.AddForce(reverseSpeed * transform.forward * Time.deltaTime);
			changeRotation();
		}
		else
		{
			changeRotation();
		}

		if (rb.velocity.magnitude > maxSpeed)
		{
			rb.velocity = rb.velocity.normalized * maxSpeed;
		}
		
		transform.rotation = Quaternion.Euler(rotation);

		if(!GameMaster.dead){
			wheel.localEulerAngles = new Vector3(wheel.localEulerAngles.x, wheel.localEulerAngles.y, 90 - timePressingA * 90 + timePressingD * 90);
		}
		
	}

	void decidingDirection()
	{
		if (Input.GetKey(KeyCode.A))
		{
			timePressingA = Mathf.Clamp(timePressingA + Time.deltaTime * rotationAccel, 0, 1);
			direction = -transform.right * timePressingA + transform.forward;

			timePressingD = Mathf.Clamp(timePressingD - Time.deltaTime * rotationAccel, 0, 1);
		}
		else if (Input.GetKey(KeyCode.D))
		{
			timePressingD = Mathf.Clamp(timePressingD + Time.deltaTime * rotationAccel, 0, 1);
			direction = transform.right * timePressingD + transform.forward;

			timePressingA = Mathf.Clamp(timePressingA - Time.deltaTime * rotationAccel, 0, 1);
		}
		else
		{
			timePressingD = Mathf.Clamp(timePressingD - Time.deltaTime * rotationAccel, 0, 1);
			timePressingA = Mathf.Clamp(timePressingA - Time.deltaTime * rotationAccel, 0, 1);
			direction = -transform.right * timePressingA  + transform.right * timePressingD + transform.forward;
		}
	}

	void changeRotation()
	{
		float speedRotation = roationChangeScale * rb.velocity.magnitude;
		
	
			rotation = new Vector3(rotation.x, rotation.y - speedRotation * timePressingA + speedRotation * timePressingD, rotation.z);
		

	}
}
