using UnityEngine;

public class Spawner : MonoBehaviour
{
    //just do cubes first
    
    //spawning variables
    //how many obstacles are spawning
    public int numberOfCubes = 10;
    //add later just do cubes first
    public int numberOfSpheres;
    //how far are the obstacles from each other, their spacing
    private float spacing = 3f;

    
    //what prefab are we spawning
    public GameObject cubePrefab;
    public GameObject spherePrefab;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //do loop first
        if (cubePrefab == null && spherePrefab == null)
        {
            Debug.Log("drag prefab into inspector");
        }

        if (cubePrefab != null)
        {
            for (int i = 0; i < numberOfCubes; i++)
            {
                Vector3 spawnPosition = new Vector3(i * spacing, 1.5f, 0);
                Instantiate(cubePrefab, spawnPosition, Quaternion.identity);
            }
        }

        if (spherePrefab != null)
        {
            //hard coded number of obstacles
            for (int i = 0; i < numberOfSpheres; i++)
            {
                Vector3 randomPosition = new Vector3(Random.Range(-23, 23), Random.Range(.25f, 3), Random.Range(-23, 23));
                Instantiate(spherePrefab, randomPosition, Quaternion.identity);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
