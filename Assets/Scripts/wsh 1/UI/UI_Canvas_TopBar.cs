using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace WSH.UI
{
    public class UI_Canvas_TopBar : CanvasBase
    {
        UI_Panel_TopBar panel_TopBar;

        public override void Initialize()
        {
            base.Initialize();
            TryGetPanel("Panel_TopBar", out panel_TopBar);
        }

        public override void Active()
        {
            base.Active();
            PlayAnimation(panel_TopBar);
        }
        public override void Deactive()
        {
            base.Deactive();
            PlayAnimation(panel_TopBar, true);
        }
    }
}