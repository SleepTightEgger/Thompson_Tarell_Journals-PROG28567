using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public Transform playerTransform;
    
    private void Update()
    {
        Debug.Log(NormalizeVector(new Vector2(3, 4)));
        Debug.Log(NormalizeVector(new Vector2(-3, 2)));
        Debug.Log(NormalizeVector(new Vector2(1.5f, -3.5f)));
    }

    Vector2 NormalizeVector(Vector2 vec)
    {
        Vector2 normalized;

        float magnitude = vec.magnitude;
        normalized = new Vector2(vec.x / magnitude, vec.y / magnitude);

        return normalized;
    }
}
