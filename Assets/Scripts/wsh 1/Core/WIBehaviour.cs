using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using WSH.Util;

namespace WSH.UI
{
    public class WIBehaviour : MonoBehaviour
    {
        public T GetCanvas<T>() where T : CanvasBase
        {
            return transform.GetCanvas<T>();
        }
        public bool GetPanel<T>(out T result) where T : PanelBase
        {
            return transform.GetPanel(out result);
        }
        public bool GetPanels<T>(out List<T> result) where T : PanelBase
        {
            return transform.GetPanels(out result);
        }
        public bool GetUIElement<T>(string targetName, out T result) where T : UIBehaviour
        {
            return transform.GetUIElement(targetName, out result);
        }
        public bool GetUIElements<T>(out T[] result) where T : UIBase
        {
            return transform.GetUIElements(out result);
        }

        protected virtual void OnEnable() { }
        public virtual void Active() { }
        public virtual void Deactive() { }
        public virtual void Initialize() { }
        protected virtual void OnDisable() { }

        public virtual void RewindAnimation(WIBehaviour animator)
        {
            animator.GetComponent<UISimpleAnimator>().Rewind();
        }
        public virtual void PlayAnimation(WIBehaviour animator)
        {
            animator.GetComponent<UISimpleAnimator>().Play();
        }
        public virtual void PlayAnimation(UIBehaviour ui)
        {
            ui.GetComponent<UISimpleAnimator>().Play();
        }
        public virtual void RewindAnimation(UIBehaviour ui)
        {
            ui.GetComponent<UISimpleAnimator>().Rewind();
        }
        public virtual void RewindAnimation()
        {
            GetComponent<UISimpleAnimator>().Rewind();
        }
        public virtual void PlayAnimation()
        {
            GetComponent<UISimpleAnimator>().Play();
        }
    }
}
