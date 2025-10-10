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

    public float radarRadius = 2f;
    public int radarPoints = 5;
    List<Vector2> angleVectors = new List<Vector2>();

    Vector3 velocity = Vector3.zero;

    void Update()
    {
        PlayerMovement();
        EnemyRadar(radarRadius, radarPoints);
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

    public void EnemyRadar(float radius, int circlePoints)
    {
        angleVectors.Clear();
        Color radarColor = Color.green;
        if (Vector2.Distance(enemyTransform.position, transform.position) < radius)
        {
            radarColor = Color.red;
        }
            for (int i = 0; i < circlePoints; i++)
            {
                float angle = (360 / circlePoints) * i;
                float angleCos = Mathf.Cos(angle * Mathf.Deg2Rad);
                float angleSin = Mathf.Sin(angle * Mathf.Deg2Rad);
                Vector2 angleVector = new Vector2(angleCos, angleSin) * radius;
                angleVectors.Add(angleVector);
            }
        for (int i = 0;i < angleVectors.Count; i++)
        {
            Debug.DrawLine((Vector2)transform.position + angleVectors[i], (Vector2)transform.position + angleVectors[(i + 1) % angleVectors.Count], radarColor);
        }
    }
}