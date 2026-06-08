//Require: https://github.com/marijnz/unity-toolbar-extender.git
using UnityEditor;
using UnityEngine;

namespace UnityToolbarExtender
{
    [InitializeOnLoad]
    internal class CustomToolbar
    {
        internal const string GetPackageRootPath = "Assets/Toolbar Extender/1.4.5";
        public static GUIStyle commandButtonStyle => new GUIStyle(EditorStyles.toolbarButton)
        {
            fixedWidth = 41,
        };
		public static GUIStyle dropDownStyle => new GUIStyle(EditorStyles.toolbarPopup)
            {
                padding = new RectOffset(5, 20, 0, 0),
                alignment = TextAnchor.MiddleRight,
                // fixedwidth = 150,
            };

        static CustomToolbar()
        {			
            ToolbarExtender.LeftToolbarGUI.Add(ToolbarSceneSelector.OnToolbarGUI);
            ToolbarExtender.LeftToolbarGUI.Add(ToolbarSceneStarter.OnToolbarGUI);
            ToolbarExtender.RightToolbarGUI.Add(ToolbarTimeSlider.OnToolbarGUI);
            ToolbarExtender.RightToolbarGUI.Add(ToolbarFPSSlider.OnToolbarGUI);
            ToolbarExtender.RightToolbarGUI.Add(ToolbarSceneRecompile.OnToolbarGUI);
            ToolbarExtender.RightToolbarGUI.Add(ToolbarReserializeSelected.OnToolbarGUI);
            ToolbarExtender.RightToolbarGUI.Add(ToolbarReserializeAll.OnToolbarGUI);
        }
    }
}