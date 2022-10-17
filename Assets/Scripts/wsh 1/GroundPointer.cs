using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using WSH.Core.Util;
using WSH.Util;

namespace WSH.Core.Util
{
    public class GroundPointer : MonoBehaviour
    {
        [MenuItem("Tools/Pointing %#t")]
        public static void Pointing()
        {
            var targets = UnityEditor.Selection.gameObjects;
            FindObjectOfType<GroundPointer>().SpawnPoint(targets);
        }

        public List<GameObject> pointObjects = new List<GameObject>();
        void SpawnPoint(GameObject[] targets)
        {
            pointObjects.Clear();
            var gps = FindObjectsOfType<Tag_GP>();
            if(gps.Length>0)
                pointObjects = gps.Select(g=>g.target.gameObject).ToList();

            for (int i = 0; i < targets.Length; ++i)
            {
                if (pointObjects.Contains(targets[i]))
                    continue;

                pointObjects.Add(targets[i]);

                var pointer = new GameObject();
                var gp = pointer.TryAddComponent<Tag_GP>();
                gp.transform.SetParent(targets[i].transform);
                gp.Initialize();
            }
            for(int i = 0; i < pointObjects.Count; ++i)
            {
                pointObjects[i].GetComponentInChildren<Tag_GP>().gameObject.name = $"GP_{i}";
            }
        }
    }
}

