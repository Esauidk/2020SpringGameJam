using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerPositionLock : MonoBehaviour
{
    public GameObject camera;
    public float minimumX =-60f;
    public float minimumY = 60f;
    public float maximumX = 60f;
    public float maximumY = 360f;

    public float sensitivityX = 15f;
    public float sensitivityY= 15f;

    float rotationX;
    float rotationY;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;  
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        camera.transform.position = transform.position;
        rotationY += Input.GetAxis("Mouse X") * sensitivityX;
        rotationX += Input.GetAxis("Mouse Y")*sensitivityY;

        rotationX = Mathf.Clamp(rotationX, minimumX,maximumX);
        //rotationY = Mathf.Clamp(rotationY,minimumY,maximumY);
        camera.transform.eulerAngles = new Vector3(-rotationX,rotationY,0);
    }
}
