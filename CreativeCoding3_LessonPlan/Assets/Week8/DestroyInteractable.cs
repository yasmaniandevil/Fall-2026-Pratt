using UnityEngine;

public class DestroyInteractable : Interactable
{
    public override void Interact(AddToPlayerInteract player)
    {
        Destroy(gameObject);
        Debug.Log("Destroyed: " + gameObject.name);
    }
}
