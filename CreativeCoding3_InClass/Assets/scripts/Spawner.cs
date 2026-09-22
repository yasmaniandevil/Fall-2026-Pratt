

using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int numberOfCubes = 10;//number of obstacles that i want to spawn
    public int numberOfSpheres = 30;

    public GameObject cubePrefab; //the object that gets spawned 
    public GameObject spherePrefab;

    private float spacing = 1.5f; //how far the obstacles are from each other the "spacing" between them

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //if we do not have anything in the inspector
        //send me a message reminding me to add it
        //null means empty
        if(cubePrefab == null && spherePrefab)
        {
            Debug.Log("drag prefab into inspector");
        }
        //if we do have something in the inspector then run the code
        //the code now will only run when we have something in the inspector
        //IT IS NOT NULL ANYMORE (null meaning empty)
        if(cubePrefab != null)
        {
            //this is a for loop
            //first we make a var type integer called "i" then set to 0, this is just like creating one at the top
            //if i is less than the number of obstacles
            //i++ means add 1.
            for(int i = 0; i < numberOfCubes; i++)
            {
                //local variable which means i can only use it here and nowhere else in the script
                //we know i goesup by 1 every single loop, the we multiply by 1.5 every time and that is on the x
                Vector3 spawnPosition = new Vector3(i * spacing, 1.5f, 0);
                //this means we want to spawn obstacle object
                //second is: where we spawn it, we are saying spawn where the script is attached to 
                //quaternion.identity means no rotation
                Instantiate(cubePrefab, spawnPosition, Quaternion.identity);
                
            }
            
        }

        if(spherePrefab != null)
        {
            for(int i = 0; i < numberOfSpheres; i++)
            {
                //make a local variable for the position
                //we want it to be random
                //we are saying on the x anywhere between -23 and 23
                //on the y anywhere between .25f and 3
                //and on the z anywhere from -23 to 24
                Vector3 randomPosition = new Vector3(Random.Range(-23, 23),
                Random.Range(.25f, 3), Random.Range(-23, 23));
                Instantiate(spherePrefab,randomPosition, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
