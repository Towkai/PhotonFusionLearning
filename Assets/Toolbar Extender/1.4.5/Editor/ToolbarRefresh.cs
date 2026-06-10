using UnityEditor;
using UnityEngine;

namespace UnityToolbarExtender
{
	internal class ToolbarRefresh
	{
		static ToolbarRefresh()
		{
			RefreshBtn = EditorGUIUtility.IconContent("Refresh");
			RefreshBtn.tooltip = "Refresh Assets";
		}
		private static GUIContent RefreshBtn;
		internal static void OnToolbarGUI()
		{
			if (GUILayout.Button(RefreshBtn, CustomToolbar.commandButtonStyle)) {
				AssetDatabase.Refresh();
				Debug.Log("Refresh");
			}
		}
	}
}