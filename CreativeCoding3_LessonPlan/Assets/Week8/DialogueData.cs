using UnityEngine;

// DIALOGUE DATA
// A "script" for a conversation. Works for NPCs, items, signs, anything.
//
// HOW TO MAKE ONE:
// Right click in the Project window > Create > Dialogue > Dialogue Data
// Fill in the name of who (or what) is talking, then add lines.
//
// Each line is one box of text. The player presses the interact key
// to see the next line. After the last line, the dialogue closes.
//
// 1 line  = a quick popup ("You found a rusty key.")
// 5 lines = a conversation

[CreateAssetMenu(menuName = "Dialogue/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    //who is talking (shows above the text)
    public string displayName;

    //every line of dialogue, in order. Click + to add more.
    [TextArea(2, 4)] public string[] lines;
}
