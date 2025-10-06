using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public List<Transform> starTransforms;
    public float drawingTime;

    int currentStar = 0;
    public float t = 0;
    Vector3 lineEnd;
    void Start()
    {
        StartCoroutine(StarTimer());
    }
    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(starTransforms[currentStar].position, lineEnd);
        Debug.Log(currentStar);
    }

    IEnumerator StarTimer()
    {
        t = 0;
        while (t < drawingTime)
        {
            t += Time.deltaTime;
            lineEnd = starTransforms[currentStar].position;
            lineEnd = Vector3.Lerp(starTransforms[currentStar].position, starTransforms[currentStar + 1].position, t);
            yield return null;
        }
        currentStar = (currentStar + 1) % (starTransforms.Count - 1);
        StartCoroutine(StarTimer());
    }
}
