using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planetGravity : MonoBehaviour
{
    public Transform player;
    public GameObject playerObject;

    public float accelerationTime = 1.0f;
    public float gravitySpeed = 15f;
    public float gravityRadius = 3f;

    List<Vector2> radiusVectors = new List<Vector2>();


    // Update is called once per frame
    void Update()
    {
        gravityRadius = transform.localScale.x;
        gravitySpeed = transform.localScale.x * 3;
        if (player != null &&  playerObject != null)
        {
            GravitationalPull(player, gravityRadius);
        }
        DrawRadius(gravityRadius);
    }

    public void GravitationalPull(Transform playerTransform, float radius)
    {
        if (Vector2.Distance(playerTransform.position, transform.position) < radius)
        {
            Vector3 dir = transform.position - playerTransform.position;
            float dist = dir.magnitude;
            if (dist < radius)
            {
                dir.Normalize();
                player.GetComponent<Player>().velocity += dir * (gravitySpeed / accelerationTime) * Time.deltaTime;
            }
        }
    }

    public void DrawRadius(float radius)
    {
        Color color = Color.white;
        if (player != null && playerObject != null)
        {
            if (Vector2.Distance(player.position, transform.position) < radius)
            {
                color = Color.green;
            }
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
