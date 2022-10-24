using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace WSH.UI
{
    public class TagBase : MonoBehaviour
    {
        public int index;
        public GameObject myFlag;
        public string customName
        {
            get
            {
                var name = gameObject.name.Split(':');
                if (name.Length > 0)
                    return name.Last();
                return gameObject.name;
            }
        }
    }
}
