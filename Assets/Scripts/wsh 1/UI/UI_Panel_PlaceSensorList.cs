using Debug = WSH.Util.Debug;
using UnityEngine;
using UnityEngine.UI;

namespace WSH.UI
{
    public class UI_Panel_PlaceSensorList : PanelBase
    {
        public UI_PlaceSensor[] button_Sensors;
        public override void Initialize()
        {
            button_Sensors = GetComponentsInChildren<UI_PlaceSensor>();
            foreach(var b in button_Sensors)
            {
                int index = b.index;
                b.GetComponent<Button>().onClick.AddListener(()=>OnClick_MoveToSensor(index));
            }
        }

        void OnClick_MoveToSensor(int index)
        {
            Debug.Log($"{name}:OnClick_MoveToSensor:index={index}");
            GetCanvas<UI_Canvas_LeftMenu>().Active();
            GetCanvas<UI_Canvas_RightMenu>().Active();
        }
    }
}