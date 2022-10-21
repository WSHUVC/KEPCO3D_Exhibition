using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System;
using WSH.Core.Util;
using WSH.Core;
using WSH.Core.Manager;

namespace WSH.UI
{
    public class UI_Flag : UIBase
    {
        protected Transform cam;
        protected TMP_Text text_Info;
        public Button button_MoveToPoint;
        protected UIAnimation[] anims;
        protected UIManager um;

        public TagBase targetEntity;
        public int index;

        protected override void Awake()
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
            anims = GetComponentsInChildren<UIAnimation>();
            um = FindObjectOfType<UIManager>();
            transform.SetParent(target.transform);
            transform.localPosition = Vector3.zero;

            if (target is Tag_Place)
            {
                text_Info = GetComponentInChildren<TextMeshProUGUI>();
                text_Info.SetText(target.customName);
            }
            button_MoveToPoint = GetComponentInChildren<Button>();
            button_MoveToPoint.onClick.AddListener(Active);
        }

        public override void Active()
        {
            foreach (var anim in anims)
                anim.Play();

            if(targetEntity is Tag_Place)
            {
                FindObjectOfType<CM_CameraManager>().MoveToIndexPoint(index);
            }
            else if(targetEntity is Tag_Sensor)
            {
                FindObjectOfType<CM_CameraManager>().ZoomintoSensor(index);
            }
        }
        public override void Deactive()
        {
            foreach (var anim in anims)
                anim.Play(false);
        }
    }
}