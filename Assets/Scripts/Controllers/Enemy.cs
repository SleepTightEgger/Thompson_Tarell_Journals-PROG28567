using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float maxSpeed = 10f;
    public float accelerationTime = 2f;
    public Transform target;

    Vector3 velocity = Vector3.zero;

    private void Update()
    {
        EnemyMovement();
    }

    public void EnemyMovement()
    {
        Vector3 dir = target.position - transform.position;
        float dist = dir.magnitude;

        if (dist > 5) {
            if (dir != Vector3.zero)
            {
                dir.Normalize();
                velocity += dir * (maxSpeed / accelerationTime) * Time.deltaTime;

                if (velocity.magnitude > maxSpeed)
                {
                    velocity = velocity.normalized * maxSpeed;
                }
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
        //Debug.Log(dist);
        transform.position += velocity * Time.deltaTime;
    }
}
