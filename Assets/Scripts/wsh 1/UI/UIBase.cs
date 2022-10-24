using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSH.Core;

namespace WSH.UI
{
    /// <summary>
    /// for Runtime UI.
    /// </summary>
    public class UIBase : WIBehaviour
    {
        protected virtual void Awake()
        {
            Initialize();
        }
    }
}