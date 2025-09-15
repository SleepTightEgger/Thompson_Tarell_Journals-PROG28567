using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public List<Transform> asteroidTransforms;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            StartCoroutine(BombDelay());
        }
    }

    void SpawnBombAtOffset(Vector3 inOffset)
    {
        Instantiate(bombPrefab, transform.position - inOffset, Quaternion.identity);
    }
    IEnumerator BombDelay()
    {
        float t = 3;
        while (t > 0)
        {
            t -= Time.deltaTime;
            yield return null;
        }

        SpawnBombAtOffset(new Vector3(0, 1, 0));
    }
}