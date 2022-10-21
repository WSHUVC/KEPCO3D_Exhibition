using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSH.Core;

namespace WSH.UI
{
    public abstract class UIBase : MonoBehaviour
    {
        protected virtual void Awake() { }
        public virtual void Active() { }
        public virtual void Deactive() { }
    }
}