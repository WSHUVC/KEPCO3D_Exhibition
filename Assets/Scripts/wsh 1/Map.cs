using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;
using WSH.Util;
using WSH.UI;
using WSH.Core.Util;

public class Map : MonoBehaviour
{
    public string[] groups = new string[]
    {
        "Wall",
        "Pipe",
        "Tower",
        "Machine",
        "Build",
        "SolaPanel",
        "Special"
    };
    public Material[] groupDefaultMats;

    Dictionary<string, Transform> groupParents = new Dictionary<string, Transform>();
    [MenuItem("Tools/MapObjectGrouping")]
    public static void MapObjectGrouping()
    {
        var map = FindObjectOfType<Map>();
        var childs = map.GetComponentsInChildren<Transform>();

        foreach(var g in map.groups)
        {
            var parent = new GameObject($"Group_{g}");
            map.groupParents.Add(g, parent.transform);
            parent.transform.SetParent(map.transform);
            parent.TryAddComponent<GroupOption>();
        }

        for(int i = 0; i < map.groups.Length; ++i)
        {
            var groupName = map.groups[i];
            var parent = map.groupParents[groupName].transform;
            int childCount = 0;
            foreach(var c in childs)
            {
                if (map.groupParents.ContainsValue(c))
                    continue;

                if (c.name.Contains(groupName, System.StringComparison.OrdinalIgnoreCase))
                {
                    c.transform.SetParent(parent.transform);
                    c.name = $"{groupName}_{childCount++}";
                    if (groupName == "SolaPanel")
                        continue;
                    var meshs = c.transform.GetComponentsInChildren<MeshRenderer>();
                    foreach (var m in meshs)
                    {
                        m.material = map.groupDefaultMats[i];
                    }
                }
            }
        }

        List<GameObject> trash = new List<GameObject>();
        foreach(var c in childs)
        {
            if (c.GetComponents<Component>().Length <= 1)
            {
                if (c.GetComponentsInChildren<Transform>().Length <= 1)
                {
                    trash.Add(c.gameObject);
                }
            }
        }

        foreach(var t in trash)
        {
            DestroyImmediate(t);
        }
    }

    [MenuItem("Tools/GroupFlagging")]
    public static void GroupFlagging()
    {
        var groupOptions = FindObjectsOfType<GroupOption>();
        var pointer = FindObjectOfType<GroundPointer>();
        foreach(var g in groupOptions)
        {
            g.LoadGroupObjects();
            switch (g.flagingOption)
            {
                //��� ������Ʈ���� �ϳ���
                case GroupOption.FlagOption.All:
                    pointer.SpawnPoint(g.groupObjects.Select(g=>g.gameObject).ToArray());
                    break;
                    //��� ������Ʈ�� �ϳ��� 
                case GroupOption.FlagOption.Total:
                    if(g.groupObjects.Count>0)
                        pointer.SpawnPoint(new GameObject[] { g.groupObjects[0].gameObject });
                    break;
                    //�ƹ��� �÷��� ��������
                case GroupOption.FlagOption.Nothing:
                    break;
            }
        }
    }
}