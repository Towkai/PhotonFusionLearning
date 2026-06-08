using UnityEditor;
using UnityEngine;

namespace UnityToolbarExtender
{
	internal class ToolbarReserializeAll
	{
		static ToolbarReserializeAll()
		{
			reserializeAllBtn = EditorGUIUtility.IconContent("P4_Updating");
			reserializeAllBtn.tooltip = "Reserialize All Assets";
		}
		private static GUIContent reserializeAllBtn;
		internal static void OnToolbarGUI()
		{
			if (GUILayout.Button(reserializeAllBtn, CustomToolbar.commandButtonStyle)) {
				UnityToolbarExtender.ForceReserializeAssetsUtils.ForceReserializeAllAssets();
			}
		}
	}
}