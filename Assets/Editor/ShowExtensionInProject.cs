using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class ShowExtensionInProject: EditorWindow
{
    static ShowExtensionInProject()
    {
        EditorApplication.projectWindowItemOnGUI += ShowExt;
        EditorApplication.projectWindowItemOnGUI += CopyGUID;
    }
    
    static void ShowExt(string guid, Rect rect)
    {
        if (rect.height > 20)
            return;
        var path = AssetDatabase.GUIDToAssetPath(guid);
        if (AssetDatabase.IsValidFolder(path))
            return;
        var ext = System.IO.Path.GetExtension(path);

        if (!string.IsNullOrEmpty(ext))
        {
            var style = new GUIStyle(EditorStyles.miniLabel)
            {
                alignment = TextAnchor.LowerRight,
            };
            style.normal.textColor = Color.gray;
            GUI.Label(rect, ext, style);
        }
    }
    static void CopyGUID(string guid, Rect rect)
    {
        GUIContent content = new GUIContent()
        {
            tooltip = guid
        };

        GUI.Label(rect, content);

        Event e = Event.current;
        if (e == null) 
            return;
        bool isClick = e.type == EventType.MouseDown && e.button == 0 && rect.Contains(e.mousePosition);
        if (isClick && e.alt)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            if (string.IsNullOrEmpty(path)) 
                return;

            string realGuid = AssetDatabase.AssetPathToGUID(path);

            // 複製到剪貼簿
            EditorGUIUtility.systemCopyBuffer = realGuid;

            // Console 提示
            Debug.Log($"Copied GUID: {realGuid}");

            // 阻止 Unity 預設點擊行為（避免選取/開啟）
            // e.Use();
        }
    }
}