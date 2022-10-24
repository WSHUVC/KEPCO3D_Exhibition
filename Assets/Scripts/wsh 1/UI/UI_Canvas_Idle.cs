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

        [SerializeField]float timer = 0f;
        public float inputBlockWatingTime = 0f;
        public override void Initialize()
        {
            um = FindObjectOfType<UIManager>();
            GetUIElement("Button_IdleOff", out button_IdleOff);
            button_IdleOff.onClick.AddListener(IdleOff);
            lights = GameObject.Find("@Lights");
            timeLines = GameObject.Find("Timelines");
            OnClickedPlayOnButton();
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
        public void IdleOff()
        {
            Debug.Log($"{name}:IdleOff");
            button_IdleOff.gameObject.SetActive(false);
            StartCoroutine(IdleChanger());
            OnClickedPlayOffButton();
        }
        public void IdleOn()
        {
            Debug.Log($"{name}:IdleOn");
            button_IdleOff.gameObject.SetActive(true);
            um.IdleOn();
            OnClickedPlayOnButton();
        }
        
        private GameObject fader;
        private GameObject lights;
        private GameObject timeLines;
        public void OnClickedPlayOffButton()
        {
            lights.SetActive(true);
            //fader.SetActive(false);
            timeLines.SetActive(false);
        }
        private void OnClickedPlayOnButton()
        {
            lights.SetActive(false);
            //fader.SetActive(true);
            timeLines.SetActive(true);
        }
    }
}