using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static WSH.Util.Wtil;

namespace WSH.Util
{
    public enum LabelGroup
    {
        Place,
        Sensor,
        SensorPart,
    }

    public class Label : MonoBehaviour
    {
        public LabelGroup group;
        public string personalName;
        public int index;

        static Dictionary<LabelGroup, IEnumerable<Label>> labelGroupTable = new Dictionary<LabelGroup, IEnumerable<Label>>();
        void AutoIndexing()
        {
            if (!labelGroupTable.TryGetValue(group, out var myGroup))
            {
                myGroup = FindObjectsOfType<Label>().Where(l => l.group == group);
                labelGroupTable.Add(group, myGroup);
            }
            labelGroupTable.TryGetValue(group, out myGroup, GroupCheck);
        }


        public bool GroupCheck(LabelGroup g1, LabelGroup g2)
        {
            return g1 == g2;
        }
    }
}
