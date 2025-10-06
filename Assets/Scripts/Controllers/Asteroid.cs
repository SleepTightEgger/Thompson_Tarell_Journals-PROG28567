using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float moveSpeed;
    public float arrivalDistance;
    public float maxFloatDistance;

    bool isTargetSelected;

    Vector3 dir;
    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AsteroidMovement();
    }

    public void AsteroidMovement()
    {
        if (!isTargetSelected)
        {
            dir = Random.insideUnitCircle.normalized;
            target = transform.position + (dir * maxFloatDistance);
            isTargetSelected = true;
        }
        else if (Vector3.Distance(target, transform.position) >= arrivalDistance)
        {
            Debug.DrawLine(transform.position, target);
            Vector3 targetDir = (target - transform.position).normalized;
            transform.position += moveSpeed * targetDir * Time.deltaTime;
        }
        else isTargetSelected = false;
    }
}
