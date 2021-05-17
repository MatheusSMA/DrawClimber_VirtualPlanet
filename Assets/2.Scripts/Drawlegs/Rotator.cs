using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    //Rotational Speed
    public float speed = 0f;
    public Rigidbody rb;
    
    //Forward Direction
    public bool ForwardX = false;
    public bool ForwardY = false;
    public bool ForwardZ = false;

    void FixedUpdate()
    {    
        //Forward Direction
        if (ForwardX == true)
        {
            addTorqueBox();
            //transform.Rotate(Time.deltaTime * speed, 0, 0, Space.Self);
        }
        if (ForwardY == true)
        {
            addTorqueBox();
            //transform.Rotate(0, Time.deltaTime * speed, 0, Space.Self);
        }
        if (ForwardZ == true)
        {
            addTorqueBox();
            transform.Rotate(0, 0, Time.deltaTime * speed, Space.Self);
        }            
    }
    public void addTorqueBox() 
    {
        rb.AddTorque(10, 0, 0);
    }

}