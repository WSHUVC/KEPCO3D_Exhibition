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

    //0:012
    //1:345
    //2:678
    //3:91011
    public class Tag_Place : TagBase
    {
        public PlaceGroup group;
        public List<TagBase> tagMembers = new List<TagBase>();
        protected void Start()
        {
            tagMembers = tags.Where(t => t is Tag_Sensor).Select(t=>t).ToList();
            tagMembers = tagMembers.Where(t => ((Tag_Sensor)t).myPlaceGroup.Equals(group)).ToList();
            var cm = FindObjectOfType<CM_CameraManager>();
            var sensors = cm.sensors;
            
            for(int i =0;i<sensors.Length;++i)
            {
                if (tagMembers.Contains(sensors[i]))
                {
                    sensors[i].index = i;
                    continue;
                }
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