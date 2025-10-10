using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    public Transform planetTransform;
    public float oribitSpeed;
    public float orbitRadius;

    float angle;

    void Update()
    {
        OrbitalMotion(orbitRadius, oribitSpeed, planetTransform);
    }

    public void OrbitalMotion(float radius, float speed, Transform target)
    {
        angle += speed;
        float angleCos = Mathf.Cos(angle * Mathf.Deg2Rad);
        float angleSin = Mathf.Sin(angle * Mathf.Deg2Rad);
        Vector2 moonPos = new Vector2(angleCos, angleSin) * radius;
        transform.position = (Vector2)target.position + moonPos;
    }
}
