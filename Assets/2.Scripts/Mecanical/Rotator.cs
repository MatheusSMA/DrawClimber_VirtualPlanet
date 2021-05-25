using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    #region Speed and rigidbody Variables   
    //Rotational Speed
    public float speed = 0f;
    public Rigidbody rb;
    #endregion

    #region Direction Variables
    //public bool forwardX = false;
    //public bool forwardY = false;
    public bool forwardZ = false;
    #endregion

    void FixedUpdate()
    {
        //Pra frente na direção do Z 
        //Forward in Z Direction       
        if (forwardZ == true)
        {
            AddTorqueBox();
            transform.Rotate(0, 0, Time.deltaTime * speed, Space.Self);
        }
    }
    public void AddTorqueBox()
    {
        //Adiciona o torque para realizar o movimento
        //Adds torque to realize moviment
        rb.AddTorque(0, 0, 10);
    }

}