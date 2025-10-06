using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Transform> asteroidTransforms;
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public Transform bombsTransform;

    public float maxSpeed = 10f;
    public float accelerationTime = 2f;

    Vector3 velocity = Vector3.zero;

    void Update()
    {
        PlayerMovement();
    }
    public void PlayerMovement()
    {
        Vector3 dir = Vector3.zero;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            dir = Vector3.left;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        { 
            dir = Vector3.right;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            dir = Vector3.up;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            dir = Vector3.down;
        }

        if (dir != Vector3.zero)
        {
            dir.Normalize();
            velocity += dir * (maxSpeed / accelerationTime) * Time.deltaTime;

            if (velocity.magnitude > maxSpeed)
            {
                velocity = velocity.normalized * maxSpeed;
            }
            
        }
        else
        {
            float decel = (maxSpeed / accelerationTime) * Time.deltaTime;
            velocity -= velocity.normalized * decel;

            if (velocity.magnitude < decel)
            {
                velocity = Vector3.zero;
            }
        }

        //Debug.Log(velocity);
        transform.position += velocity * Time.deltaTime;
    }

}