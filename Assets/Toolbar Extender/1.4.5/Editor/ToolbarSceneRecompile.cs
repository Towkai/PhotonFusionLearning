using UnityEditor;
using UnityEngine;

namespace UnityToolbarExtender
{
	internal class ToolbarSceneRecompile
	{
		static ToolbarSceneRecompile()
		{
			recompileBtn = EditorGUIUtility.IconContent("WaitSpin05");
			recompileBtn.tooltip = "Recompile";
		}
		private static GUIContent recompileBtn;
		internal static void OnToolbarGUI()
		{
			if (GUILayout.Button(recompileBtn, CustomToolbar.commandButtonStyle)) {
				UnityEditor.Compilation.CompilationPipeline.RequestScriptCompilation();
				Debug.Log("Recompile");
			}
		}
	}
}