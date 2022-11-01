using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WSH.UI
{
    public class UI_Panel_Map : PanelBase
    {

        public override void Active()
        {
            base.Active();
            gameObject.SetActive(true);
        }

        public override void Deactive()
        {
            base.Deactive();
            gameObject.SetActive(false);
        }
    }
}