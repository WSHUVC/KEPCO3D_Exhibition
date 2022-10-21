using UnityEngine;
using Debug = WSH.Util.Debug;
using UnityEngine.UI;
using System.Collections.Generic;

namespace WSH.UI
{
    public class UI_Panel_BottomButtons : PanelBase
    {
        UI_FlagButton[] placeButtons;
        public int length => placeButtons.Length;
        Dictionary<UI_FlagButton, UI_Flag> flagButton = new Dictionary<UI_FlagButton, UI_Flag>();
        public override void Initialize()
        {
            base.Initialize();
            flagButton.Clear();
            placeButtons = GetComponentsInChildren<UI_FlagButton>();
                
            for(int i = 0; i < placeButtons.Length; ++i)
            {
                placeButtons[i].AddListener(OnClick_PlaceButton);
            }
        }

        void OnClick_PlaceButton()
        {

        }
    }
}