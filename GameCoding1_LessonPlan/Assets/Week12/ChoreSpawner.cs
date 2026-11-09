using System.Collections;
using UnityEngine;

// CHORE SPAWNER
// Every few seconds, a new chore shows up somewhere in the level.
// This is a coroutine that keeps restarting itself, like a kitchen timer
// that you set again every time it goes off.
//
// SETUP:
// 1. Make an empty GameObject called "ChoreSpawner" and put this script on it.
// 2. Make a few empty GameObjects where chores can show up. Drag them into Spawn Points.
// 3. Drag your Chore prefab into Chore Prefab.

public class ChoreSpawner : MonoBehaviour
{
    // The prefab we make copies of (it should have the Chore script on it)
    public GameObject chorePrefab;

    // The spots where a chore is allowed to show up
    public Transform[] spawnPoints;

    // The wait between chores is random, somewhere between these two numbers (in seconds)
    public float minWait = 3f;
    public float maxWait = 6f;

    // Don't let more than this many chores pile up at once
    public int maxChores = 3;

    // Unity calls Start automatically, once, when the scene begins
    void Start()
    {
        // Start the timer. After this line, the coroutine runs on its own
        StartCoroutine(SpawnChores());
    }

    // A coroutine is a method that can pause and come back later.
    // We call this one ourselves (in Start), Unity does not call it for us.
    IEnumerator SpawnChores()
    {
        // while (true) means "keep looping forever"
        // This is ONLY safe because there is a yield inside the loop.
        // Without the yield, the loop never pauses and Unity freezes.
        while (true)
        {
            // Pick a random wait time, then pause here until it's over
            float waitTime = Random.Range(minWait, maxWait);
            yield return new WaitForSeconds(waitTime);

            // Every chore we spawn becomes a child of this spawner,
            // so childCount tells us how many chores are waiting to be done
            if (transform.childCount < maxChores)
            {
                // Pick a random spot from the array
                int randomIndex = Random.Range(0, spawnPoints.Length);
                Transform spot = spawnPoints[randomIndex];

                // Make a copy of the chore at that spot.
                // The last argument (transform) makes it a child of this spawner.
                Instantiate(chorePrefab, spot.position, Quaternion.identity, transform);
            }

            // Then we go back to the top of the loop and wait again
        }
    }
}
