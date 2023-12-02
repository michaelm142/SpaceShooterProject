using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script defines the size of the ‘Boundary’ depending on Viewport. When objects go beyond the ‘Boundary’, they are destroyed or deactivated.
/// </summary>
public class Boundary : MonoBehaviour {

    BoxCollider2D boundareCollider;

    //receiving collider's component and changing boundary borders
    private void Start()
    {
        boundareCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        playerPosition.x = Mathf.Clamp(playerPosition.x, boundareCollider.bounds.min.x, boundareCollider.bounds.max.x);
        playerPosition.y = Mathf.Clamp(playerPosition.y, boundareCollider.bounds.min.y, boundareCollider.bounds.max.y);

        GameObject.FindGameObjectWithTag("Player").transform.position = playerPosition;
    }

    //when another object leaves collider
    private void OnTriggerExit2D(Collider2D collision) 
    {        
        if (collision.tag == "Projectile")
        {
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "Bonus") 
            Destroy(collision.gameObject); 
    }

}
