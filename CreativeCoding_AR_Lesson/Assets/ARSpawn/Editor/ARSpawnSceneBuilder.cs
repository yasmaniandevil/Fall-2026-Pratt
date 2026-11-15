using System.Linq;
using Unity.XR.CoreUtils;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.Rendering;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/// <summary>
/// Menu: Tools > AR Spawn > Create Clean AR Scene
///
/// Builds a bare-bones AR scene with none of the Mobile AR template's UI, menus, tutorial or
/// XR Interaction Toolkit rig. It only has:
///   - AR Session
///   - XR Origin (AR camera, plane detection, raycasts, and the ARTapToSpawn script)
///   - A directional light
/// It also makes a small red cube prefab to spawn and a see-through plane prefab.
/// Run it once. After that, edit the scene and prefabs like any others.
/// </summary>
public static class ARSpawnSceneBuilder
{
    const string Root = "Assets/ARSpawn";
    const string PrefabFolder = Root + "/Prefabs";
    const string CubePrefabPath = PrefabFolder + "/SpawnCube.prefab";
    const string PlanePrefabPath = PrefabFolder + "/ARPlane.prefab";
    const string PlaneMaterialPath = PrefabFolder + "/ARPlane.mat";
    const string ScenePath = "Assets/Scenes/ARSpawnScene.unity";
    const string RedMaterialPath = "Assets/Red.mat";

    [MenuItem("Tools/AR Spawn/Create Clean AR Scene")]
    public static void CreateScene()
    {
        if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            return;

        if (AssetDatabase.LoadAssetAtPath<SceneAsset>(ScenePath) != null &&
            !EditorUtility.DisplayDialog("AR Spawn",
                ScenePath + " already exists. Replace it with a fresh copy?", "Replace", "Cancel"))
            return;

        EnsureFolder(Root);
        EnsureFolder(PrefabFolder);

        GameObject cubePrefab = GetOrCreateCubePrefab();
        GameObject planePrefab = GetOrCreatePlanePrefab();

        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        // Light so lit objects (like the red cube) aren't black.
        var lightGo = new GameObject("Directional Light");
        var light = lightGo.AddComponent<Light>();
        light.type = LightType.Directional;
        light.shadows = LightShadows.Soft;
        lightGo.transform.rotation = Quaternion.Euler(50f, -30f, 0f);

        // AR Session: starts and runs AR tracking on the device.
        new GameObject("AR Session", typeof(ARSession), typeof(ARInputManager));

        // XR Origin: the AR camera rig. Plane detection, raycasts and the spawn script go here.
        var originGo = new GameObject("XR Origin",
            typeof(XROrigin), typeof(ARPlaneManager), typeof(ARRaycastManager), typeof(ARTapToSpawn));
        var offsetGo = new GameObject("Camera Offset");
        offsetGo.transform.SetParent(originGo.transform, false);
        Camera arCamera = CreateARCamera();
        arCamera.transform.SetParent(offsetGo.transform, false);

        var origin = originGo.GetComponent<XROrigin>();
        origin.CameraFloorOffsetObject = offsetGo;
        origin.Camera = arCamera;

        var planeManager = originGo.GetComponent<ARPlaneManager>();
        planeManager.planePrefab = planePrefab;
        planeManager.requestedDetectionMode = PlaneDetectionMode.Horizontal;

        var spawner = originGo.GetComponent<ARTapToSpawn>();
        spawner.objectToSpawn = cubePrefab;

        EditorSceneManager.SaveScene(scene, ScenePath);
        AddSceneToBuildFirst(ScenePath);

        Selection.activeGameObject = originGo;
        Debug.Log("AR Spawn: created " + ScenePath + ". Select XR Origin to change what gets spawned.", originGo);
    }

    // Same camera setup AR Foundation's "GameObject > XR > XR Origin (Mobile AR)" menu uses.
    static Camera CreateARCamera()
    {
        var cameraGo = new GameObject("Main Camera",
            typeof(Camera), typeof(AudioListener),
            typeof(ARCameraManager), typeof(ARCameraBackground), typeof(TrackedPoseDriver));
        cameraGo.tag = "MainCamera";

        var camera = cameraGo.GetComponent<Camera>();
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.backgroundColor = Color.black;
        camera.nearClipPlane = 0.1f;
        camera.farClipPlane = 20f;

        // The phone's position and rotation drive the camera.
        var poseDriver = cameraGo.GetComponent<TrackedPoseDriver>();
        var position = new InputAction("Position", binding: "<XRHMD>/centerEyePosition", expectedControlType: "Vector3");
        position.AddBinding("<HandheldARInputDevice>/devicePosition");
        var rotation = new InputAction("Rotation", binding: "<XRHMD>/centerEyeRotation", expectedControlType: "Quaternion");
        rotation.AddBinding("<HandheldARInputDevice>/deviceRotation");
        poseDriver.positionInput = new InputActionProperty(position);
        poseDriver.rotationInput = new InputActionProperty(rotation);

        return camera;
    }

    // A 10 cm red cube. The cube sits on top of the prefab's pivot, so it rests ON the
    // surface instead of sinking halfway into it.
    static GameObject GetOrCreateCubePrefab()
    {
        var existing = AssetDatabase.LoadAssetAtPath<GameObject>(CubePrefabPath);
        if (existing != null)
            return existing;

        const float size = 0.1f; // meters: AR uses real-world scale
        var root = new GameObject("SpawnCube");
        var model = GameObject.CreatePrimitive(PrimitiveType.Cube); // comes with a BoxCollider
        model.name = "Model";
        model.transform.SetParent(root.transform, false);
        model.transform.localScale = Vector3.one * size;
        model.transform.localPosition = new Vector3(0f, size / 2f, 0f);

        var red = AssetDatabase.LoadAssetAtPath<Material>(RedMaterialPath);
        if (red != null)
            model.GetComponent<MeshRenderer>().sharedMaterial = red;

        var prefab = PrefabUtility.SaveAsPrefabAsset(root, CubePrefabPath);
        Object.DestroyImmediate(root);
        return prefab;
    }

    // What a detected surface looks like: a faint see-through white fill.
    static GameObject GetOrCreatePlanePrefab()
    {
        var existing = AssetDatabase.LoadAssetAtPath<GameObject>(PlanePrefabPath);
        if (existing != null)
            return existing;

        var go = new GameObject("ARPlane",
            typeof(ARPlane), typeof(ARPlaneMeshVisualizer), typeof(MeshFilter), typeof(MeshRenderer));
        var renderer = go.GetComponent<MeshRenderer>();
        renderer.sharedMaterial = GetOrCreatePlaneMaterial();
        renderer.shadowCastingMode = ShadowCastingMode.Off;

        var prefab = PrefabUtility.SaveAsPrefabAsset(go, PlanePrefabPath);
        Object.DestroyImmediate(go);
        return prefab;
    }

    static Material GetOrCreatePlaneMaterial()
    {
        var existing = AssetDatabase.LoadAssetAtPath<Material>(PlaneMaterialPath);
        if (existing != null)
            return existing;

        // URP Unlit, set to Transparent / Alpha blend (what the material Inspector would do).
        var mat = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
        mat.SetColor("_BaseColor", new Color(1f, 1f, 1f, 0.2f));
        mat.SetFloat("_Surface", 1f);
        mat.SetFloat("_Blend", 0f);
        mat.SetFloat("_SrcBlend", (float)BlendMode.SrcAlpha);
        mat.SetFloat("_DstBlend", (float)BlendMode.OneMinusSrcAlpha);
        mat.SetFloat("_SrcBlendAlpha", (float)BlendMode.One);
        mat.SetFloat("_DstBlendAlpha", (float)BlendMode.OneMinusSrcAlpha);
        mat.SetFloat("_ZWrite", 0f);
        mat.SetFloat("_Cull", (float)CullMode.Off);
        mat.SetOverrideTag("RenderType", "Transparent");
        mat.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
        mat.renderQueue = (int)RenderQueue.Transparent;

        AssetDatabase.CreateAsset(mat, PlaneMaterialPath);
        return mat;
    }

    // Put the new scene first in Build Settings so it's the one that runs on the phone.
    // Other scenes that still exist stay in the list; deleted ones are dropped.
    static void AddSceneToBuildFirst(string path)
    {
        var scenes = EditorBuildSettings.scenes
            .Where(s => s.path != path && System.IO.File.Exists(s.path))
            .ToList();
        scenes.Insert(0, new EditorBuildSettingsScene(path, true));
        EditorBuildSettings.scenes = scenes.ToArray();
    }

    static void EnsureFolder(string path)
    {
        if (AssetDatabase.IsValidFolder(path))
            return;
        int slash = path.LastIndexOf('/');
        AssetDatabase.CreateFolder(path.Substring(0, slash), path.Substring(slash + 1));
    }
}
