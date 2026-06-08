using UnityEditor;
using UnityEngine;

namespace UnityToolbarExtender
{
    internal class ToolbarFPSSlider
    {
        private static int minFPS = 1;
        private static int maxFPS = 120;

        private static int selectedFPS = 60;

        private static GUIStyle labelStyle;

        private static void InitStyle()
        {
            if (labelStyle != null)
                return;

            labelStyle = new GUIStyle(EditorStyles.label)
            {
                alignment = TextAnchor.MiddleCenter
            };
        }

        internal static void OnToolbarGUI()
        {
            InitStyle();

            GUILayout.Space(4);

            GUILayout.Label(
                $"FPS {selectedFPS}",
                labelStyle,
                GUILayout.Width(60));

            int newFPS = Mathf.RoundToInt(
                GUILayout.HorizontalSlider(
                    selectedFPS,
                    minFPS,
                    maxFPS,
                    GUILayout.Width(120)));

            if (newFPS != selectedFPS)
            {
                selectedFPS = newFPS;

                if (EditorApplication.isPlaying)
                {
                    Application.targetFrameRate = selectedFPS;
                }
            }
        }
    }
}