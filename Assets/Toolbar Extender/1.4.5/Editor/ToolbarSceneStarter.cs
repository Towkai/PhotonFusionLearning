using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityToolbarExtender
{
	internal class ToolbarSceneStarter
	{
		internal static void OnToolbarGUI()
		{
			if(GUILayout.Button(new GUIContent((Texture2D)AssetDatabase.LoadAssetAtPath($"{CustomToolbar.GetPackageRootPath}/Icons/LookDevSingle0@2x.png", typeof(Texture2D)), "Start from Scene 0"), CustomToolbar.commandButtonStyle))
			{
				if (!EditorApplication.isPlaying) {
					EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
					EditorPrefs.SetInt("LastActiveSceneToolbar", EditorSceneManager.GetActiveScene().buildIndex);
					EditorSceneManager.OpenScene(SceneUtility.GetScenePathByBuildIndex(0));
				}
				EditorApplication.isPlaying = !EditorApplication.isPlaying;
			}
		}
	}
}