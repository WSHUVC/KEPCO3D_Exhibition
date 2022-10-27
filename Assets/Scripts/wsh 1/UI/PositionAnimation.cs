
using System;
using System.Collections;
using UnityEngine;
using WSH.UI;
using static WSH.Util.EasingFunction;

namespace WSH.UI {
    [Serializable]
    public class PositionAnimation : UIAnimation
    {
        [SerializeField] Vector3 startPosition;
        [SerializeField] Vector3 endPosition;
        Vector3 currentStartPos;
        private float currentMaxProcess;
        RectTransform rect;

        public PositionAnimation(UIAnimator actor) : base(actor)
        {
            rect = actor.GetComponent<RectTransform>();
        }
        public void SaveStart()
        {
            startPosition = rect.localPosition;
        }
        public void SaveEnd()
        {
            endPosition = rect.localPosition;
        }
        float currentSpeed;
        protected override void Ready()
        {
            base.Ready();
            currentStartPos = rect.localPosition;
            currentMaxProcess = Vector3.Distance(currentStartPos, endPosition);
            currentSpeed = AutoSpeed ? currentMaxProcess : speed;
        }
        
        public override void Play()
        {
            Ready();
            normalEasingFunction = GetEasingFunction(normalEasingType);
            anim= actor.StartCoroutine(AnimationPlay());
        }
        IEnumerator AnimationPlay()
        {
            while (animationEndCheck)
            {
                PositionUpdate(normalEasingFunction);
                yield return null;
            }
        }
        void PositionUpdate(Function ef)
        {
            process += currentSpeed* Time.deltaTime / currentMaxProcess;
            var p1 = ef(0f, currentMaxProcess, process);
            var l1 = Vector3.Lerp(currentStartPos, endPosition, p1);
            rect.localPosition = l1;
        }

        public override void Rewind()
        {
            Ready();
            rewindEasingFunction = GetEasingFunction(rewindEasingType);
        }
    }
}