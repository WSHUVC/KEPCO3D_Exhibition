using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using WSH.Core.Manager;
using WSH.Util;

namespace WSH.Core
{
    [Serializable]
    public class Managers : MonoBehaviour
    {
        static Managers _ins;
        public static Managers instance
        {
            get
            {
                if(_ins==null)
                {
                    _ins = FindObjectOfType<Managers>();
                    _ins.Initialize();
                }
                return _ins;
            }
            private set {
                _ins = value;
            }
        }
        protected virtual void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }
            instance = this;
            Initialize();
        }
        [SerializeField] UIManager _uiManager = new UIManager();
        [SerializeField] MachineManager _machineManager = new MachineManager();

        void Initialize()
        {
            _uiManager.Initialize();
            _machineManager.Initialize();
        }
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