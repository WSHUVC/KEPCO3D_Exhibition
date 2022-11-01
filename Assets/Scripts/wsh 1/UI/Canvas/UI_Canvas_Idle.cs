using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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
        public Button button_IdleOff;
        public float inputBlockWatingTime = 0f;
        public GameObject Fader; 
        [SerializeField] float timer = 0f;
        public PlayableDirector timeLines;
        public PlayableDirector simulationSequence;
        public PlayableDirector electricSequence;
        UIManager um;
        GameObject lights;
        Image image_GradiantBackground;
        TextMeshProUGUI text_Welcome;

        public override void Initialize()
        {
            um = FindObjectOfType<UIManager>();
            GetUIElement("Button_IdleOff", out button_IdleOff);
            GetUIElement("Image_GradiantBackground",out image_GradiantBackground);
            GetUIElement("Text_Welcome", out text_Welcome);
            Fader = GameObject.Find("Fader");
            button_IdleOff.onClick.AddListener(IdleOff);
            lights = GameObject.Find("@Lights");

            var sqs = FindObjectsOfType<PlayableDirector>();
            timeLines = sqs.Where(s => s.gameObject.name == "Timelines").First();
            simulationSequence = sqs.Where(s => s.gameObject.name == "SimulationTimeLine").First();
            electricSequence = sqs.Where(s => s.gameObject.name.Equals("ElectricSequence")).First();

            electricSequence.gameObject.SetActive(false);
            simulationSequence.gameObject.SetActive(false);

            OnClickedPlayOnButton();
        }
        public enum SquenceType
        {
            Normal,
            Simulation,
            Electric,
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
                case SquenceType.Electric:
                    currentSequence = electricSequence;
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
            PlayAnimation(text_Welcome);
            OnClickedPlayOffButton();
            StartCoroutine(IdleChanger());
        }
        public void IdleOn()
        {
            Debug.Log($"{name}:IdleOn");
            image_GradiantBackground.enabled = false;
            button_IdleOff.gameObject.SetActive(true);
            um.IdleOn();
            RewindAnimation(text_Welcome);
            GetCanvas<UI_Canvas_Bottom>().Deactive();
            GetCanvas<UI_Canvas_LeftMenu>().Deactive();
            GetCanvas<UI_Canvas_RightMenu>().Deactive();
            OnClickedPlayOnButton();
        }

        public void OnClickedPlayOffButton()
        {
            Fader.SetActive(false);
            lights.SetActive(true);
            timeLines.gameObject.SetActive(false);
            electricSequence.gameObject.SetActive(false);
        }
        private void OnClickedPlayOnButton()
        {
            Fader.SetActive(true);
            lights.SetActive(false);
            SequenceChange(SquenceType.Normal);
        }
    }
}