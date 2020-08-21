using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverPizzaBall : MonoBehaviour
{

	public AudioSource audio1;
	public AudioSource audio2;
	public AudioSource audio3;
	public AudioSource[] throws;

	public GameObject pizzaBall;
	public float throwSpeed;
	public Rigidbody truck;
	private bool holding;
	private GameObject ball;
	private Rigidbody ballRb;
	private Queue<GameObject> pbsInScene;
	public int maxPbsInScene = 5;
	public bool certainAmount;
    // Start is called before the first frame update
    void Start()
    {
		pbsInScene = new Queue<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonDown(0))
		{
			if (transform.localEulerAngles.y > 180 && transform.localEulerAngles.y < 330 && holding)//THROW
			{
				throws[Random.Range(0, throws.Length)].Play();
				holding = false;
				ballRb.velocity = truck.velocity;
				ballRb.AddForce(transform.TransformDirection(Vector3.forward) * throwSpeed);
				ball.GetComponent<SphereCollider>().enabled = true;

				ball = null;
				ballRb = null;
				if (certainAmount) {
					GameMaster.usedPizzaBalls();
				}
			}
			else if (transform.localEulerAngles.y < 180 && transform.localEulerAngles.y > 30 && !holding) //GRABBING
			{
				var rand = Random.Range(0, 3);
				if (rand == 0)
				{
					audio1.Play();
				}
				else if (rand == 1)
				{
					audio2.Play();
				}
				else
				{
					audio3.Play();
				}
				//Debug.Log("grab");
				holding = true;
				if (pbsInScene.Count > maxPbsInScene)
				{
					Debug.Log("stinky");
					ball = pbsInScene.Dequeue();
					pbsInScene.Enqueue(ball);
				}
				else
				{
					
					ball = GameObject.Instantiate(pizzaBall, transform.position + transform.forward * 4, new Quaternion(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1)));
					pbsInScene.Enqueue(ball);
				}
				ballRb = ball.GetComponent<Rigidbody>();
				ballRb.velocity = Vector3.zero;
				ball.GetComponent<SphereCollider>().enabled = false;
			}
		}
		if (holding)
		{
			ball.transform.position = transform.position + transform.forward * 5 + transform.up * -2;
			ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
		}

	}
	
}
