using UnityEditor;
using UnityEngine;

namespace UnityToolbarExtender
{
	internal class ToolbarReserializeAssets
	{
		static ToolbarReserializeAssets()
		{
			reserializeAssetsBtn = EditorGUIUtility.IconContent("preaudioloopoff");
			reserializeAssetsBtn.tooltip = "Reserialize Selected/All Asset(s)";
		}
		private static GUIContent reserializeAssetsBtn;
		internal static void OnToolbarGUI()
		{
			if (GUILayout.Button(reserializeAssetsBtn, CustomToolbar.commandButtonStyle)) {
				UnityToolbarExtender.ForceReserializeAssetsUtils.ForceReserializeAssets();
			}
		}
	}
}