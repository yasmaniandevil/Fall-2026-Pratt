using UnityEngine;

// DeltaTimeDemo: the Monday "why deltaTime" warmup.
// Run it at 30 FPS, then at 144 FPS, and watch how fast hunger drops in the Console.
// Then switch useDeltaTime on and run both again.
public class DeltaTimeDemo : MonoBehaviour
{
    public int frameRate = 30;        // try 30, then 144
    public bool useDeltaTime = false; // flip this on for part two

    float hunger = 1000f;

    void Start()
    {
        // The Editor ignores targetFrameRate while VSync is on, so turn VSync off first.
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = frameRate;
    }

    void Update()
    {
        if (useDeltaTime)
        {
            // Paid per hour: drops 10 every SECOND, no matter the frame rate.
            hunger -= 10f * Time.deltaTime;
        }
        else
        {
            // Paid per step: drops 1 every FRAME. More frames = faster drain.
            hunger -= 1f;
        }

        // Time.time is seconds since play started.
        Debug.Log("Time: " + Time.time.ToString("0.0") + "   Hunger: " + hunger.ToString("0"));
    }
}
