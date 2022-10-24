using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
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
        Image image_GradiantBackground;
        [SerializeField]float timer = 0f;
        public float inputBlockWatingTime = 0f;
        public PlayableDirector timeLines;
        GameObject lights;
        public PlayableDirector simulationSequence;

        public override void Initialize()
        {
            um = FindObjectOfType<UIManager>();
            GetUIElement("Button_IdleOff", out button_IdleOff);
            GetUIElement("Image_GradiantBackground",out image_GradiantBackground);

            button_IdleOff.onClick.AddListener(IdleOff);
            lights = GameObject.Find("@Lights");
            var sqs = FindObjectsOfType<PlayableDirector>();
            timeLines = sqs.Where(s => s.gameObject.name == "Timelines").First();
            simulationSequence = sqs.Where(s => s.gameObject.name == "SimulationTimeLine").First();
            simulationSequence.gameObject.SetActive(false);
            OnClickedPlayOnButton();
        }
        public enum SquenceType
        {
            Normal,
            Simulation,
        }
        PlayableDirector currentSequence;
        public void SequenceChange(SquenceType st)
        {
            if (currentSequence != null)
                currentSequence.gameObject.SetActive(false);
            switch (st)
            {
                case SquenceType.Normal:
                    currentSequence = timeLines;
                    break;
                case SquenceType.Simulation:
                    currentSequence = simulationSequence;
                    break;
            }
            currentSequence.gameObject.SetActive(true);
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
            image_GradiantBackground.enabled = true;
            button_IdleOff.gameObject.SetActive(false);
            StartCoroutine(IdleChanger());
            OnClickedPlayOffButton();
        }
        public void IdleOn()
        {
            Debug.Log($"{name}:IdleOn");
            image_GradiantBackground.enabled = false;
            button_IdleOff.gameObject.SetActive(true);
            um.IdleOn();
            OnClickedPlayOnButton();
        }

        public void OnClickedPlayOffButton()
        {
            lights.SetActive(true);
            timeLines.gameObject.SetActive(false);
        }
        private void OnClickedPlayOnButton()
        {
            lights.SetActive(false);
            timeLines.gameObject.SetActive(true);
        }
    }
}