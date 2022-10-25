using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace WSH.UI
{
    public class UI_Panel_Weather : PanelBase
    {
        TextMeshProUGUI text_HumidityValue;
        TextMeshProUGUI text_TherometerValue;
        TextMeshProUGUI text_WindSpeedValue;

        public override void Initialize()
        {
            base.Initialize();
            GetUIElement("Text_HumidityValue", out text_HumidityValue);
            GetUIElement("Text_WindSpeedValue", out text_WindSpeedValue);
            GetUIElement("Text_TherometerValue", out text_TherometerValue);

            text_HumidityValue.SetText($"45%");
            text_WindSpeedValue.SetText($"1m/s");
            text_TherometerValue.SetText($"18¡ÆC");
        }

    }
}