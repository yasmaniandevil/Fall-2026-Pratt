using UnityEngine;
using UnityEngine.InputSystem;

public class Spawning : MonoBehaviour
{
    //we are going to give it the gameobject in the inspector
    //circleprefab is the name of our empty bucket that can only hold gameObjects
    public GameObject circlePrefab;
    public GameObject trianglePrefab;
    public GameObject hexPrefab;
    public Vector2 spawnPoint; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //easiest most simple way with only one parameter
        //Instantiate(circlePrefab);
        //first we give Instantiate the object we want it to spawn
        //then we give it the position
        //transform.position just means spawn where the gameobject that this script is attched to
        //quaternion.identity = no rotation
        //we put this in start so it spawns once, if we had this line it update it would be continuously spawning
        //and it would eventually crash unity (30 spawns a second)
        Instantiate(circlePrefab, transform.position, Quaternion.identity);
        //this is us calling our function
        //SpawnSimple(); 
        SpawnObjectComplex(trianglePrefab, new Vector2(3, 3), new Vector3(2, 2, 2), Color.purple);
    }

    //we created a function
    //a function performs a specific task, it is a way to organize our code in reusable bits
    void SpawnSimple()
    {
        Instantiate(trianglePrefab, spawnPoint, Quaternion.identity);
    }

    void SpawnObjectComplex(GameObject prefab, Vector2 pos, Vector2 scale, Color color)
    {
        //i am instantiating but i am using the parameters from above
        //when it spawns, it will create a variable type GameObject called spawned object
        //what if we want to do something with what we spawn? how do we access it?
        //we create another variable called spawnedObj and every thing we spawn will be that variable
        GameObject spawnedObj = Instantiate(prefab, pos, Quaternion.identity);
        //now that i can access what i have instantiated i want to change the scale of it
        //i set it to the empty scale we have in the ()
        spawnedObj.transform.localScale = scale;
        //now when we spawn it, we are grabbing the sprite renderer and the color of it
        //and just applying it to our empty color in the ()
        spawnedObj.GetComponent<SpriteRenderer>().color = color;
    }

    //this is a different way of doing input in unity
    //when we press Q we will spawn a circle
    public void OnSpawnCircle(InputAction.CallbackContext context)
    {
        //if we pressed Q (context.performed) we performed the pressing of Q
        //Instantiate the circle
        if (context.performed) Instantiate(circlePrefab, spawnPoint, Quaternion.identity);
    }

    //if we press W (context has been performed)
    //then call our SpawnSimple Function we made
    public void OnSpawnTriangle(InputAction.CallbackContext context)
    {
        if (context.performed) SpawnSimple();
    }

    //has to be public
    //if we press E spawn a hex
    public void OnSpawnHex(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            SpawnObjectComplex(hexPrefab, spawnPoint, new Vector3(2, 2, 2), Color.green);
        }
    }

    //when we press R
    public void OnSpawnRandomHex(InputAction.CallbackContext context)
    {
        //we are creating a new vector 2 which is (x, y)
        //but we are saying make a vector 2 but choose a number randomly between -8 to 8 on the x
        //on the y randomly choose any number between -4.5 and 4.5
        //x, y = (-8 to 8, -4.5 to 4.5) so what we can finally get is
        //(-5, -2) which will get stored in thge variable randomPos
        Vector2 randomPos = new Vector2(Random.Range(-8, 8), Random.Range(-4.5f, 4.5f));
        //scale can not be negative
        Vector2 randomScale = new Vector2(Random.Range(1, 5), Random.Range(1, 5));
        //now will choose any random color and store it in our randomColor var
        Color randomColor = Random.ColorHSV();
        //if we press R
        if (context.performed)
        {
            //then call spawn complex where we pass in the LOCAL variables we made in this function
            SpawnObjectComplex(hexPrefab, randomPos, randomScale, randomColor);
        }
    }
}
