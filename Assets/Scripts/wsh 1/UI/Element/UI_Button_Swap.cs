using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WSH.UI
{

    public class UI_Button_Swap : UIBase
    {
        public List<GameObject> swaptargets = new List<GameObject>();
        int currentIndex;
        public override void Initialize()
        {
            base.Initialize();
            Button thisbutton;

            GetUIElement(this.name, out thisbutton);
            //thisbutton.onClick.AddListener(Onclick_Swap);

            foreach(GameObject tmpGO in swaptargets)
            {
                tmpGO.SetActive(false);
            }
            swaptargets[0].SetActive(true);

        }

        public void Onclick_Swap()
        {
            swaptargets[currentIndex].SetActive(false);
            Debug.Log(currentIndex);
            currentIndex++;
            currentIndex %= swaptargets.Count;

            swaptargets[currentIndex].SetActive(true);


        }
    }
}

