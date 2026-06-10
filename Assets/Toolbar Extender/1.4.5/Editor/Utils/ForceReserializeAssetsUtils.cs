using System;
using UnityEditor;


namespace UnityToolbarExtender {
    public static class ForceReserializeAssetsUtils {
        public static void ForceReserializeAllAssets() {
            if (!EditorUtility.DisplayDialog("Attention", "Do you want to force reserialize all assets? This can be time heavy operation and result in massive list of changes.", "Ok", "Cancel")) {
                return;
            }

            AssetDatabase.ForceReserializeAssets();
        }

        public static void ForceReserializeSelectedAssets(string[] assetGUIDs = null) {
            if (assetGUIDs == null)
                assetGUIDs = Selection.assetGUIDs;
            if (assetGUIDs.Length == 0) {
                EditorUtility.DisplayDialog("Attention", "No assets are selected.", "Ok");
                return;
            }

            var assetPaths = Array.ConvertAll<string, string>(assetGUIDs, AssetDatabase.GUIDToAssetPath);
            AssetDatabase.ForceReserializeAssets(assetPaths);
        }
        public static void ForceReserializeAssets() {
            var assetGUIDs = Selection.assetGUIDs;
            if (assetGUIDs == null || assetGUIDs.Length == 0) {
                if (EditorUtility.DisplayDialog("ForceReserializeAllAssets", "Are you sure?", "Yes", "No"))
                    ForceReserializeAllAssets();
            }
            else {
                ForceReserializeSelectedAssets(assetGUIDs);
            }
        }
    }
}