using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Transform> asteroidTransforms;
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public Transform bombsTransform;
    public GameObject powerupPrefab;
    public GameObject missilePrefab;

    public float maxSpeed = 10f;
    public float accelerationTime = 2f;

    public float radarRadius = 2f;
    public int radarPoints = 5;
    public float powerupRadius = 3f;
    public int powerupPoints = 5;
    List<Vector2> radarVectors = new List<Vector2>();
    List<Vector2> powerupVectors = new List<Vector2>();

    public Vector3 velocity = Vector3.zero;
    float sign;

    void Update()
    {
        PlayerMovement();
        EnemyRadar(radarRadius, radarPoints);
        SpawnPowerups(powerupRadius, powerupPoints);
        SpawnMissile();
    }

    public float AngleSign(Vector3 vector)
    {
        Vector3 dta = (transform.position + vector.normalized) - transform.position;
        float dA = Mathf.Atan2(dta.y, dta.x) * Mathf.Rad2Deg;
        float upAngle = Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg;
        float deltaA = Mathf.DeltaAngle(upAngle, dA);
        return Mathf.Sign(deltaA);
    }
    public void PlayerMovement()
    {
        Vector3 dir = Vector3.zero;
        sign = 0;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            sign = AngleSign(Vector3.left);
            dir = Vector3.left;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            sign = AngleSign(Vector3.right);
            dir = Vector3.right;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            sign = AngleSign(Vector3.up);
            dir = Vector3.up;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            sign = AngleSign(Vector3.down);
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

        transform.Rotate(new Vector3(0, 0, 90f * Time.deltaTime * sign));
        transform.position += velocity * Time.deltaTime;
    }

    public void EnemyRadar(float radius, int circlePoints)
    {
        Color radarColor = Color.green;
        if (Vector2.Distance(enemyTransform.position, transform.position) < radius)
        {
            radarColor = Color.red;
        }
        DetermineAngleVectors(radarVectors, radius, circlePoints);
        for (int i = 0;i < radarVectors.Count; i++)
        {
            Debug.DrawLine((Vector2)transform.position + radarVectors[i], (Vector2)transform.position + radarVectors[(i + 1) % radarVectors.Count], radarColor);
        }
    }

    public void SpawnPowerups(float radius, int numberOfPowerups)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DetermineAngleVectors(powerupVectors, radius, numberOfPowerups);
            for (int i = 0; i < numberOfPowerups; i++)
            {
                Instantiate(powerupPrefab, (Vector2)transform.position + powerupVectors[i], Quaternion.identity);
            }
        }
    }

    public void DetermineAngleVectors(List<Vector2> vectors, float radius, int numberOfPoints)
    {
        vectors.Clear();
        for(int i = 0; i<numberOfPoints; i++)
        {
            float angle = (360/ numberOfPoints) * i;
            float angleCos = Mathf.Cos(angle * Mathf.Deg2Rad);
            float angleSin = Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector2 angleVector = new Vector2(angleCos, angleSin) * radius;
            vectors.Add(angleVector);
        }
    }

    public void SpawnMissile()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject missile = Instantiate(missilePrefab, transform.position + transform.up, transform.rotation);
            missile.GetComponent<HomingMissile>().target = enemyTransform;
            missile.GetComponent<HomingMissile>().velocity = transform.up * 5;
        }
    }
}