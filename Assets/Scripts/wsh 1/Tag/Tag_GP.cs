using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WSH.Core.Util
{
    [ExecuteAlways]
    public class Tag_GP : MonoBehaviour
    {
        public Transform target;
        public Transform frontPoint;
        public void Initialize(Transform target)
        {
            this.target = target;
            frontPoint = new GameObject("Handle").transform;
            frontPoint.SetParent(transform);
            frontPoint.localPosition = Vector3.zero;
            frontPoint.localRotation = Quaternion.identity;
        }
    }
}