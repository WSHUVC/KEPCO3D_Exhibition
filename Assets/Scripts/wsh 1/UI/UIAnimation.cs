using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static WSH.Util.EasingFunction;

namespace WSH.UI
{
    public class UIAnimation
    {
        public string animationGroup;
        public float speed;
        public EaseType normalEasingType;
        public EaseType rewindEasingType;
        public bool AutoSpeed;
        [SerializeField] protected bool isPlay;
        protected UIAnimator actor;
        protected float process;
        protected Coroutine anim;
        protected Function normalEasingFunction;
        protected Function rewindEasingFunction;

        protected bool animationEndCheck => process < 1f;//1미만이면 아직 재생중

        public UIAnimation(UIAnimator actor)
        {
            this.actor = actor;
        }

        protected virtual void Ready()
        {
            isPlay = true;
            process = 0f;
        }

        public virtual void Play() { }
        public virtual void Rewind() { }
        public virtual void Stop()
        {
            if (anim != null)
                actor.StopCoroutine(anim);
        }
    }
}
