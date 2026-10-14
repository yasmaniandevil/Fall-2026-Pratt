using UnityEngine;

// DESTROY
// Put this on any object that should disappear when the player interacts with it.
// The object needs a Collider so the player can find it.

public class DestroyInteractable : Interactable
{
    public override void Interact(PlayerInteract player)
    {
        Debug.Log("Destroyed: " + gameObject.name);
        Destroy(gameObject);
    }
}
