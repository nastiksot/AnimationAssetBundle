using System.IO;
using UnityEditor;

namespace Editor
{
    public static class AssetBundlesBuilder
    {
        [MenuItem("AssetBundles/Build Windows")]
        public static void BuildWindowsAssetBundles()
        {
            string assetBundleDirectory = "Assets/AssetBundles/Windows";
            if (!Directory.Exists(assetBundleDirectory))
            {
                Directory.CreateDirectory(assetBundleDirectory);
            }

            BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
        }

        [MenuItem("AssetBundles/Build Android")]
        public static void BuildAndroidAssetBundles()
        {
            var assetBundleDirectory = "Assets/AssetBundles/Android";
            if (!Directory.Exists(assetBundleDirectory))
            {
                Directory.CreateDirectory(assetBundleDirectory);
            }

            BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.Android);
        }

        [MenuItem("AssetBundles/Build IOS")]
        public static void BuildIosAssetBundles()
        {
            var assetBundleDirectory = "Assets/AssetBundles/IOS";
            if (!Directory.Exists(assetBundleDirectory))
            {
                Directory.CreateDirectory(assetBundleDirectory);
            }

            BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.iOS);
        }

        [MenuItem("AssetBundles/Build Linux")]
        public static void BuildLinuxAssetBundles()
        {
            var assetBundleDirectory = "Assets/AssetBundles/Linux";
            if (!Directory.Exists(assetBundleDirectory))
            {
                Directory.CreateDirectory(assetBundleDirectory);
            }

            BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.StandaloneLinux64);
        }
    }
}