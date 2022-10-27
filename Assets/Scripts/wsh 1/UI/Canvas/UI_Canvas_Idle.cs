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
    public enum Day
    {
        Night,
        AfterNoon,
    }

    public class UI_Canvas_Idle : CanvasBase
    {

        private Day day = Day.AfterNoon;

        public GameObject nightSkyFog;
        public GameObject nightGlobal;
        public GameObject nightSun;
        // 
        public GameObject SkyFog;
        public GameObject SkyGlobal;
        public GameObject MorningSun;


        // 
        public GameObject fader;

        float sunAngle = -30.0f ;

        public Day Today
        {
            get { return day; }
            set 
            { day = value;
                switch (day)
                {
                    case Day.Night:
                        nightGlobal.SetActive(true);
                        nightSkyFog.SetActive(true);
                        nightSun.SetActive(true);
                        SkyFog.SetActive(false);
                        SkyGlobal.SetActive(false);
                        MorningSun.SetActive(false);
                        break;
                    case Day.AfterNoon:
                        nightGlobal.SetActive(false);
                        nightSkyFog.SetActive(false);
                        nightSun.SetActive(false);
                        SkyFog.SetActive(true);
                        SkyGlobal.SetActive(true);
                        MorningSun.SetActive(true);
                        break;
                    default:
                        break;
                }
            }
        }


        UIManager um;
        public Button button_IdleOff;
        Image image_GradiantBackground;
        GameObject lights;
        [SerializeField]float timer = 0f;
        public float inputBlockWatingTime = 0f;

        PlayableDirector timeLines;
        PlayableDirector simulationSequence;

        TextMeshProUGUI text_Welcome;

        public override void Initialize()
        {
         
            Today = Day.AfterNoon;
  

            um = FindObjectOfType<UIManager>();
            GetUIElement("Button_IdleOff", out button_IdleOff);
            GetUIElement("Image_GradiantBackground",out image_GradiantBackground);
            GetUIElement("Text_Welcome", out text_Welcome);

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

            if (Input.GetMouseButtonUp(1))
                Today = (Today == Day.AfterNoon) ? Day.Night : Day.AfterNoon;

            if(Input.GetKeyDown("u"))
                movingSunAxisXRotation();

       
        }

        private void LateUpdate()
        {
           sunAngle += Time.deltaTime;
            if (sunAngle % 1 == 0)
                Debug.Log($"{sunAngle}");

            if (sunAngle > 160) sunAngle = -30;
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
            StartCoroutine(IdleChanger());
            OnClickedPlayOffButton();
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
            lights.SetActive(true);
            fader.SetActive(false);
            timeLines.gameObject.SetActive(false);
      
            
        }
        private void OnClickedPlayOnButton()
        {
            lights.SetActive(false);
            fader.SetActive(true);
            SequenceChange(SquenceType.Normal);
            
        }

        private void movingSunAxisXRotation()
        {
            Transform morningSunTrnasfrom = MorningSun.GetComponent<Transform>();

            // Set to Sunrise rotation
            if (((int)morningSunTrnasfrom.rotation.x) > 160)
            {
                sunAngle = 20f;
                morningSunTrnasfrom.rotation = Quaternion.Euler(new Vector3(sunAngle, 0.0f, 0.0f)); //  setsunaxis to sunrise axis


            } else
            {
                morningSunTrnasfrom.rotation = Quaternion.Slerp(morningSunTrnasfrom.rotation, Quaternion.Euler(new Vector3(sunAngle, 0.0f, 0.0f)), 1f);
                sunAngle += 30f;
                Debug.Log($"{sunAngle}");
            }

        }
    }
    
}