using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/// <summary>
/// Tap on a detected surface to spawn an object there.
/// Touch an object you already spawned and drag your finger to slide it along the surface.
///
/// Put this on the XR Origin, next to the ARRaycastManager.
/// Works with a finger on a phone and with the mouse in the Editor (XR Simulation).
/// </summary>
[RequireComponent(typeof(ARRaycastManager))]
public class ARTapToSpawn : MonoBehaviour
{
    [Tooltip("The object to place in the world. It needs a Collider so you can grab it again.")]
    public GameObject objectToSpawn;

    [Tooltip("Turn the spawned object so its front faces the camera.")]
    public bool faceCamera = true;

    [Tooltip("How many objects can exist at once. 0 = no limit.")]
    public int maxObjects = 0;

    [Tooltip("Show a line of text at the top of the screen saying what to do next.")]
    public bool showHint = true;

    [Tooltip("Print to the Console when surfaces are found and what each tap hit. Handy for debugging.")]
    public bool logToConsole = true;

    int lastPlaneCount = -1;

    ARRaycastManager raycastManager;
    ARPlaneManager planeManager;
    Camera arCamera;
    GUIStyle hintStyle;

    // AR raycasts write their results into this list (reused so we don't make garbage every frame).
    static readonly List<ARRaycastHit> hits = new List<ARRaycastHit>();

    readonly List<GameObject> spawnedObjects = new List<GameObject>();
    GameObject draggedObject;   // the object under your finger right now (null if none)

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();
        arCamera = Camera.main;
    }

    // Nothing can spawn until AR has found a surface, so say what's going on.
    void OnGUI()
    {
        if (!showHint)
            return;

        string hint;
        if (planeManager != null && planeManager.trackables.count == 0)
            hint = Application.isEditor
                ? "Looking for surfaces... hold SPACE + move finger to look, SPACE + WASD to walk"
                : "Looking for surfaces... slowly move your phone around";
        else if (spawnedObjects.Count == 0)
            hint = "Tap a surface to place a cube";
        else
            hint = "Drag a cube to move it, or tap a surface to add another";

        if (hintStyle == null)
        {
            hintStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, wordWrap = true };
            hintStyle.normal.textColor = Color.white;
        }
        hintStyle.fontSize = Mathf.RoundToInt(Screen.height * 0.025f);

        Rect box = new Rect(0, 0, Screen.width, Screen.height * 0.08f);
        GUI.Box(box, GUIContent.none);
        GUI.Label(box, hint, hintStyle);
    }

    void Update()
    {
        if (logToConsole && planeManager != null && planeManager.trackables.count != lastPlaneCount)
        {
            lastPlaneCount = planeManager.trackables.count;
            Debug.Log("ARTapToSpawn: surfaces found = " + lastPlaneCount, this);
        }

        // Pointer = whatever touched the screen last: a finger on a phone, the mouse in the Editor.
        Pointer pointer = Pointer.current;
        if (pointer == null)
            return;

        Vector2 screenPos = pointer.position.ReadValue();

        // 1) Finger just went down: grab an existing object, or spawn a new one.
        if (pointer.press.wasPressedThisFrame)
        {
            draggedObject = FindSpawnedObjectAt(screenPos);

            if (draggedObject != null)
                Log("tap grabbed " + draggedObject.name);
            else if (TryGetSurfacePose(screenPos, out Pose pose))
            {
                draggedObject = Spawn(pose);
                Log("tap hit a surface, spawned at " + pose.position);
            }
            else
                Log("tap at " + screenPos + " didn't hit a surface (" + lastPlaneCount + " surfaces found so far)");
        }

        // 2) Finger is held down on an object: move it to wherever the finger points on a surface.
        else if (pointer.press.isPressed && draggedObject != null)
        {
            if (TryGetSurfacePose(screenPos, out Pose pose))
                draggedObject.transform.position = pose.position;
        }

        // 3) Finger lifted: let go.
        if (pointer.press.wasReleasedThisFrame)
            draggedObject = null;
    }

    void Log(string message)
    {
        if (logToConsole)
            Debug.Log("ARTapToSpawn: " + message, this);
    }

    /// Shoots a ray from the camera through the screen point and returns
    /// the spawned object it hits (or null). Uses normal Unity physics.
    GameObject FindSpawnedObjectAt(Vector2 screenPos)
    {
        Ray ray = arCamera.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            foreach (GameObject obj in spawnedObjects)
            {
                if (obj != null && hit.transform.IsChildOf(obj.transform))
                    return obj;
            }
        }
        return null;
    }

    /// Asks AR Foundation where the screen point lands on a detected plane.
    bool TryGetSurfacePose(Vector2 screenPos, out Pose pose)
    {
        if (raycastManager.Raycast(screenPos, hits, TrackableType.PlaneWithinPolygon))
        {
            pose = hits[0].pose;   // hits are sorted, closest first
            return true;
        }
        pose = default;
        return false;
    }

    GameObject Spawn(Pose pose)
    {
        if (objectToSpawn == null)
        {
            Debug.LogWarning("ARTapToSpawn: assign an Object To Spawn in the Inspector.", this);
            return null;
        }

        // Remove the oldest object if we hit the limit.
        if (maxObjects > 0 && spawnedObjects.Count >= maxObjects)
        {
            Destroy(spawnedObjects[0]);
            spawnedObjects.RemoveAt(0);
        }

        Quaternion rotation = pose.rotation;
        if (faceCamera)
        {
            // Look toward the camera but stay upright (ignore the camera's height).
            Vector3 toCamera = arCamera.transform.position - pose.position;
            toCamera.y = 0f;
            if (toCamera.sqrMagnitude > 0.0001f)
                rotation = Quaternion.LookRotation(toCamera);
        }

        GameObject spawned = Instantiate(objectToSpawn, pose.position, rotation);
        spawnedObjects.Add(spawned);
        return spawned;
    }
}
