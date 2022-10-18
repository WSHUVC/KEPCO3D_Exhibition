using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace WSH.Util
{
    [ExecuteAlways]
    public class MaterialPalette : MonoBehaviour
    {
        public Material[] materials;
        public List<Material> selectedMaterials = new List<Material>();
        public MaterialChanger[] matChangers;
        public void Initialize()
        {
            matChangers = FindObjectsOfType<MaterialChanger>();
            if (materials.Length == 0)
            {
                var temps = Resources.LoadAll<Material>("");
                Dictionary<string, Material> table = new Dictionary<string, Material>();
                foreach (var t in temps)
                {
                    if (!table.TryGetValue(t.name, out var value))
                        table.Add(t.name, t);
                    else
                    {
                        Debug.Log($"SameName Material : {t.name}_{AssetDatabase.GetAssetPath(t)}, {AssetDatabase.GetAssetPath(value)}");
                    }
                }
                materials = table.Select(t => t.Value).ToArray();
            }
        }

        private void OnEnable()
        {
            Initialize();
        }

        public int index;
        [MenuItem("Tools/MaterialPreview_Front #5")]
        public static void MaterialPreview_Forward()
        {
            var mp = FindObjectOfType<MaterialPalette>();
            foreach (var mc in mp.matChangers)
            {
                if (mp.index >= mp.materials.Length)
                    mp.index = 0;
                mc.MaterialChange(mp.materials[mp.index++]);
            }
        }

        [MenuItem("Tools/MaterialPreview_Back #4")]
        public static void MaterialPreview_Back()
        {
            var mp = FindObjectOfType<MaterialPalette>();
            foreach (var mc in mp.matChangers)
            {
                if (mp.index < 0)
                    mp.index = mp.materials.Length - 1;

                mc.MaterialChange(mp.materials[mp.index--]);
            }
        }

        [MenuItem("Tools/KeepMaterial #3")]
        public static void KeepMaterial()
        {
            var mp = FindObjectOfType<MaterialPalette>();
            if (mp.selectedMaterials.Contains(mp.materials[mp.index]))
                return;

            mp.selectedMaterials.Add(mp.materials[mp.index]);
        }

        [MenuItem("Tools/MaterialPaletteChange")]
        public static void MaterialPaletteChange()
        {
            var mp = FindObjectOfType<MaterialPalette>();
            mp.materials = mp.selectedMaterials.ToArray();
            mp.selectedMaterials.Clear();
        }

        [MenuItem("Tools/MaterialPreview")]
        public static void MaterialPreview()
        {
            
            var mp = FindObjectOfType<MaterialPalette>();
            mp.index = 0;
            foreach(var mc in mp.matChangers)
            {
                mc.MaterialChange(mp.materials[mp.index]);
                mp.index++;
                if (mp.index >= mp.materials.Length)
                    mp.index = 0;
            }
        }
    }
}