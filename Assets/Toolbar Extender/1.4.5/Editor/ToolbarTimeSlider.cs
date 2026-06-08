using UnityEditor;
using UnityEngine;

namespace UnityToolbarExtender
{
    internal class ToolbarTimeSlider
    {
        private static float minTime = 0f;
        private static float maxTime = 10f;

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
                $"Time {Time.timeScale:F1}",
                labelStyle,
                GUILayout.Width(70));

            float newScale =
                GUILayout.HorizontalSlider(
                    Time.timeScale,
                    minTime,
                    maxTime,
                    GUILayout.Width(120));

            if (!Mathf.Approximately(newScale, Time.timeScale))
            {
                Time.timeScale = newScale;
            }
        }
    }
}