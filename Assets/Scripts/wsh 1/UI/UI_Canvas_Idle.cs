using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WSH.Core.Manager;
using WSH.Util;
using Debug = WSH.Util.Debug;

namespace WSH.UI
{
    public class UI_Canvas_Idle : CanvasBase
    {
        UIManager um;
        public Button button_IdleOff;
        public override void Initialize()
        {
            base.Initialize();
            um = FindObjectOfType<UIManager>();
            TryGetUIElement("Button_IdleOff", out button_IdleOff);
            button_IdleOff.onClick.AddListener(IdleOff);
        }

        [SerializeField]float timer = 0f;
        public float inputBlockWatingTime = 0f;
        public void IdleOff()
        {
            Debug.Log($"{name}:IdleOff");
            button_IdleOff.gameObject.SetActive(false);
            StartCoroutine(IdleChanger());
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
                timer = 0f;
        }

        IEnumerator IdleChanger()
        {
            timer = 0f;
            while (timer < inputBlockWatingTime)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            IdleOn();
        }

        public void IdleOn()
        {
            Debug.Log($"{name}:IdleOn");
            button_IdleOff.gameObject.SetActive(true);
            um.IdleOn();
        }
    }
}