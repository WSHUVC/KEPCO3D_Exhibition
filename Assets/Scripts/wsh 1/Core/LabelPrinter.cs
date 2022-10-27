using System.Collections.Generic;
using UnityEngine;

namespace WSH.Util
{
    public enum LabelGroup
    {
        Place,
        Sensor,
        SensorPart,
    }

    public struct LabelData
    {
        public LabelGroup group;
        public string personalName;

        public LabelGroup rootGroup;
        public string rootName;
    }

    public class LabelPrinter : MonoBehaviour
    {
        [SerializeField]
        public SDictionary<LabelGroup, List<Label>> labelGroupTable = new SDictionary<LabelGroup, List<Label>>();
        public void LabelingObjects()
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
                l.index = currentList.Count;
                currentList.Add(l);
            }
        }

        public void FindPreLabelObjects()
        {
            var labelingObjects = FindObjectsOfType<GameObject>();
            var labelNames = typeof(LabelGroup).GetEnumNames();

            foreach (var preLabel in labelingObjects)
            {
                var preName = preLabel.name.Split(':');
            }
        }
        public void ExtractLabel()
        {
        }
    }
}