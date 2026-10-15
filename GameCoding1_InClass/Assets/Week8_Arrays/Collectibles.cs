using UnityEngine;

public class Collectibles : MonoBehaviour
{
    //so we are making two arrays one of gameobject prefabs and the other of where we want to spawn them
    //at index 0 is triangeprefab; and at index 0 of the array spawnposition its 3, 4

    //put this on an empty game object and make sure to give it prefabs and positions, make them equal to eachother, so 8 
    //prefabs abd 8 positions
    public GameObject[] collectibesPrefabs;
    public Vector3[] spawnPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //this is going to loop over the array
        //the length of the array
        for(int i = 0; i < collectibesPrefabs.Length; i++)
        {
            Instantiate(collectibesPrefabs[i], spawnPosition[i], Quaternion.identity);
     
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
