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
        protected static List<TagBase> tags = new List<TagBase>();

        protected virtual void Awake()
        {
            if (tags.Contains(this))
                return;
            tags.Add(this);
        }
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
