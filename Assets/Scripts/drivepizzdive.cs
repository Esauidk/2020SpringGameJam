using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drivepizzdive : MonoBehaviour
{
    Rigidbody rb;
    public float speed;
    public float maxSpeed;
    public float roationChangeScale;
    private Vector3 rotation;
    private Vector3 direction;
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
        
        
    }

    private void FixedUpdate() {
        if(Input.GetKey(KeyCode.W)){
            rb.AddForce(speed * direction*Time.deltaTime);
            if(rb.velocity.magnitude > maxSpeed){
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
            changeRotation();
            transform.rotation = Quaternion.Euler(rotation);
        }
    }

    void decidingDirection(){
        if(Input.GetKey(KeyCode.A)){
            direction = -transform.right+transform.forward;
        }else if(Input.GetKey(KeyCode.D)){
            direction = transform.right + transform.forward;
        }else if(Input.GetKey(KeyCode.S)){
               direction = -transform.forward;
        }else{
            direction = transform.forward;
        }
    }

    void changeRotation(){
        float speedRotation = roationChangeScale*rb.velocity.magnitude;
        if(Input.GetKey(KeyCode.A)){
            rotation = new Vector3(rotation.x,rotation.y-roationChangeScale,rotation.z);
        }else if(Input.GetKey(KeyCode.D)){
            rotation = new Vector3(rotation.x,rotation.y+roationChangeScale,rotation.z);
        }
        
    }
}
