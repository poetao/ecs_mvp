using System;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace Toybox.Editor
{
    public class ToyboxTools : MonoBehaviour
    {
        [MenuItem("Tool/AtlasMaker")]
        [Obsolete]
        static private void MakeAtlas()
        {
            int rootIndex = Application.dataPath.IndexOf("Assets", StringComparison.InvariantCulture) + "Assets".Length;
            DirectoryInfo rootDirInfo = new DirectoryInfo(Application.dataPath + "/Images");
            foreach (DirectoryInfo dirInfo in rootDirInfo.GetDirectories())
            {
                foreach (FileInfo pngFile in dirInfo.GetFiles("*.png", SearchOption.AllDirectories))
                {
                    string allPath = pngFile.FullName;
                    string assetPath = allPath.Substring(rootIndex);
                    Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets" + assetPath);
                    GameObject go = new GameObject(sprite.name);
                    go.AddComponent<SpriteRenderer>().sprite = sprite;
                    string prefabDir = "Resources/Prefab/Sprite" + pngFile.DirectoryName.Substring(rootIndex);
                    if (!Directory.Exists(Application.dataPath + "/" + prefabDir))
                    {
                        Directory.CreateDirectory(Application.dataPath + "/" + prefabDir);
                    }
                    _ = PrefabUtility.CreatePrefab("Assets/" + prefabDir + "/" + sprite.name + ".prefab", go);
                    DestroyImmediate(go);
                }
            }
        }
    }
}
