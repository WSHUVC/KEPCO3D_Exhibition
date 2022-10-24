using UnityEngine;
using UnityEngine.UI;
using TMPro;
using WSH.Core.Manager;
using Debug = WSH.Util.Debug;

namespace WSH.UI
{
    public class UI_Flag : UIBase
    {
        protected Transform cam;
        protected TextMeshProUGUI text_Name;
        public Button button_Flag;
        protected UIAnimator[] anims;
        protected UIManager um;

        public TagBase targetEntity;

        public override void Initialize()
        {
            cam = Camera.main.transform;
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
            targetEntity = target;
            cam = Camera.main.transform;
            anims = GetComponentsInChildren<UIAnimator>();
            um = FindObjectOfType<UIManager>();
            transform.SetParent(target.transform);
            transform.localPosition = Vector3.zero;
            if (targetEntity is Tag_Place)
            {
                GetUIElement("Text_Name", out text_Name);
                text_Name.SetText(targetEntity.customName);
            }
            GetUIElement("Button_Flag", out button_Flag);
            button_Flag.onClick.AddListener(Active);
            if (targetEntity is Tag_Sensor)
            {
                var right = GetCanvas<UI_Canvas_RightMenu>();
                var left = GetCanvas<UI_Canvas_LeftMenu>();
                button_Flag.onClick.AddListener(right.Active);
                button_Flag.onClick.AddListener(left.Active);
            }
        }

        public override void Active()
        {
            Debug.Log($"{name}:Active");
            //foreach (var anim in anims)
            //    anim.Play();
            if(targetEntity is Tag_Place)
            {
                FindObjectOfType<CM_CameraManager>().MoveToIndexPoint(targetEntity.index);
            }
            else if(targetEntity is Tag_Sensor)
            {
                FindObjectOfType<CM_CameraManager>().ZoomintoSensor(targetEntity.index);
            }
        }
        public override void Deactive()
        {
            //foreach (var anim in anims)
            //    anim.Play(false);
        }
    }
}