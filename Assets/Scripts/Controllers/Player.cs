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

        transform.position += dir * Time.deltaTime;
    }

}