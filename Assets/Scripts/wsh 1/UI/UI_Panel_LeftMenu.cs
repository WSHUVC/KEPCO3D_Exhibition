using UnityEngine.UI;

namespace WSH.UI
{
    public class UI_Panel_LeftMenu : PanelBase
    {
        Button button_FacilitiesInfo;
        UI_Canvas_Tabs canvas_Tabs;
        public override void Initialize()
        {
            GetUIElement("Button_FacilitiesInfo",out button_FacilitiesInfo);
            canvas_Tabs = GetCanvas<UI_Canvas_Tabs>();
            button_FacilitiesInfo.onClick.AddListener(OnClick_Info);
        }

        void OnClick_Info()
        {
            canvas_Tabs.Active();
        }
    }
}