using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Binder<T> where T : UIBehaviour
{
    public T item;
    public Binder(string name)
    {
        //var components = GetComponentsInChildren<T>();
        //foreach(var c in components)
        //{
        //    if(c.name.Equals(name))
        //    {
        //        item = c;
        //    }
        //}
    }
}
public class Panel_RightSideMenu : MonoBehaviour
{
    public Binder<Button> button_Open;
}
