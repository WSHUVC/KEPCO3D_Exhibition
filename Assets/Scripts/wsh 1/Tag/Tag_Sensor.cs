using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WSH.UI
{
    public class Tag_Sensor : TagBase
    {
        public PlaceGroup myPlaceGroup;
        [SerializeField] Tag_SensorPart[] parts;
        protected override void Awake()
        {
            base.Awake();
            parts = GetComponentsInChildren<Tag_SensorPart>();
        }

        public void ActiveOutline()
        {
            foreach (var p in parts)
                p.ActiveOutline();
        }
        public void DeactiveOutline()
        {
            foreach (var p in parts)
                p.DeactiveOutline();
        }

        public void MaterialChange(Material mat)
        {
            foreach (var p in parts)
                p.MaterialChange(mat);
        }

        public void MaterialReset()
        {
            foreach (var p in parts)
                p.MaterialReset();
        }
    }
}