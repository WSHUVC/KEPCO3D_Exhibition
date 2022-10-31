using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WSH.UI
{
    public class UI_Canvas_RightMenu : CanvasBase
    {
        UI_Panel_RightMenu panel_RightMenu;
        public override void Initialize()
        {
            GetPanel(out panel_RightMenu);
        }
        public override void Active()
        {
            base.Active();
            PlayAnimation(panel_RightMenu);
        }

        public override void Deactive()
        {
            base.Deactive();
            DeleteGraph();
            RewindAnimation(panel_RightMenu);
        }

        void DeleteGraph()
        {
            GameObject.FindObjectOfType<QuadScript>().DeleteGraph();
        }
    }
}