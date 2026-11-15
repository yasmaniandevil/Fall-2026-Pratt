using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Menu: Tools > AR Spawn > Use Trackpad-Friendly Simulation Controls
///
/// XR Simulation (testing AR in the Editor) only lets you move the camera while the RIGHT mouse
/// button is held, which is awkward on a Mac trackpad. This switches it to
/// Assets/ARSpawn/Input/TrackpadSimulationControls.inputactions, where holding SPACE works too:
///   hold Space + move finger on trackpad = look around
///   hold Space + W A S D                 = walk,  Q / E = down / up,  Shift = faster
///
/// Same as setting the four actions by hand in
/// Project Settings > XR Plug-in Management > XR Simulation.
/// </summary>
public static class ARSpawnSimulationControls
{
    const string ActionsPath = "Assets/ARSpawn/Input/TrackpadSimulationControls.inputactions";
    const string PreferencesPath = "Assets/XR/UserSimulationSettings/Resources/XRSimulationPreferences.asset";

    [MenuItem("Tools/AR Spawn/Use Trackpad-Friendly Simulation Controls")]
    public static void Apply()
    {
        var preferences = AssetDatabase.LoadAssetAtPath<ScriptableObject>(PreferencesPath);
        if (preferences == null)
        {
            Debug.LogError("AR Spawn: couldn't find " + PreferencesPath +
                ". Open Project Settings > XR Plug-in Management > XR Simulation once, then try again.");
            return;
        }

        // Unity makes one InputActionReference per action inside the .inputactions file.
        var references = AssetDatabase.LoadAllAssetsAtPath(ActionsPath).OfType<InputActionReference>().ToArray();

        var so = new SerializedObject(preferences);
        bool ok = Assign(so, "m_UnlockInputActionReference", references, "Unlock")
                & Assign(so, "m_MoveInputActionReference", references, "Move")
                & Assign(so, "m_LookInputActionReference", references, "Look")
                & Assign(so, "m_SprintInputActionReference", references, "Sprint");
        if (!ok)
            return;

        so.ApplyModifiedPropertiesWithoutUndo();
        EditorUtility.SetDirty(preferences);
        AssetDatabase.SaveAssets();
        Debug.Log("AR Spawn: XR Simulation now uses trackpad-friendly controls. " +
            "In Play mode, hold SPACE and move your finger to look, SPACE + WASD to walk.");
    }

    static bool Assign(SerializedObject so, string field, InputActionReference[] references, string actionName)
    {
        var property = so.FindProperty(field);
        var reference = references.FirstOrDefault(r => r.action != null && r.action.name == actionName);
        if (property == null || reference == null)
        {
            Debug.LogError("AR Spawn: couldn't set the '" + actionName + "' control (" +
                (property == null ? "settings field " + field + " not found" : "action missing in " + ActionsPath) + ").");
            return false;
        }
        property.objectReferenceValue = reference;
        return true;
    }
}
