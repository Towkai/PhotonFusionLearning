#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System;
using System.IO;

public static class GenericScriptableObjectImporter
{
    private const string MenuPath = "Assets/Create/ScriptableObject Config";

    [MenuItem(MenuPath, false, 20)]
    private static void CreateAssetFromSelectedScript()
    {
        // 1. 取得目前選中的資源
        UnityEngine.Object selectedObject = Selection.activeObject;
        if (selectedObject == null) return;

        // 2. 將資源轉換為 MonoScript (C# 腳本檔案)
        MonoScript script = selectedObject as MonoScript;
        if (script == null) return;

        // 3. 取得該腳本對應的 C# System.Type
        Type scriptType = script.GetClass();
        if (scriptType == null) return;

        // 4. 驗證該類別是否繼承自 ScriptableObject 且非抽象類別
        if (!typeof(ScriptableObject).IsAssignableFrom(scriptType) || scriptType.IsAbstract)
        {
            Debug.LogWarning($"[{script.name}] 不是有效的 ScriptableObject 類別，無法建立 Config。");
            return;
        }

        // 5. 獲取目前滑鼠點擊所在的資料夾路徑
        string path = GetSelectedFolderPath();
        
        // 6. 組合預設檔案名稱 (例如: NewMyData.asset)
        string defaultName = $"New{scriptType.Name}.asset";
        string assetPath = AssetDatabase.GenerateUniqueAssetPath(Path.Combine(path, defaultName));

        // 7. 動態實例化該型態的 ScriptableObject
        ScriptableObject asset = ScriptableObject.CreateInstance(scriptType);
        
        // 8. 建立資產並進入重新命名狀態
        ProjectWindowUtil.CreateAsset(asset, assetPath);
    }

    // 驗證選單是否應該顯示 (只有點擊 C# 腳本才啟用按鍵)
    [MenuItem(MenuPath, true)]
    private static bool ValidateCreateAssetFromSelectedScript()
    {
        UnityEngine.Object selectedObject = Selection.activeObject;
        if (selectedObject == null) return false;

        // 必須是 MonoScript (C# 腳本)
        MonoScript script = selectedObject as MonoScript;
        if (script == null) return false;

        // 檢查該腳本類別是否繼承自 ScriptableObject
        Type scriptType = script.GetClass();
        if (scriptType == null) return false;

        return typeof(ScriptableObject).IsAssignableFrom(scriptType) && !scriptType.IsAbstract;
    }

    // 輔助方法：獲取當前選定的資料夾路徑
    private static string GetSelectedFolderPath()
    {
        string path = "Assets";
        foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
        {
            path = AssetDatabase.GetAssetPath(obj);
            if (File.Exists(path))
            {
                path = Path.GetDirectoryName(path);
            }
            break;
        }
        return path;
    }
}
#endif
