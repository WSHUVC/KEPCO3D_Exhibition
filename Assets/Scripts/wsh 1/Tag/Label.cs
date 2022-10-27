using UnityEngine;

namespace WSH.Util
{
    public enum LabelGroup
    {
        Place,
        Sensor,
        SensorPart,
    }

    [SerializeField]
    public class Label : MonoBehaviour
    {
        public LabelGroup group;
        public string personalName;
        public int index;
    }
}
