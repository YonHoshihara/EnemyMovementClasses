using UnityEditor;
using UnityEngine;
using System;

public class EventChannelCreatorWindow : EditorWindow
{
    private string assetName = "NewEvent";
    private int selectedTypeIndex = 0;

    private static readonly string[] eventTypeNames =
    {
        "VoidEvent",
        "IntEvent",
        "FloatEvent",
        "DoubleEvent",
        "StringEvent",
        "BoolEvent",
        "GameObjectEvent",
        "Vector3Event"
    };

    private static readonly Type[] eventTypes =
    {
        typeof(VoidEventSO),
        typeof(IntEventSO),
        typeof(FloatEventSO),
        typeof(DoubleEventSO),
        typeof(StringEventSO),
        typeof(BoolEventSO),
        typeof(GameObjectEventSO),
        typeof(Vector3EventSO),
    };

    private const string BASE_PATH = "Assets/Scripts/ScriptableObjectEventsTool/Instances";

    [MenuItem("Tools/Events/Create Event")]
    public static void OpenWindow()
    {
        GetWindow<EventChannelCreatorWindow>("Event Creator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Create Event", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        assetName = EditorGUILayout.TextField("Event Name", assetName);
        selectedTypeIndex = EditorGUILayout.Popup("Event Type", selectedTypeIndex, eventTypeNames);

        EditorGUILayout.Space();

        if (GUILayout.Button("Create Event"))
        {
            CreateEventAsset();
        }
    }

    private void CreateEventAsset()
    {
        if (string.IsNullOrWhiteSpace(assetName))
        {
            EditorUtility.DisplayDialog("Error", "Event name cannot be empty.", "OK");
            return;
        }

        // 1️⃣ garante APENAS o caminho base
        EnsureFolderExists(BASE_PATH);

        // 2️⃣ cria APENAS a pasta do tipo escolhido
        string typeFolder = $"{BASE_PATH}/{eventTypeNames[selectedTypeIndex]}";
        EnsureFolderExists(typeFolder);

        string assetPath = $"{typeFolder}/{assetName}.asset";

        if (AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath) != null)
        {
            EditorUtility.DisplayDialog("Error", "An event with this name already exists.", "OK");
            return;
        }

        ScriptableObject asset = CreateInstance(eventTypes[selectedTypeIndex]);
        AssetDatabase.CreateAsset(asset, assetPath);
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }

    private static void EnsureFolderExists(string path)
    {
        if (AssetDatabase.IsValidFolder(path))
            return;

        string parent = System.IO.Path.GetDirectoryName(path).Replace("\\", "/");
        string folderName = System.IO.Path.GetFileName(path);

        AssetDatabase.CreateFolder(parent, folderName);
    }
}
