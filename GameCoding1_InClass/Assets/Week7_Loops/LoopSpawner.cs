using UnityEngine;

public class LoopSpawner : MonoBehaviour
{
    public GameObject prefab;
    public int numberToSpawn; //how many do we want to spawn
    public float spacing = 1.5f;
    public Vector2 startPos = new Vector2 (-4, 2);
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //first we make a variable called i
        //and we set it to 0
        //then we say if i is less than the number
        //i++ means add 1
        for(int i = 0; i < numberToSpawn; i++)
        {
            //first we create a new vector2
            //that takes the start position and multiplies it by the spacing
            //everytime we instantiate a  new position is calculated
            Vector2 spawnPos = new Vector2(startPos.x + i * spacing, startPos.y);
            Instantiate(prefab, spawnPos, Quaternion.identity);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
