using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace UnityToolbarExtender
{
    // [InitializeOnLoad]
    internal class ToolbarSceneSelector
    {
        static ToolbarSceneSelector()
        {
            RefreshScenes();			

            EditorSceneManager.sceneOpened += (_, __) =>
            {
                RefreshScenes();
            };

            EditorApplication.projectChanged += RefreshScenes;
        }
        private struct SceneData
        {
            public string path;
            public GUIContent content;
        }
        private static SceneData[] scenes;

        private static int selectedIndex = -1;

        private static string pendingSceneToOpen;

        internal static void OnToolbarGUI()
        {
            if (EditorApplication.isPlaying)
                GUI.enabled = false;
			GUILayout.FlexibleSpace();

            int newIndex = EditorGUILayout.Popup(selectedIndex, scenes.Select(s => s.content).ToArray(), CustomToolbar.dropDownStyle);

            GUI.enabled = true;

            // 不要用 GUI.changed
            if (newIndex != selectedIndex)
            {
                selectedIndex = newIndex;

                if (selectedIndex >= 0 &&
                    selectedIndex < scenes.Length)
                {
                    string path = scenes[selectedIndex].path;

                    // defer execution
                    pendingSceneToOpen = path;

                    EditorApplication.delayCall += OpenPendingScene;
                }
            }
        }

        private static void OpenPendingScene()
        {
            if (string.IsNullOrEmpty(pendingSceneToOpen))
                return;

            if (EditorApplication.isPlaying ||
                EditorApplication.isCompiling ||
                EditorApplication.isPlayingOrWillChangePlaymode)
            {
                return;
            }

            string scenePath = pendingSceneToOpen;

            pendingSceneToOpen = null;

            if (!File.Exists(scenePath))
            {
                Debug.LogWarning($"Scene not found: {scenePath}");
                return;
            }

            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorSceneManager.OpenScene(scenePath);
            }
        }

        private static void RefreshScenes()
        {
            List<SceneData> result = new();

            Scene activeScene = SceneManager.GetActiveScene();

            string[] buildScenes =
                EditorBuildSettings.scenes
                    .Where(s => s.enabled)
                    .Select(s => s.path)
                    .ToArray();

            HashSet<string> added = new();

            foreach (var path in buildScenes)
            {
                AddScene(path, true);
            }

            AddScene(string.Empty, false); //分隔線
            string[] allSceneGuids =
                AssetDatabase.FindAssets("t:scene");

            foreach (var guid in allSceneGuids)
            {
                string path =
                    AssetDatabase.GUIDToAssetPath(guid);

                if (added.Contains(path))
                    continue;

                AddScene(path, false);
            }

            scenes = result.ToArray();

            void AddScene(string path, bool inBuild)
            {
                string name = string.Empty, fullname = string.Empty;
                if (File.Exists(path))
                {
                    name = Path.GetFileNameWithoutExtension(path);
                    var folder = Path.GetDirectoryName(path);
                    fullname = Path.Combine(folder, name).Replace("\\", "/");
                }
                GUIContent content = inBuild ? new GUIContent(name, EditorGUIUtility.IconContent("BuildSettings.SelectedIcon").image) : new GUIContent(fullname);
                // GUIContent content = new GUIContent(fullname);

                int index = result.Count;

                result.Add(new SceneData()
                {
                    path = path,
                    content = content
                });

                added.Add(path);

                if (activeScene.path == path)
                {
                    selectedIndex = index;
                }
            }
        }
    }
}