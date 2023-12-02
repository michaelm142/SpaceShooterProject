using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float FollowDistance = 10.0f;
    public float MoveSpeed = 1.0f;
    public float RotationSpeed = 15.0f;

    private Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        targetPosition = target.position - target.forward * FollowDistance;
        RaycastHit hit;
        if (Physics.Raycast(target.position, -target.forward, out hit, FollowDistance))
            targetPosition = hit.point;

        // move towards point
        // find length vector
        Vector3 L = targetPosition - transform.position;
        // update position by length
        Debug.Log(Vector3.Distance(transform.position, target.position));
        transform.position += L.normalized * System.Math.Min(MoveSpeed * Time.deltaTime, Vector3.Distance(transform.position, targetPosition));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, RotationSpeed * Time.deltaTime);
    }
}
