using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WSH.UI
{
    public class UI_Canvas_Bottom : CanvasBase
    {
        [SerializeField] UI_Panel_BottomButtons panel_BottomButtons;
        public override void Initialize()
        {
            base.Initialize();
            TryGetPanel("Panel_BottomButtons", out panel_BottomButtons);
        }
        public override void Active()
        {
            base.Active();
            PlayAnimation(panel_BottomButtons);
        }
        public override void Deactive()
        {
            base.Deactive();
            PlayAnimation(panel_BottomButtons, true);
        }
    }
}