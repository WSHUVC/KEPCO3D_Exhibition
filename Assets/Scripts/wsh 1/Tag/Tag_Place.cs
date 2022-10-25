using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WSH.UI
{
    public enum PlaceGroup
    {
        _345kVGIS,
        주변압기,
        _765kVGIS,
        태양열발전현황
    }
    public class Tag_Place : TagBase
    {
        public PlaceGroup group;
        public List<TagBase> tagMembers = new List<TagBase>();
        protected void Start()
        {
            tagMembers = tags.Where(t => t is Tag_Sensor).Select(t=>t).ToList();
            tagMembers = tagMembers.Where(t => ((Tag_Sensor)t).myPlaceGroup.Equals(group)).ToList();

            for(int i = 0; i < tagMembers.Count; ++i)
            {
                tagMembers[i].index = i;
            }
            var panels = FindObjectsOfType<UI_Panel_PlaceSensorList>();
            foreach(var p in panels)
            {
                if(p.myPlaceGroup.Equals(group))
                {
                    p.SetPlace(this);
                    return;
                }
            }
        }
    }
}