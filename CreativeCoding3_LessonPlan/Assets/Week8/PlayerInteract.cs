using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

// PLAYER INTERACT
// Put this on your Player. That's it. No Player Input component needed,
// the controls are created inside this script.
//
// Pick your Player Type in the Inspector:
// FIRST PERSON: aim the reticle at something. It turns red when you can interact.
// THIRD PERSON: walk up to something. An invisible bubble in front of your
//               character finds the closest interactable.
//               Click the Player in the Scene view to see the bubble (yellow circle).
//
// While dialogue is open, the interact key shows the next line instead.

public class PlayerInteract : MonoBehaviour
{
    public enum PlayerType { FirstPerson, ThirdPerson }

    [Header("What kind of player is this?")]
    public PlayerType playerType = PlayerType.FirstPerson;

    [Header("Controls")]
    [Tooltip("The key that interacts and moves dialogue forward")]
    public Key interactKey = Key.E;

    [Header("First person settings")]
    public Transform playerCamera;
    public Image reticleImage;
    [Tooltip("How far the ray reaches")]
    public float lookDistance = 3f;

    [Header("Third person settings")]
    [Tooltip("How big the invisible bubble is")]
    public float nearbyRadius = 1.5f;
    [Tooltip("How far in front of the player the bubble sits")]
    public float nearbyForwardOffset = 1f;
    [Tooltip("How high off the ground the bubble sits")]
    public float nearbyHeight = 1f;

    [Header("Optional")]
    [Tooltip("A UI text like 'Press E' that only shows when something is interactable")]
    public GameObject interactPrompt;
    [Tooltip("Dialogue closes if the player walks this far away from where it started")]
    public float walkAwayDistance = 4f;

    //these are our events that DialogueManager listens for
    public static event Action<DialogueData> OnDialogueRequested;
    public static event Action OnDialogueNextLine;
    public static event Action OnDialogueClose;

    private Interactable currentInteractable;
    private InputAction interactAction;
    private Vector3 dialogueStartPosition;

    //OnEnable runs when the script turns on. We build our controls here.
    void OnEnable()
    {
        //this is the same as making an "Interact" action in an Input Actions asset
        interactAction = new InputAction("Interact", InputActionType.Button);
        interactAction.AddBinding("<Keyboard>/" + GetKeyName(interactKey));

        //actions don't listen until you turn them on
        interactAction.Enable();
    }

    //OnDisable runs when the script turns off. Clean up our controls.
    void OnDisable()
    {
        if (interactAction != null)
        {
            interactAction.Disable();
            interactAction.Dispose();
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool interactPressed = interactAction.WasPressedThisFrame();

        //IF DIALOGUE IS OPEN: the key moves dialogue forward instead of interacting
        if (DialogueManager.IsDialogueOpen)
        {
            currentInteractable = null;
            ResetReticle();
            UpdatePrompt();
            CheckWalkAway();

            if (interactPressed)
            {
                OnDialogueNextLine?.Invoke();
            }
            return; //stop here, skip the normal interact stuff below
        }

        //NORMAL: find something to interact with, depending on the player type
        if (playerType == PlayerType.FirstPerson)
        {
            CheckLookAt();
        }
        else
        {
            CheckNearby();
        }

        UpdatePrompt();

        if (interactPressed && currentInteractable != null)
        {
            currentInteractable.Interact(this);
        }
    }

    //FIRST PERSON: shoot a ray out of the camera (center of screen)
    void CheckLookAt()
    {
        currentInteractable = null;

        if (playerCamera == null)
        {
            Debug.LogWarning("Player Type is First Person, but Player Camera is empty. Drag your camera in.");
            return;
        }

        ResetReticle();

        //make a ray that goes straight out of the camera, the players eyesight
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        Debug.DrawRay(playerCamera.position, playerCamera.forward * lookDistance, Color.blue);

        //hit stores what we hit, like the collider
        if (Physics.Raycast(ray, out RaycastHit hit, lookDistance))
        {
            currentInteractable = hit.collider.GetComponentInParent<Interactable>();

            if (currentInteractable != null && reticleImage != null)
            {
                reticleImage.color = Color.red;
            }
        }
    }

    //THIRD PERSON: check an invisible bubble in front of the character
    void CheckNearby()
    {
        currentInteractable = null;

        //no reticle in third person
        if (reticleImage != null) reticleImage.enabled = false;

        //grab every collider inside the bubble
        Collider[] hits = Physics.OverlapSphere(GetNearbyCenter(), nearbyRadius);

        //find the closest one that is interactable
        float closestDistance = Mathf.Infinity;
        foreach (Collider hit in hits)
        {
            Interactable interactable = hit.GetComponentInParent<Interactable>();
            if (interactable == null) continue; //not interactable, skip it

            float distance = Vector3.Distance(transform.position, hit.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                currentInteractable = interactable;
            }
        }
    }

    //if the player walks too far from where the dialogue started, close it
    void CheckWalkAway()
    {
        float distance = Vector3.Distance(transform.position, dialogueStartPosition);
        if (distance > walkAwayDistance)
        {
            OnDialogueClose?.Invoke();
        }
    }

    //interactables call this to start dialogue
    public void RequestDialogue(DialogueData dialogueData)
    {
        //if nobody is listening, there is no DialogueManager in the scene
        if (OnDialogueRequested == null)
        {
            Debug.LogWarning("Tried to start dialogue, but there is no DialogueManager in the scene. Add one to your Canvas.");
            return;
        }

        //remember where we were standing, for the walk away check
        dialogueStartPosition = transform.position;
        OnDialogueRequested.Invoke(dialogueData);
    }

    //the middle of the bubble: in front of the player and a little off the ground
    Vector3 GetNearbyCenter()
    {
        return transform.position + transform.forward * nearbyForwardOffset + Vector3.up * nearbyHeight;
    }

    //reticle back to normal (first person only)
    void ResetReticle()
    {
        if (reticleImage == null || playerType != PlayerType.FirstPerson) return;
        reticleImage.enabled = true;
        reticleImage.color = new Color(0, 0, 0, .7f);
    }

    //show "Press E" only when there is something to interact with
    void UpdatePrompt()
    {
        if (interactPrompt != null)
        {
            interactPrompt.SetActive(currentInteractable != null);
        }
    }

    //turns the dropdown choice into the name the Input System expects (E becomes "e", Digit1 becomes "1")
    string GetKeyName(Key key)
    {
        if (Keyboard.current != null) return Keyboard.current[key].name;
        return key.ToString();
    }

    //draws the bubble in the Scene view when the Player is selected, so you can size it
    void OnDrawGizmosSelected()
    {
        if (playerType != PlayerType.ThirdPerson) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(GetNearbyCenter(), nearbyRadius);
    }
}