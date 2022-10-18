using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System;
using WSH.Core.Util;
using WSH.Core.Manager;
using WSH.Core;

namespace WSH.UI
{
    public class UI_Flag : UIBase
    {
        public Tag_GP gp;
        public Transform targetEntity;
        [Header("View")]
        public Transform cam;
        public Button button_MoveToPoint;
        public TMP_Text text_Info;
        [Header("Animation Options")]
        [Range(0f, 1f)]
        public float axisScale;
        public float xSpeed;
        public float ySpeed;

        Vector2 originSize;
        Vector2 originPivot;
        RectTransform rect;
        Sequence currentAnim;
        bool isAnim;

        private void LateUpdate()
        {
            var pos = targetEntity.position;
            pos.y += 60f;
            transform.position = pos; 
            transform.LookAt(transform.position + cam.rotation * Vector3.forward,
                cam.rotation * Vector3.up);
        }

        public void Flagging(Tag_GP target)
        {
            gp = target;
            isOpen = true;
            cam = Camera.main.transform;
            targetEntity = target.target;
            rect = GetComponent<RectTransform>();
            originPivot = rect.pivot;
            originSize = rect.sizeDelta;
            transform.SetParent(target.transform);
            transform.localPosition = Vector3.zero;
            text_Info = GetComponentInChildren<TMP_Text>();
            button_MoveToPoint = GetComponentInChildren<Button>();
            button_MoveToPoint.onClick.RemoveAllListeners();
            button_MoveToPoint.onClick.AddListener(OnClickPoint);
        }

        void OnClickPoint()
        {
            if (isAnim)
                return;

            isAnim = true;
            Managers.uiManager.OnClickEvent(this);
        }

        #region Animation
        bool isOpen;
        public void Close()
        {
            if (currentAnim != null)
                currentAnim.Kill();
            if (!isOpen)
                return;

            isOpen = false;
            var target1 = new Vector2(rect.sizeDelta.x * axisScale, rect.sizeDelta.y);
            currentAnim = DOTween.Sequence()
                .Append(rect.DOSizeDelta(target1, xSpeed))
                .Append(rect.DOSizeDelta(Vector2.zero, ySpeed))
                .OnComplete(() => { isAnim = false; });
        }
        public void Open()
        {
            if (currentAnim != null)
                currentAnim.Kill();
            if (isOpen)
                return;
            isOpen = true;
            var target1 = new Vector2(rect.sizeDelta.x * axisScale, originSize.y);
            currentAnim = DOTween.Sequence()
                .Append(rect.DOSizeDelta(target1, ySpeed))
                .Append(rect.DOSizeDelta(originSize, xSpeed))
                .OnComplete(() => { isAnim = false; });
        }
        #endregion
    }
}