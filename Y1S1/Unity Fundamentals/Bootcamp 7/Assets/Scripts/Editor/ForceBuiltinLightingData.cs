using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.Linq;

public class CloneLightingFromNewScene
{
    private const string sourceSceneName = "New Scene"; // Must match exactly

    [MenuItem("Tools/Clone Lighting from 'New Scene'")]
    public static void CloneLightingDataToSelectedScenes()
    {
        // Find the source scene path
        string sourceScenePath = AssetDatabase.FindAssets("t:Scene")
            .Select(AssetDatabase.GUIDToAssetPath)
            .FirstOrDefault(p => System.IO.Path.GetFileNameWithoutExtension(p) == sourceSceneName);

        if (string.IsNullOrEmpty(sourceScenePath))
        {
            Debug.LogError($"Scene '{sourceSceneName}' not found in project.");
            return;
        }

        // Open source scene and extract lighting data
        var sourceScene = EditorSceneManager.OpenScene(sourceScenePath, OpenSceneMode.Single);
        var sourceLightingData = Lightmapping.lightingDataAsset;

        if (sourceLightingData == null)
        {
            Debug.LogWarning("Source scene does not contain valid baked lighting data.");
            EditorSceneManager.CloseScene(sourceScene, true);
            return;
        }

        Debug.Log($"Cloning lighting from: {sourceScenePath}");

        // Get selected target scenes
        var targetScenePaths = Selection.objects
            .Select(AssetDatabase.GetAssetPath)
            .Where(path => path.EndsWith(".unity") && path != sourceScenePath)
            .ToList();

        foreach (var path in targetScenePaths)
        {
            var scene = EditorSceneManager.OpenScene(path, OpenSceneMode.Single);
            Debug.Log($"Applying lighting to: {path}");

            Lightmapping.ClearLightingDataAsset();
            Lightmapping.bakeOnSceneLoad = Lightmapping.BakeOnSceneLoadMode.Never;
            Lightmapping.lightingDataAsset = sourceLightingData;

            EditorSceneManager.MarkSceneDirty(scene);
            EditorSceneManager.SaveScene(scene);
        }

        // Restore original scene if needed
        EditorSceneManager.CloseScene(sourceScene, true);

        Debug.Log("Lighting clone complete.");
    }
}
