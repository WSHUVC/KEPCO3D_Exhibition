using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using WSH.Core.Manager;

namespace WSH.Core
{
    [Serializable]
    public class Managers : MonoBehaviour
    {
        public static Managers instance
        {
            get;
            private set;
        }
        protected virtual void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }
            instance = this;
            _uiManager.Initialize();
            _machineManager.Initialize();
        }
        [SerializeField] UIManager _uiManager;
        [SerializeField] MachineManager _machineManager;

        public static UIManager uiManager
        {
            get => instance._uiManager;
        }
        public static MachineManager machineManager
        {
            get => instance._machineManager;
        }
    }
}