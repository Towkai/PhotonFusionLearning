using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class ShowExtensionInProject: EditorWindow
{
    static ShowExtensionInProject()
    {
        EditorApplication.projectWindowItemOnGUI += ItemOnGUI;
    }

    static void ItemOnGUI(string guid, Rect rect)
    {
        if (rect.height > 20)
            return;
        var path = AssetDatabase.GUIDToAssetPath(guid);
        if (AssetDatabase.IsValidFolder(path))
            return;
        var ext = System.IO.Path.GetExtension(path);

        if (!string.IsNullOrEmpty(ext))
        {
            Rect labelRect = new Rect(rect);
            var style = new GUIStyle(EditorStyles.miniLabel)
            {
                alignment = TextAnchor.LowerRight,
            };
            style.normal.textColor = Color.gray;
            GUI.Label(labelRect, ext, style);
        }
    }
}