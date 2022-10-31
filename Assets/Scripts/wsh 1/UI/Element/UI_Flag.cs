using UnityEngine;
using UnityEngine.UI;
using TMPro;
using WSH.Core.Manager;
using Debug = WSH.Util.Debug;
using System.Linq;

namespace WSH.UI
{
    public class UI_Flag : UIBase
    {
        protected Transform cam;
        protected TextMeshProUGUI text_Name;
        public Button button_Flag;
        protected UIAnimator[] anims;

        public TagBase targetEntity;
        UIManager um;
        public override void Initialize()
        {
            cam = FindObjectsOfType<Camera>().Where(c => c.name == "Camera_Map").First().transform;
        }
        void LateUpdate()
        {
            var pos = targetEntity.transform.position;
            transform.position = pos;
            transform.LookAt(transform.position + cam.rotation * Vector3.forward,
                cam.rotation * Vector3.up);
        }

        public void Flagging(TagBase target)
        {
            cam = FindObjectsOfType<Camera>().Where(c => c.name == "Camera_Map").First().transform;
            um = FindObjectOfType<UIManager>();
            targetEntity = target;
            anims = GetComponentsInChildren<UIAnimator>();
            transform.SetParent(target.transform);
            transform.localPosition = Vector3.zero;
            
            GetUIElement("Button_Flag", out button_Flag);
            
            button_Flag.onClick.AddListener(Active);

            if (targetEntity is Tag_Place)
            {
                GetUIElement("Text_Name", out text_Name);
                text_Name.SetText(targetEntity.customName);
            }

            if (targetEntity is Tag_Sensor)
            {
                var left = GetCanvas<UI_Canvas_LeftMenu>();
                var right = GetCanvas<UI_Canvas_RightMenu>();
                button_Flag.onClick.AddListener(left.Active);
                button_Flag.onClick.AddListener(right.Active);
                button_Flag.onClick.AddListener(ActiveOutline);
            }
        }

        static Tag_Sensor currentSelectFlag;
        public void ActiveOutline()
        {
            if (currentSelectFlag != null)
                currentSelectFlag.DeactiveOutline();

            currentSelectFlag = targetEntity as Tag_Sensor;
            currentSelectFlag.ActiveOutline();
            //target.MaterialChange(um.GetHighLightMaterial(this));
        }

        public void DeactiveOutline()
        {
            var target = targetEntity as Tag_Sensor;
            target.DeactiveOutline();
        }

        public override void Deactive()
        {
            base.Deactive();
            DeactiveOutline();
        }

        public override void Active()
        {
            Debug.Log($"{name}:Active");
            //foreach (var anim in anims)
            //    anim.Play();
            if(targetEntity is Tag_Place)
            {
                FindObjectOfType<CM_CameraManager>().MoveTo(targetEntity.index);
            }
            else if(targetEntity is Tag_Sensor)
            {
                var sensor = targetEntity as Tag_Sensor;
                FindObjectOfType<CM_CameraManager>().ZoomintoSensor(sensor);
                sensor.ActiveOutline();
                //Debug.Log($"{name}");
                //Debug.Log($"{sensor.name}");
            }
        }
    }
}