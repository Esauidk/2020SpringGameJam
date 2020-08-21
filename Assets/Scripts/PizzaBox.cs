using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaBox : MonoBehaviour
{
	public int numPizzaBalls;
	public Vector3 pizzaBallSize;
	public float pizzaBallMass;
	private GameObject[] balls;
	public GameObject pizzaBall;
    // Start is called before the first frame update
    void Start()
    {
		GameMaster.setPizzaBall(numPizzaBalls);
		balls = new GameObject[numPizzaBalls];
		for (int i = 0; i < numPizzaBalls; i++)
		{
			GameObject ball = Instantiate(pizzaBall, transform.position, Quaternion.identity);
			ball.GetComponent<Rigidbody>().mass = pizzaBallMass;
			ball.transform.localScale = pizzaBallSize;
			balls[i] = ball;
		}
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		foreach (GameObject ball in balls)
		{
			if (Vector3.Distance(transform.position, ball.transform.position) > 10)
			{
				ball.transform.position = transform.position;
				ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
			}
		}
    }
}
