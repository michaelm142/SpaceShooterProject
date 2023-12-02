using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;

    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        Vector3 L = target.position - transform.position;
        L.z = 0.0f;
        transform.Translate(L * moveSpeed * Time.deltaTime);
    }
}
