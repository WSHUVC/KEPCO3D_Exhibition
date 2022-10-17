using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WSH.Core.Manager
{
    public class CameraControler : MonoBehaviour
    {
        public enum CameraMode
        {
            Lock,   //이동, 회전 금지
            Targeting, //타겟 오브젝트를 바라본다. 타겟 오브젝트를 중심으로한 구 안에서만 이동 가능.
            Rotating, //이동이 금지되고 회전만 가능하다.
        }

        [SerializeField] CameraMode mode;
        delegate void CameraAction();
        CameraAction action;
        public void ModeChange(CameraMode mode)
        {
            switch (mode)
            {
                case CameraMode.Lock:
                    lockPos = transform.position;
                    lockRot = transform.rotation;
                    action = Action_Lock;
                    break;
                case CameraMode.Targeting:
                    action = Action_Targeting;
                    break;
                case CameraMode.Rotating:
                    action = Action_Rotating;
                    break;
            }
        }

        Vector3 lockPos;
        Quaternion lockRot;
        void Action_Lock()
        {
            transform.position = lockPos;
            transform.rotation = lockRot;
        }

        [SerializeField] Transform target;
        public float targetDistance;    //대상과의 거리.
        void Action_Targeting()
        {
            if(target == null)
            {
                Debug.LogError($"Camera Target is NULL");
                return;
            }
            transform.LookAt(target);
        }

        void Action_Rotating()
        {
        }

        private void LateUpdate()
        {
            action();
        }
    }
}