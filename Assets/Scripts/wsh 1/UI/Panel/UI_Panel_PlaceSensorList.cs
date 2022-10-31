using Debug = WSH.Util.Debug;
using UnityEngine;
using UnityEngine.UI;

namespace WSH.UI
{
    public class UI_Panel_PlaceSensorList : PanelBase
    {
        public UI_Button_PlaceSensor[] button_PlaceSensors;
        public PlaceGroup myPlaceGroup;
        public Tag_Place myPlace;
        internal UI_Button_PlaceSensor currentSensor;
        public void SetPlace(Tag_Place place)
        {
            button_PlaceSensors = GetComponentsInChildren<UI_Button_PlaceSensor>();
            foreach (var b in button_PlaceSensors)
            {
                b.gameObject.SetActive(false);
            }
            myPlace = place;
            int placeSensorCount = myPlace.tagMembers.Count;
            for(int i = 0; i < placeSensorCount; ++i)
            {
                if (i >= button_PlaceSensors.Length)
                    return;
                button_PlaceSensors[i].SetPlace(this, place, myPlace.tagMembers[i].index);
                button_PlaceSensors[i].gameObject.SetActive(true);
            }
        }
    }
}