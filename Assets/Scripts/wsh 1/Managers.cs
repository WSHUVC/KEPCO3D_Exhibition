using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using WSH.Core.Manager;
using WSH.UI;
using WSH.Util;

namespace WSH.Core
{
    [Serializable]
    public class Managers : MonoBehaviour
    {
        public enum GroupIndex
        {
            ETC,
            Road,
            Tower,
            Ground,
            Machine,
            Building,
            SolarPanel,
            LargeTower,
        }

        public MeshRenderer[] etcs;
        public Tag_Road[] roads;
        public Tag_Tower[] towers;
        public Tag_Ground[] grounds;
        public Tag_Machine[] machines;
        public Tag_Building[] buildings;
        public Tag_LargeTower[] largeTowers;
        public Tag_SolarPanel[] solarPanels;
        public Tag_Sensor[] sensors;
        public Tag_Place[] places;
        public List<Material> defaultMaterials;

        public UI_Flag prefab_Flag_Sensor;
        public UI_Flag prefab_Flag_Place;
        
        public void FindTagObjects()
        {
            roads = FindObjectsOfType<Tag_Road>();
            towers = FindObjectsOfType<Tag_Tower>();
            grounds = FindObjectsOfType<Tag_Ground>();
            machines = FindObjectsOfType<Tag_Machine>();
            buildings = FindObjectsOfType<Tag_Building>();
            largeTowers = FindObjectsOfType<Tag_LargeTower>();
            solarPanels = FindObjectsOfType<Tag_SolarPanel>();
            sensors = FindObjectsOfType<Tag_Sensor>();
            places = FindObjectsOfType<Tag_Place>();
        }
        public void SetDefaultMaterial()
        {
            SetDefalutMaterial(roads, GroupIndex.Road);
            SetDefalutMaterial(towers, GroupIndex.Tower);
            SetDefalutMaterial(grounds, GroupIndex.Ground);
            SetDefalutMaterial(machines, GroupIndex.Machine);
            SetDefalutMaterial(buildings, GroupIndex.Building);
            SetDefalutMaterial(largeTowers, GroupIndex.LargeTower);
            SetDefalutMaterial(solarPanels, GroupIndex.SolarPanel);
            etcs = FindObjectsOfType<MeshRenderer>()
                .Where(g => g.GetComponent<TagBase>() == null)
                .ToArray();
            SetDefalutMaterial(etcs, GroupIndex.ETC);
        }
        public void PlaceFlagging()
        {
            places = FindObjectsOfType<Tag_Place>();
            Flagging(places, prefab_Flag_Place);
        }
        public void SensorFlagging()
        {
            sensors = FindObjectsOfType<Tag_Sensor>();
            Flagging(sensors, prefab_Flag_Sensor);
        }
        public void FlagCleaning()
        {
            var flags = FindObjectsOfType<UI_Flag>();
            for(int i = 0; i < flags.Length; ++i)
            {
                DestroyImmediate(flags[i].gameObject);
            }
        }
        public void ActivePlaceFlag()
        {
            GroupFlagControl(places,true);
        }
        public void ActiveSensorFlag()
        {
            GroupFlagControl(sensors,true);
        }
        public void DeactiveSensorFlag()
        {
            GroupFlagControl(sensors, false);
        }
        public void DeactivePlaceFlag()
        {
            GroupFlagControl(places, false);
        }

        void GroupFlagControl(TagBase[] group, bool isActive)
        {
            foreach (var f in group)
                f.gameObject.SetActive(isActive);
        }

        void Flagging(TagBase[] points, UI_Flag prefab)
        {
            foreach (var p in points)
            {
                var flag = Instantiate(prefab);
                flag.Flagging(p);
                if (p.myFlag != null)
                    DestroyImmediate(p.myFlag.gameObject);

                p.myFlag = flag.gameObject;
            }
        }
        void SetDefalutMaterial(MeshRenderer[] group, GroupIndex index)
        {
            var i = (int)index;
            foreach (var mesh in group)
            {
                mesh.material = defaultMaterials[i];
            }
        }
        void SetDefalutMaterial<T>(T[] group, GroupIndex index) where T : MonoBehaviour
        {
            int i = (int)index;
            foreach (var g in group)
            {
                foreach (var mesh in g.GetComponentsInChildren<MeshRenderer>())
                {
                    mesh.gameObject.TryAddComponent<T>();
                    mesh.material = defaultMaterials[i];
                    for (int c = 0; c < mesh.sharedMaterials.Length; ++c)
                    {
                        mesh.sharedMaterials[c] = defaultMaterials[i];
                    }
                }
            }
        }

        private void Awake()
        {
            DeactivePlaceFlag();
            DeactiveSensorFlag();
        }
    }
}