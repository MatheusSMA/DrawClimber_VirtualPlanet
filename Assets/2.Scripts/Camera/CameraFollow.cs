using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region Camera Variables
    public Transform target;
    public Vector3 offset;
    #endregion

    void Update()
    {
        //Seguir o objeto com distancia determinada
        //Follow the object with certain distance
        transform.position = target.position + offset;
    }
}
