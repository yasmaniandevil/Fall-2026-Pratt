using UnityEngine;

// NPC
// Put this on any character you want the player to talk to.
// The object needs a Collider so the player can find it.
// Drag a Dialogue Data asset into "Dialogue".

public class NPCInteractable : Interactable
{
    public DialogueData dialogue;

    public override void Interact(PlayerInteract player)
    {
        if (dialogue == null)
        {
            Debug.LogWarning(gameObject.name + " has no Dialogue Data. Drag one in.");
            return;
        }

        //if we are interacting with the npc and it has dialogue, request it
        player.RequestDialogue(dialogue);
    }
}
