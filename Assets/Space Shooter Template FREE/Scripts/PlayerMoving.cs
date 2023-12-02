using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script defines the borders of ‘Player’s’ movement. Depending on the chosen handling type, it moves the ‘Player’ together with the pointer.
/// </summary>

[System.Serializable]
public class Borders
{
    [Tooltip("offset from viewport borders for player's movement")]
    public float minXOffset = 1.5f, maxXOffset = 1.5f, minYOffset = 1.5f, maxYOffset = 1.5f;
    [HideInInspector] public float minX, maxX, minY, maxY;
}

public class PlayerMoving : MonoBehaviour {

    public float TurnSpeed = 10.0f;

    private Camera mainCamera;
    private bool controlIsActive = true;

    private Vector3 positionPrev;
    private Vector3 mousePosition;

    BoxCollider2D boundry;

    public static PlayerMoving instance; //unique instance of the script for easy access to the script

    private void Awake()
    {
        if (instance == null)
            instance = this;

        positionPrev = transform.position;
    }

    private void Start()
    {
        mainCamera = Camera.main;

        boundry = FindObjectOfType<Boundary>().GetComponent<BoxCollider2D>();
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(mousePosition, positionPrev);
    }

    private void Update()
    {
        if (controlIsActive)
        {
#if UNITY_STANDALONE || UNITY_EDITOR    //if the current platform is not mobile, setting mouse handling 

            if (Input.GetMouseButton(0)) //if mouse button was pressed       
            {
                mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition); //calculating mouse position in the worldspace
                mousePosition.z = transform.position.z;
                transform.position = Vector3.MoveTowards(transform.position, mousePosition, 30 * Time.deltaTime);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Vector3.forward, transform.position - positionPrev), TurnSpeed);

                if (Vector3.Distance(positionPrev, mousePosition) > 0.5f)
                    positionPrev = transform.position;
            }
#endif

#if UNITY_IOS || UNITY_ANDROID //if current platform is mobile, 

            if (Input.touchCount == 1) // if there is a touch
            {
                Touch touch = Input.touches[0];
                Vector3 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);  //calculating touch position in the world space
                touchPosition.z = transform.position.z;
                transform.position = Vector3.MoveTowards(transform.position, touchPosition, 30 * Time.deltaTime);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Vector3.forward, transform.position - positionPrev), TurnSpeed);

                if (Vector3.Distance(positionPrev, mousePosition) > 0.5f)
                    positionPrev = transform.position;
            }
            else if (Input.touchCount == 2)
            {
                Touch touch1 = Input.touches[0];
                Touch touch2 = Input.touches[1];
                Vector3 touchPosition = mainCamera.ScreenToWorldPoint((touch1.position + touch2.position) * 0.5f);  //calculating touch position in the world space
                Vector3 touchLength = (touch2.position - touch1.position).normalized;
                touchPosition.z = transform.position.z;
                transform.position = Vector3.MoveTowards(transform.position, touchPosition, 30 * Time.deltaTime);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Vector3.forward, touchLength), TurnSpeed);
            }
#endif
            transform.position = new Vector3    //if 'Player' crossed the movement borders, returning him back 
                (
                Mathf.Clamp(transform.position.x, boundry.bounds.min.x, boundry.bounds.max.x),
                Mathf.Clamp(transform.position.y, boundry.bounds.min.y, boundry.bounds.max.y),
                0
                );
        }
    }
}
