using UnityEngine;
using UnityEngine.UI;

namespace WSH.UI
{
    public class UI_PlaceSensor : UIBase
    {
        public int index;

        Button button_PlaceSensor;

        public override void Initialize()
        {
            base.Initialize();
            //Debug.Log(gameObject.name);
            GetUIElement(gameObject.name, out button_PlaceSensor);
            button_PlaceSensor.onClick.AddListener(OnClick_MoveToSensor);
        }

        void OnClick_MoveToSensor()
        {
            Debug.Log($"{name}:OnClick_MoveToSensor:index={index}");
            GetCanvas<UI_Canvas_LeftMenu>().Active();
            GetCanvas<UI_Canvas_RightMenu>().Active();
            CM_CameraManager.instance.ZoomintoSensor(index);
        }

    }
}
