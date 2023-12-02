using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script moves the attached object along the Y-axis with the defined speed
/// </summary>
public class DirectMoving : MonoBehaviour {

    [Tooltip("Moving speed on Y axis in local space")]
    public float speed;

    public Vector3 axis = Vector3.up;

    //moving the object with the defined speed
    private void Update()
    {
        transform.Translate(axis * speed * Time.deltaTime, Space.Self); 
    }
}
