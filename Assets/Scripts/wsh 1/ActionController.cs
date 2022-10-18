using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using WSH.UI;
using WSH.Util;

namespace WSH.Core.Manager
{
    public class ActionController : MonoBehaviour
    {
        public static ActionController instance
        {
            get;
            private set;
        }

        [Header("Camera Option")]
        public GameObjectLender<Camera> camLender;
        public float fovMin;
        public float fovMax;
        public float camLerpStrength;
        public float camControlSpeed;
        public bool closeUp;
        public float zoomInSpeed;

        [Header("Map Option")]
        public GameObjectLender<Map> mapLender;
        public float dragDelay;
        public float mapControlSpeed;

        [Header("ETC")]
        public UI_Flag currentFlag;
        public GameObject currentTarget;
        public bool drawGizmo;
        public float rewindSpeed;
        List<Vector3> line = new List<Vector3>();
        float actionProcess_Cam = 0f;
        float inputTimer;
        float actionProcess_Map;
        public enum Anim
        {
            ZoomIn,
            RewindCam,
            RewindMap,
            RewindEnd,
        }
        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }
            instance = this;
            var map = FindObjectOfType<Map>();
            mapLender = new GameObjectLender<Map>(map);
            Camera.main.transform.LookAt(mapLender.originPos);
            camLender = new GameObjectLender<Camera>(Camera.main);
            camLender.Lental(out var cam);
            camLender.Repay(cam);
        }
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Rewind();
            }

            if (closeUp)
                CamRotateControl();
            else
                MapRotateControl();

            CameraFOVControl();
        }
        public void Rewind()
        {
            if (camLender.IsNewProduct && mapLender.IsNewProduct)
            {
                Debug.Log($"Is Already Init");
                return;
            }
            Debug.Log($"Rewinding");
            PlayAnim(Anim.RewindMap);
            PlayAnim(Anim.RewindCam);
            PlayAnim(Anim.RewindEnd);
        }
        public void OnClickFlag(UI_Flag uI_Flag)
        {
            Debug.Log($"OnClickFlag : {uI_Flag.gameObject}");
            currentFlag = uI_Flag;
            currentTarget = currentFlag.targetEntity.gameObject;
            PlayAnim(Anim.ZoomIn);
        }
        #region Animation
        public void PlayAnim(Anim anim)
        {
            inputTimer = 0f;
            switch (anim)
            {
                case Anim.ZoomIn:
                    closeUp = true;
                    Debug.Log($"MoveToFlag : {currentFlag.gameObject}");
                    Managers.uiManager.CloseFlags();
                    mapLender.Repack();
                    StartCoroutine(Anim_MoveToFlag());
                    break;
                case Anim.RewindCam:
                    closeUp = false;
                    StartCoroutine(Anim_RewindCam());
                    break;
                case Anim.RewindMap:
                    StartCoroutine(Anim_RewindMap());
                    break;
                case Anim.RewindEnd:
                    StartCoroutine(Anim_RewindEnd());
                    break;
            }
        }
        IEnumerator Anim_RewindEnd()
        {
            while (camLender.InUse || mapLender.InUse)
            {
                yield return null;
            }
            Managers.uiManager.OpenFlags();
        }

        IEnumerator Anim_RewindCam()
        {
            if (currentFlag == null)
                yield break;

            if (camLender.Lental(out var cam))
            {
                var cCamPos = cam.transform.position;
                var camLerpHandle = cam.transform.position + (camLender.originPos - cam.transform.position) * 0.5f;
                camLerpHandle.y = cam.transform.position.y;
                actionProcess_Cam = 0.1f;

                while (actionProcess_Cam < 1f)
                {
                    actionProcess_Cam += rewindSpeed * Time.deltaTime;
                    var camLerp1 = Vector3.Lerp(cCamPos, camLerpHandle, actionProcess_Cam);
                    var camLerp2 = Vector3.Lerp(camLerpHandle, camLender.originPos, actionProcess_Cam);
                    var camLerpResult = Vector3.Lerp(camLerp1, camLerp2, actionProcess_Cam);
                    cam.transform.position = camLerpResult;
                    var lookPos = Vector3.Lerp(currentTarget.transform.position, mapLender.originPos, actionProcess_Cam);
                    cam.transform.LookAt(lookPos);
                    yield return null;
                }
                actionProcess_Cam = 0f;
                camLender.Repack();
                camLender.Repay(cam);
            }
        }

        IEnumerator Anim_RewindMap()
        {
            if (mapLender.Lental(out var map))
            {
                var cMapPos = map.transform.position;
                var cMapRot = map.transform.rotation;
                actionProcess_Map = 0f;

                while (actionProcess_Map < 1f)
                {
                    actionProcess_Map += rewindSpeed * Time.deltaTime;
                    map.transform.position = Vector3.Lerp(cMapPos, mapLender.originPos, actionProcess_Map);
                    map.transform.rotation = Quaternion.Lerp(cMapRot, mapLender.originRot, actionProcess_Map);
                    yield return null;
                }
                mapLender.Repack();
                actionProcess_Map = 0f;
                mapLender.Repay(map);
            }
        }

        public bool fixedRotate;
        public float targetDistance;
        IEnumerator Anim_MoveToFlag()
        {
            if (camLender.Lental(out var cam))
            {
                var start = cam.transform.position;
                var end = currentTarget.transform.position;

                Vector3 center;
                if (fixedRotate)
                    center = end + (currentTarget.transform.forward * targetDistance);
                else
                    center = start + (end - start) * 0.5f;

                float process= 0f;
                float maxProcess = Vector3.Distance(start, end);
                while (process < 1f)
                {
                    actionProcess_Cam += zoomInSpeed * Time.deltaTime;
                    process = actionProcess_Cam / maxProcess;
                    var lerp1 = Vector3.Lerp(start, center, process);
                    var lerp2 = Vector3.Lerp(center, end, process);
                    var pos = Vector3.Lerp(lerp1, lerp2, process);
                    cam.transform.position = pos;
                    cam.transform.LookAt(lerp2);
                    yield return null;
                }
                actionProcess_Cam = 0f;
                camLender.Repay(cam);
            }
        }
        #endregion
        #region InputAction
        void MapRotateControl()
        {
            if (Input.GetMouseButton(0))
            {
                if (inputTimer >= dragDelay)
                {
                    if (mapLender.Lental(out var map))
                    {
                        map.transform.Rotate(0f, -Input.GetAxis("Mouse X") * mapControlSpeed, 0f, Space.World);
                        map.transform.Rotate(-Input.GetAxis("Mouse Y") * mapControlSpeed, 0f, 0f);
                        mapLender.Repay(map);
                    }
                }
                else
                    inputTimer += Time.deltaTime;
            }
            if (Input.GetMouseButtonUp(0))
            {
                inputTimer = 0f;
            }
        }
        void CamRotateControl()
        {
            if (Input.GetMouseButton(0))
            {
                if (inputTimer >= dragDelay)
                {
                    if (camLender.Lental(out var cam))
                    {
                        cam.transform.Rotate(0f, -Input.GetAxis("Mouse X") * camControlSpeed, 0f, Space.World);
                        cam.transform.Rotate(-Input.GetAxis("Mouse Y") * camControlSpeed, 0f, 0f);
                        camLender.Repay(cam);
                    }
                }
                else
                    inputTimer += Time.deltaTime;
            }
            if (Input.GetMouseButtonUp(0))
            {
                inputTimer = 0f;
            }
        }
        void CameraFOVControl()
        {
            if (camLender.Lental(out var cam))
            {
                var fov = cam.fieldOfView;
                fov -= Input.mouseScrollDelta.y;
                if (fov < fovMin)
                    fov = fovMin;
                if (fov > fovMax)
                    fov = fovMax;
                cam.fieldOfView = fov;
                camLender.Repay(cam);
            }
        }
        #endregion
        #region util
        private void OnDrawGizmos()
        {
            if (!drawGizmo)
                return;

            for (int i = 0; i < line.Count - 1; ++i)
            {
                UnityEngine.Debug.DrawLine(line[i], line[i + 1]);
            }
        }
        public Vector3 Lerp(Vector3[] points, float process)
        {
            Vector3[] lerpPoints;
            Vector3[] prevLerps = points;
            while (prevLerps.Length > 1)
            {
                lerpPoints = new Vector3[prevLerps.Length - 1];
                for (int i = 0; i < lerpPoints.Length; ++i)
                {
                    lerpPoints[i] = Vector3.Lerp(prevLerps[i], prevLerps[i + 1], process);
                }
                prevLerps = lerpPoints;
            }
            return prevLerps[0];
        }
        #endregion
    }
}