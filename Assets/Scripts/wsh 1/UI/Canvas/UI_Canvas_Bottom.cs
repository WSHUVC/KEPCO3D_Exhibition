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
            GetPanel(out panel_BottomButtons);
        }
        public override void Active()
        {
            base.Active();
            panel_BottomButtons.Active();
            PlayAnimation(panel_BottomButtons);
        }
        public override void Deactive()
        {
            base.Deactive();
            panel_BottomButtons.Deactive();
            RewindAnimation(panel_BottomButtons);
        }
    }
}