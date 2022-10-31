using Knife.HDRPOutline.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace WSH.UI
{
    [RequireComponent(typeof(OutlineObject))]
    public class Tag_SensorPart : TagBase
    {
        Material origin;
        MeshRenderer mesh;
        [SerializeField] OutlineObject outline;

        protected override void Awake()
        {
            mesh = GetComponent<MeshRenderer>();
            origin = mesh.material;
            outline = GetComponent<OutlineObject>();
            outline.enabled = false;
        }

        public void ActiveOutline()
        {
            outline.enabled = true;
        }
        public void DeactiveOutline()
        {
            outline.enabled = false;
        }

        public void MaterialChange(Material next)
        {
            mesh.material = next;
        }

        public void MaterialReset()
        {
            mesh.material = origin;
        }
    }
}
