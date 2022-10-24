using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WSH.UI
{
    public class UI_Canvas_LeftMenu : CanvasBase
    {
        UI_Panel_InfoTab panel_InfoTab;
        public override void Initialize()
        {
            GetPanel(out panel_InfoTab);
        }

        public override void Active()
        {
            base.Active();
            panel_InfoTab.PlayAnimation();
        }

        public override void Deactive()
        {
            base.Deactive();
            panel_InfoTab.PlayAnimation(true);
        }
    }
}