using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planetGravity : MonoBehaviour
{
    public Transform player;

    public float accelerationTime = 1.0f;
    public float gravitySpeed = 20f;
    public float gravityRadius = 2f;

    Vector3 objectVelocity = Vector3.zero;

    List<Vector2> radiusVectors = new List<Vector2>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GravitationalPull(player, 5);
        DrawRadius(gravityRadius);
    }

    public void GravitationalPull(Transform playerTransform, float radius)
    {
        if (Vector2.Distance(playerTransform.position, transform.position) < radius)
        {
            Vector3 dir = playerTransform.position - transform.position;
            float dist = dir.magnitude;
            dir.Normalize();
            objectVelocity += dir * (gravitySpeed / accelerationTime) * Time.deltaTime;

            playerTransform.position = objectVelocity * Time.deltaTime;
        }
    }

    public void DrawRadius(float radius)
    {
        Color color = Color.white;
        if (Vector2.Distance(player.position, transform.position) < radius)
        {
            color = Color.green;
        }
        radiusVectors.Clear();
        for (int i = 0; i < 8;  i++)
        {
            float angle = 45 * i;
            float angleCos = Mathf.Cos(angle * Mathf.Deg2Rad);
            float angleSin = Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector2 angleVector = new Vector2(angleCos, angleSin) * radius;
            radiusVectors.Add(angleVector);
        }
        for (int i =0; i < radiusVectors.Count; i++)
        {
            Debug.DrawLine((Vector2)transform.position + radiusVectors[i], (Vector2)transform.position + radiusVectors[(i + 1) % radiusVectors.Count], color);
        }
    }
}
