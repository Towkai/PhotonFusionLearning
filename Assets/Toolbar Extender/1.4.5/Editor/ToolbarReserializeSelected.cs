using UnityEditor;
using UnityEngine;

namespace UnityToolbarExtender
{
	internal class ToolbarReserializeSelected
	{
		static ToolbarReserializeSelected()
		{
			reserializeSelectedBtn = EditorGUIUtility.IconContent("Refresh");
			reserializeSelectedBtn.tooltip = "Reserialize Selected Assets";
		}
		private static GUIContent reserializeSelectedBtn;
		internal static void OnToolbarGUI()
		{
			if (GUILayout.Button(reserializeSelectedBtn, CustomToolbar.commandButtonStyle)) {
				ForceReserializeAssetsUtils.ForceReserializeSelectedAssets();
			}
		}
	}
}