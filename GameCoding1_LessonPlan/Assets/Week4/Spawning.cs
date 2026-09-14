using UnityEngine;
using UnityEngine.InputSystem;

public class Spawning : MonoBehaviour
{
    //this varabile will hold our prefab game object
    public GameObject circlePrefab;
    public GameObject trianglePrefab;
    public GameObject hexPrefab;
    

    public Vector2 spawnPoint;
    int add = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        //instantiation means spawning a game object
        //we are going to spawn it at the position of the script
        //quaternion.identity means we do not want rotation
        //Instantiate(circlePrefab, transform.position, Quaternion.identity);
    }
    

    //we are making a function where we instantiate something and then call it in update on key press
    void SpawnSimple()
    {
        //for later when they start spawning on top of each other
        Vector3 offset = spawnPoint;
        add++;
        Debug.Log(add);
        Vector3 newOffset = new Vector3(add, spawnPoint.y, 0);
        
        
        //now we are spawning but we are hard coding where we want it to spawn to
        //Instantiate(trianglePrefab, new Vector2(5, -3), Quaternion.identity);
        Instantiate(trianglePrefab, newOffset, Quaternion.identity);
    }

    void SpawnObjectComplex(GameObject gameObject, Vector2 pos, Vector2 scale, Color color)
    {
        //we first made a new variable of type game object
        //to store the game object that we instantiate 
        GameObject spawnedObj = Instantiate(gameObject, pos, Quaternion.identity);
        spawnedObj.transform.localScale = scale;
        spawnedObj.GetComponent<SpriteRenderer>().color = color;
        
    }

    public void OnSpawnCircle(InputAction.CallbackContext context)
    {
        
        if (context.performed)
        {
            Instantiate(circlePrefab, spawnPoint, Quaternion.identity);
            
        }
    }

    public void OnSpawnTriangle(InputAction.CallbackContext context)
    {
        
        if (context.performed) SpawnSimple();
    }

    public void OnSpawnHex(InputAction.CallbackContext context)
    {
        //function with params/argus
        if(context.performed) SpawnObjectComplex(hexPrefab, spawnPoint, new Vector2(3,3), Color.red);
    }

    public void OnSpawnRandomHex(InputAction.CallbackContext context)
    {
        //what if we want to randomly position our prefab?
        //what if we wanted the scale to be random?
        //vector2(x, y) the first random range is anywhere on the x between -8 and 8
        //the y random range is between -4.5 - 4.5
        Vector2 randomPos = new Vector2(Random.Range(-8, 8), Random.Range(-4.5f, 4.5f));
        //now we are making a new local variable called randomScale and assigning it a random range
        Vector2 randomScale = new Vector2(Random.Range(1, 5), Random.Range(1, 5));
        Color randomColor = Random.ColorHSV();
        if(context.performed) SpawnObjectComplex(hexPrefab, randomPos, randomScale, randomColor);
        
    }

    
    
}
