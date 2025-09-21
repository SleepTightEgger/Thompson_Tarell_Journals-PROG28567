using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public List<Transform> asteroidTransforms;

    // Declare and define default values for bomb trail spacing and number of trail bomb public variables
    public float bombTrailSpacing = 0.5f;
    public int numberOfTrailBombs = 5;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            StartCoroutine(BombDelay());
        }

        // Check if player is pressing down the T key
        if (Input.GetKeyDown(KeyCode.T))
        {
            // Call Spawn Bomb Trail function, passing in bomb spacing and number variables
            SpawnBombTrail(bombTrailSpacing, numberOfTrailBombs);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SpawnBombOnRandomCorner(1f);
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

    // Bomb trail spawn function
    public void SpawnBombTrail(float inBombSpacing, int inNumberOfBombs)
    {
        // For each iteration of the loop where the iteration number is less than the passed in number of bombs
        for (int i = 0; i < inNumberOfBombs; i++)
        {
            // Create an instance of the bomb prefab at the ships transform minus a new Vector3 that uses the spacing variable
            Instantiate(bombPrefab, transform.position - new Vector3(0, inBombSpacing, 0), Quaternion.identity);
            // increment the bomb spacing variable for the next iteration, causing the next bomb to be spaced further away
            inBombSpacing += 1;
        }
    }

    public void SpawnBombOnRandomCorner(float inDistance)
    {
        int randomCorner = Random.Range(0, 4);

        Vector2 spawnPos = transform.position;

        if (randomCorner == 0)
        {
            spawnPos += new Vector2(-1, 1).normalized * inDistance;
        } 
        else if (randomCorner == 1)
        {
            spawnPos += new Vector2(1, 1).normalized * inDistance;
        }
        else if (randomCorner == 2)
        {
            spawnPos += new Vector2(1, -1).normalized * inDistance;
        }
        else if (randomCorner == 3)
        {
            spawnPos += new Vector2(-1, -1).normalized * inDistance;
        }

        Instantiate(bombPrefab, spawnPos, Quaternion.identity);
    }
}