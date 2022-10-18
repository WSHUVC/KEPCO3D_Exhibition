using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WSH.Core.Manager
{
    public class CameraControler : MonoBehaviour
    {
        public enum CameraMode
        {
            Lock,   //�̵�, ȸ�� ����
            Targeting, //Ÿ�� ������Ʈ�� �ٶ󺻴�. Ÿ�� ������Ʈ�� �߽������� �� �ȿ����� �̵� ����.
            Rotating, //�̵��� �����ǰ� ȸ���� �����ϴ�.
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
        public float targetDistance;    //������ �Ÿ�.
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