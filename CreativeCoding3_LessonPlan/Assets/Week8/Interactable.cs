using UnityEngine;

// INTERACTABLE
// The base that every interactable script builds on (Pickup, NPC, Destroy).
// You don't put this on anything directly.
//
// Want to make your own? Make a new script, change ": MonoBehaviour"
// to ": Interactable", and write your own Interact method:
//
//   public override void Interact(PlayerInteract player)
//   {
//       //what happens when the player interacts with this
//   }

public abstract class Interactable : MonoBehaviour
{
    public abstract void Interact(PlayerInteract player);
}
