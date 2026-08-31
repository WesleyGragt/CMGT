using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class ProjectValidator : Editor
{
	[MenuItem("Assets/Validate Folder", true)]
	private static bool ValidateFolderValidation()
	{
		// Ensure this is called on a folder
		return Selection.activeObject != null && AssetDatabase.IsValidFolder(AssetDatabase.GetAssetPath(Selection.activeObject));
	}

	[MenuItem("Assets/Validate Folder")]
	private static void ValidateFolder()
	{
		// Get the path of the selected folder
		string folderPath = AssetDatabase.GetAssetPath(Selection.activeObject);

		// Get all scene files in the selected folder and its subfolders
		string[] sceneFiles = Directory.GetFiles(folderPath, "*.unity", SearchOption.AllDirectories);

		// Validate each scene
		foreach (string sceneFile in sceneFiles)
		{
			string scenePath = sceneFile.Replace("\\", "/"); // Ensure path format is consistent
			Debug.Log($"Validating scene: {scenePath}");
			ValidateScene(scenePath, folderPath);
		}

		Debug.Log("Validation complete.");
	}

	private static void ValidateScene(string scenePath, string rootFolderPath)
	{
		// Save the current active scene
		Scene currentActiveScene = SceneManager.GetActiveScene();

		// Open the scene additively
		Scene scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);

		// Get all root game objects in the scene
		GameObject[] rootObjects = scene.GetRootGameObjects();

		foreach (GameObject rootObject in rootObjects)
		{
			ValidateGameObject(rootObject, rootFolderPath);
		}

		// Close the scene and revert to the previous active scene
		EditorSceneManager.CloseScene(scene, true);
		EditorSceneManager.SetActiveScene(currentActiveScene);
	}

	private static void ValidateGameObject(GameObject gameObject, string rootFolderPath)
	{
		// Get all components on the game object
		Component[] components = gameObject.GetComponentsInChildren<Component>(true);

		foreach (Component component in components)
		{
			SerializedObject so = new SerializedObject(component);
			SerializedProperty prop = so.GetIterator();
			while (prop.NextVisible(true))
			{
				if (prop.propertyType == SerializedPropertyType.ObjectReference && prop.objectReferenceValue != null)
				{
					string assetPath = AssetDatabase.GetAssetPath(prop.objectReferenceValue);
					if (
							!string.IsNullOrEmpty(assetPath) && !assetPath.StartsWith(rootFolderPath) && !
							(
								assetPath.StartsWith("Packages") || 
								assetPath.StartsWith("Library/unity")  || 
								assetPath.StartsWith("Resources/unity")  || 
								assetPath.StartsWith("Assets/_External")  || 
								assetPath.Contains("TextMesh Pro")
							)
						)
					{
						Debug.LogWarning($"GameObject '{gameObject.name}' in scene '{gameObject.scene.name}' references asset outside root folder: {assetPath}", gameObject);
					}
				}
			}
		}
	}
}
