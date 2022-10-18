using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using WSH.Core;

namespace WSH.Util
{
    [ExecuteAlways]
    public class MaterialChanger : MonoBehaviour
    {
        MeshRenderer target;
        Material originalMaterial;
        private void OnEnable()
        {
            target = GetComponent<MeshRenderer>();
            if (target == null)
            {
                Destroy(this);
                return;
            }
            originalMaterial = target.sharedMaterial;
        }

        Material currentMat;

        public void MaterialChange(Material newMaterial)
        {
            currentMat = newMaterial;
            var temp = Instantiate(newMaterial);
            target.material = temp;
        }
        public void Repaint()
        {
            var temp = target.material;
            Destroy(temp);

            target.material = originalMaterial;
        }
    }
}