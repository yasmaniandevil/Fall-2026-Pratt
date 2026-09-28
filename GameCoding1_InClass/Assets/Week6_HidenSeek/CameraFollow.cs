using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followPlayer;
    public float smoothSpeed;
    private Vector3 offsetPos = new Vector3(0, 0, -10);


    // Update is called once per frame
    void Update()
    {
        //if the public followPlayer field is empty(we didnt drag anything in)
        //it wont run the rest of this code
        if(followPlayer != null)
        {
            //the position we want the camera at is the players position
            //plus the offset
            Vector3 desiredPos = followPlayer.position + offsetPos;

            //a lerp interpolates smoothly between two values
            //our two values are the camera position and where we want the camera to be, over the smooth speed time
            Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);

            //then take the cameras positon and set it to smooth pos
            //so it will lerp
            transform.position = smoothPos;
        }
    }
}
