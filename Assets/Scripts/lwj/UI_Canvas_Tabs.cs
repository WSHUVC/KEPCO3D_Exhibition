using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using WSH.Core.Manager;
using Debug = WSH.Util.Debug;
using System.Linq;

namespace WSH.UI
{
    public enum TabButtonElements
    {
        Button_AllInfo,
        Button_OperationElements,
        Button_MaintenenceHistory,
        Button_DiagnosisHistory,
        Button_PatrolHistory,
        Button_MulfunctionHistory,
        Button_SCADAMeter,
        Button_SOMASLoad,
        Button_RepairRequest,
        Button_Memo,
        Button_OffHistory,
        Button_BasicInfoAndBlueprint,
    }

    public enum TabPanelElements
    {
        Panel_AllInfo,
        Panel_OperationElements,
        Panel_MaintenenceHistory,
        Panel_DiagnosisHistory,
        Panel_PatrolHistory,
        Panel_MulfunctionHistory,
        Panel_SCADAMeter,
        Panel_SOMASLoad,
        Panel_RepairRequest,
        Panel_Memo,
        Panel_OffHistory,
        Panel_BasicInfoAndBlueprint,
    }

    public class UI_Canvas_Tabs : CanvasBase
    {
        List<Button> buttons;
        List<UI_Panel_InfoTab> tabs;
        
        Button currentSelectedButton;
        Button prevSelectedButton;
        Button closeButton;

        UI_Panel_InfoTab currentSelectedPanel;
        UI_Panel_InfoTab prevSelectedPanel;
        public override void Initialize()
        {
            buttons = new List<Button>();
            tabs = new List<UI_Panel_InfoTab>();

            Button tmpButton;
            int TabElementsCount = System.Enum.GetValues(typeof(TabButtonElements)).Length;

            for ( int i = 0; i < TabElementsCount; i++)
            {
                GetUIElement(((TabButtonElements)i).ToString(), out tmpButton);
                tmpButton.onClick.AddListener(OnClick_Tabs);
                buttons.Add(tmpButton);
            }
            for (int i = 1; i < TabElementsCount; i++)
            {
                buttons[i].GetComponent<Image>().enabled = false;
            }
            prevSelectedButton = buttons[(int)TabButtonElements.Button_AllInfo];

            int PanelElementsCount = System.Enum.GetValues(typeof(TabPanelElements)).Length;

            //GetPanels<UI_Panel_InfoTab>(out tabs);
            UI_Panel_InfoTab tmpPanel;

            for (int i = 0; i < PanelElementsCount; i++)
            {
                GetUIElement(((TabPanelElements)i).ToString(), out tmpPanel);
                tabs.Add(tmpPanel);
            }

            for ( int i = 1; i < PanelElementsCount; i++)
            {
               tabs[i].gameObject.SetActive(false);
            }
            prevSelectedPanel = tabs[(int)TabPanelElements.Panel_AllInfo];

            GetUIElement("Button_CloseTab", out closeButton);
            closeButton.onClick.AddListener(Onclick_CloseTab);
        }
        public override void Active()
        {
            base.Active();
            gameObject.SetActive(true);
        }

        public override void Deactive()
        {
            base.Deactive();
            gameObject.SetActive(false);
        }
        void OnClick_Tabs()
        {
            string name = EventSystem.current.currentSelectedGameObject.name;
            int selectedIndex = (int)(TabButtonElements)System.Enum.Parse(typeof(TabButtonElements), name);

            Debug.Log(name);

            prevSelectedButton.GetComponent<Image>().enabled = false;
            prevSelectedPanel.gameObject.SetActive(false);

            currentSelectedButton = buttons[selectedIndex];
            currentSelectedButton.GetComponent<Image>().enabled = true;

            currentSelectedPanel = tabs[selectedIndex];
            currentSelectedPanel.gameObject.SetActive(true);

            prevSelectedButton = currentSelectedButton;
            prevSelectedPanel = currentSelectedPanel;
        }

        void Onclick_CloseTab()
        {
            this.gameObject.SetActive(false);
        }
    }
}


