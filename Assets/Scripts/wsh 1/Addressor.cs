using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Addressor : MonoBehaviour
{
    static Addressor instance;
    Dictionary<UIBehaviour, List<GameObject>> uiObjects;
    Dictionary<GameObject, UIBehaviour> binder;
    private void Awake()
    {
        if (instance != null)
            return;

        //binder = new Dictionary<GameObject, UIBehaviour>();
        //uiObjects = new Dictionary<UIBehaviour, List<GameObject>>();
        //var uiElements = FindObjectsOfType<UIBehaviour>();

        //foreach(var u in uiElements)
        //{
        //    if(!uiObjects.TryGetValue(u, out var list))
        //    {
        //        uiObjects.Add(u, new List<GameObject>());
        //    }
        //    if (uiObjects[u].Contains(u.gameObject))
        //        Debug.LogError($"OverLap UIElement!{u.GetType().Name}/{u.gameObject}");
        //    else
        //    {
        //        uiObjects[u].Add(u.gameObject);
        //        binder.Add(u.gameObject, u);
        //    }
        //}
    }

    //public void GetUIElement<T>(string name, out T result)
    //{
    //    if(!uiObjects])
    //}
}
