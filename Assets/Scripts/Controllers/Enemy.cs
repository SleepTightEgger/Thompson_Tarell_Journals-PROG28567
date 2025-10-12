using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float maxSpeed = 10f;
    public float accelerationTime = 2f;
    public Transform target;
    public GameObject missilePrefab;

    Vector3 velocity = Vector3.zero;

    float t;

    public void Start()
    {
        StartCoroutine(RandomMissileTimer(Random.Range(0.2f, 2f)));
    }

    private void Update()
    {
        EnemyMovement();
        EnemyRotation();
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

    public void EnemyRotation()
    {
        Vector3 directionToTarget = (target.position - transform.position).normalized;

        float directionAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        float upAngle = Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg;

        float deltaAngle = Mathf.DeltaAngle(upAngle, directionAngle);
        float sign = Mathf.Sign(deltaAngle);

        if (Mathf.Abs(deltaAngle) < 0.1f) return;

        transform.Rotate(new Vector3(0, 0, 90 * Time.deltaTime * sign));
    }

    public void SpawnMissile(Transform targetTransform)
    {
        GameObject missile = Instantiate(missilePrefab, transform.position + transform.up, Quaternion.identity);
        missile.GetComponent<HomingMissile>().target = target;
        missile.GetComponent<HomingMissile>().velocity = transform.up * 5;
    }

    IEnumerator RandomMissileTimer(float timer)
    {
        t = timer;
        while (t > 0)
        {
            t -= Time.deltaTime;
            yield return null;
        }
        SpawnMissile(target);
        StartCoroutine(RandomMissileTimer(Random.Range(1f, 5f)));
    }
}
