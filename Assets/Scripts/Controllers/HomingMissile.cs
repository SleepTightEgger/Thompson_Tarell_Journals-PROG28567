using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public Transform target;
    public float angluarSpeed = 180f;

    public float maxSpeed = 10f;
    public float accelerationTime = 1f;
    
    public Vector3 velocity = Vector3.zero;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MissileRotation(target);
        MissileMovement(target);
    }

    public void MissileRotation(Transform targetTransform)
    {
        Vector3 directionToTarget = (targetTransform.position - transform.position).normalized;

        float directionAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        float upAngle = Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg;

        float deltaAngle = Mathf.DeltaAngle(upAngle, directionAngle);
        float sign = Mathf.Sign(deltaAngle);

        if (Mathf.Abs(deltaAngle) < 0.1f) return;

        transform.Rotate(new Vector3(0, 0, angluarSpeed * Time.deltaTime * sign));

        Debug.DrawLine(transform.position, transform.position + transform.up, Color.green);
        Debug.DrawLine(transform.position, transform.position + directionToTarget, Color.red);
    }

    public void MissileMovement(Transform targetTransform)
    {
        Vector3 dir = (target.position - transform.position);
        float dist = dir.magnitude;

        if (dist > 0.3 && dir != Vector3.zero)
        {
            dir.Normalize();
            velocity += transform.up.normalized * maxSpeed;
            velocity += dir * (maxSpeed / accelerationTime) * Time.deltaTime;

            if (velocity.magnitude > maxSpeed)
            {
                velocity = velocity.normalized * maxSpeed;
            }
        }
        else
        {
            Destroy(gameObject);
        }
        transform.position += velocity * Time.deltaTime;
    }
}
