using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class AddToPlayerInteract : MonoBehaviour
{
    
    //interaction variables
    private GameObject currentTarget;
    public Image reticleImage;
    private bool interactPressed;
    //this is our event that the other scripts will be listening for
    public static event Action<NPCData> OnDialogueRequested;
    private Interactable currentInteractable;
    public Transform playerCamera;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInteract();
        HandleInteract();
    }
    
    void CheckInteract()
    {
        //reset reticle image to normal color first
        if(reticleImage != null) reticleImage.color = new Color(0, 0, 0, .7f);
        currentInteractable = null;
        //make a ray that goes straight out of the camera(center of screen)
        //players eyesight
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        //RaycastHit hit;
        //asking unity if it hit something within 3 units
        //hit stores what we hit like the collider
        //bool didHit = Physics.Raycast(ray, out hit, 3);
        //if (!didHit) return;//if we didn't hit anything start here
        //if we hit something tagged interactable
        if (Physics.Raycast(ray, out RaycastHit hit, 3f))
        {
            currentInteractable = hit.collider.GetComponentInParent<Interactable>();
            Debug.Log("Current interactable: " + currentInteractable);
            if(currentInteractable != null && reticleImage != null)
            {
                reticleImage.color = Color.red;
                Debug.DrawRay(playerCamera.position, playerCamera.forward * 3, Color.blue);
            }
            else
            {
                Debug.DrawRay(playerCamera.position, playerCamera.forward * 3, Color.blue);
            }
            
        }
        
    }
    
    void HandleInteract()
    {
        //if the player did not press interact this frame do nothing
        if (!interactPressed) return;
        //consume the input so one click only triggers one interactions
        //this changes next frame
        interactPressed = false;
        if(currentInteractable == null) return;
        //Debug.Log("current interactable: " + currentInteractable);
        //currentInteractable.Interact(this);
        currentInteractable.Interact(this);
        //Debug.Log("handle interact should be running");
    
        //it is not storing current interactable

    }
    
    public void OnInteract(InputAction.CallbackContext context)
    {
        if(context.performed) interactPressed = true;
        Debug.Log("OnInteract fired. performed=" + context.performed);
    }
    public void RequestDialogue(NPCData npcData)
    {
        OnDialogueRequested?.Invoke(npcData);
    }
}
