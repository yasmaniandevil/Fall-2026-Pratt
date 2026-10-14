using TMPro;
using UnityEngine;

// DIALOGUE MANAGER
// Put this on your Canvas. NOT on the dialogue panel itself:
// the panel gets turned off, and turned off objects can't listen for events.
//
// Drag in:
// Dialogue Panel: the box that holds your text
// Display Name:   the TextMeshPro text for who is talking
// Dialogue Text:  the TextMeshPro text for what they say
//
// Tip: add a small text inside your panel like "Press E to continue"
// so players know what to do.
//
// You don't need this script if your game has no dialogue.

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI displayName;
    public TextMeshProUGUI dialogueText;

    //other scripts can check this to see if dialogue is showing
    public static bool IsDialogueOpen { get; private set; }

    private DialogueData currentDialogue;
    //which line we are on, starts at 0 just like an array
    private int lineIndex = 0;

    void Awake()
    {
        //start the game with dialogue closed
        IsDialogueOpen = false;
        if (dialoguePanel != null) dialoguePanel.SetActive(false);
    }

    private void OnEnable()
    {
        PlayerInteract.OnDialogueRequested += StartDialogue;
        PlayerInteract.OnDialogueNextLine += NextLine;
        PlayerInteract.OnDialogueClose += EndDialogue;
    }

    private void OnDisable()
    {
        PlayerInteract.OnDialogueRequested -= StartDialogue;
        PlayerInteract.OnDialogueNextLine -= NextLine;
        PlayerInteract.OnDialogueClose -= EndDialogue;
        IsDialogueOpen = false;
    }

    void StartDialogue(DialogueData dialogueData)
    {
        if (dialogueData == null)
        {
            Debug.LogWarning("Tried to start dialogue, but the Dialogue Data is empty.");
            return;
        }

        if (dialogueData.lines == null || dialogueData.lines.Length == 0)
        {
            Debug.LogWarning(dialogueData.name + " has no lines. Add some in the Inspector.");
            return;
        }

        currentDialogue = dialogueData;
        lineIndex = 0;
        IsDialogueOpen = true;

        if (dialoguePanel != null) dialoguePanel.SetActive(true);
        if (displayName != null) displayName.text = currentDialogue.displayName;
        ShowLine();
    }

    //move to the next line, or close if we ran out
    void NextLine()
    {
        if (!IsDialogueOpen) return;

        lineIndex++;

        //if we went past the last line, we're done
        if (lineIndex >= currentDialogue.lines.Length)
        {
            EndDialogue();
        }
        else
        {
            ShowLine();
        }
    }

    void ShowLine()
    {
        if (dialogueText != null) dialogueText.text = currentDialogue.lines[lineIndex];
    }

    void EndDialogue()
    {
        IsDialogueOpen = false;
        currentDialogue = null;
        if (dialoguePanel != null) dialoguePanel.SetActive(false);
    }
}
