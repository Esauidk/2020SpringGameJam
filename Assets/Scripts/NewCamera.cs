using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCamera : MonoBehaviour
{
	//public GameObject camera;
	public float minimumX = -60f;
	public float minimumY = -60f;
	public float maximumX = 60f;
	public float maximumY = 60f;

	public float sensitivityX = 15f;
	public float sensitivityY = 15f;

	//float rotationX;
	//float rotationY;

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	// Update is called once per frame
	void Update()
	{

		float xChange = -Input.GetAxis("Mouse Y") * sensitivityY;
		//Debug.Log(transform.localEulerAngles);
		if (transform.localEulerAngles.x + xChange > maximumX && transform.localEulerAngles.x + xChange < 180)
		{
			xChange = 0;
			//Debug.Log("1");
		}
		else if (transform.localEulerAngles.x + xChange < 360 + minimumX && transform.localEulerAngles.x + xChange > 180)
		{
			xChange = 0;
			//Debug.Log("2");
		}
		float yChange = Input.GetAxis("Mouse X") * sensitivityX;
		if (transform.localEulerAngles.y + yChange > maximumY && transform.localEulerAngles.y + yChange < 180)
		{
			yChange = 0;
			//Debug.Log("1");
		}
		else if (transform.localEulerAngles.y + yChange < 360 + minimumY && transform.localEulerAngles.y + yChange > 180)
		{
			yChange = 0;
			//Debug.Log("2");
		}
		transform.localEulerAngles = new Vector3(transform.localEulerAngles.x + xChange,  
												 transform.localEulerAngles.y + yChange, 
												 transform.localEulerAngles.z);
		/*transform.localEulerAngles = new Vector3(Mathf.Clamp(transform.localEulerAngles.x + xChange, minimumX, maximumX),
												Mathf.Clamp(transform.localEulerAngles.y + yChange, minimumY, maximumY),
												transform.localEulerAngles.z);*/

		//rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);
		//rotationY = Mathf.Clamp(rotationY,minimumY,maximumY);
		//camera.transform.eulerAngles = new Vector3(-rotationX, rotationY, 0);
	}
}
