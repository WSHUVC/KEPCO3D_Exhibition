using System.Collections.Generic;
using UnityEngine;

namespace WSH.Util
{
    public class LabelPrinter: MonoBehaviour
    {
        [SerializeField]
        public SDictionary<LabelGroup, List<Label>> labelGroupTable = new SDictionary<LabelGroup, List<Label>>();
        public void FindLabels()
        {
            var labels = FindObjectsOfType<Label>();
            
            int groupCount = typeof(LabelGroup).GetEnumNames().Length;
            labelGroupTable.Clear();
            for (int i = 0; i < groupCount; ++i)
            {
                labelGroupTable.TryAdd((LabelGroup)i, new List<Label>());
            }

            foreach(var l in labels)
            {
                var labelGroup = l.group;
                List<Label> currentList = labelGroupTable[labelGroup];
                if (currentList.Contains(l))
                {
                    continue;
                }
                currentList.Add(l);
            }
        }
    }
}