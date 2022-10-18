using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace WSH.Util
{
    [InitializeOnLoadAttribute]
    public static class ModelMaterialExtracter
    {
        [MenuItem("Tools/MaterialExtract")]
        static void MaterialExtract()
        {
            var objs = Resources.LoadAll("FBX");
            foreach (var o in objs)
            {
                var path = AssetDatabase.GetAssetPath(o);
                if(path.Contains(".FBX"))
                    ExtractMaterials(path, "Assets/WSH_MAT");
            }
        }

        public static void ExtractMaterials(string assetPath, string destinationPath)
        {
            HashSet<string> hashSet = new HashSet<string>();
            IEnumerable<Object> enumerable = from x in AssetDatabase.LoadAllAssetsAtPath(assetPath)
                                             where x.GetType() == typeof(Material)
                                             select x;
            foreach (Object item in enumerable)
            {
                var path = string.Join(Path.DirectorySeparatorChar.ToString(), new[] { destinationPath, item.name }) + ".mat";
                //string path = System.IO.Path.Combine(destinationPath, item.name) + ".mat";
                //path = AssetDatabase.GenerateUniqueAssetPath(path);
                string value = AssetDatabase.ExtractAsset(item, path);
                if (string.IsNullOrEmpty(value))
                {
                    hashSet.Add(assetPath);
                }
            }

            foreach (string item2 in hashSet)
            {
                AssetDatabase.WriteImportSettingsIfDirty(item2);
                AssetDatabase.ImportAsset(item2, ImportAssetOptions.ForceUpdate);
            }
        }
    }
}