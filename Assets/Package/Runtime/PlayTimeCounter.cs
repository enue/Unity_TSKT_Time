using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TSKT
{
    public class PlayTimeCounter
    {
        readonly float initializedTime;
        System.TimeSpan fixedDuration;

        public PlayTimeCounter()
        {
            initializedTime = Time.unscaledTime;
            fixedDuration = System.TimeSpan.Zero;
        }

        public PlayTimeCounter(System.TimeSpan duration)
        {
            initializedTime = Time.unscaledTime;
            fixedDuration = duration;
        }

        public System.TimeSpan Duration
        {
            get => System.TimeSpan.FromSeconds(Time.unscaledTime - initializedTime) + fixedDuration;
            set
            {
                fixedDuration = value - System.TimeSpan.FromSeconds(Time.unscaledTime - initializedTime);
            }
        }

        public long ToTicks() => Duration.Ticks;
        static public PlayTimeCounter FromTicks(long ticks)
        {
            return new PlayTimeCounter(new System.TimeSpan(ticks));
        }
    }
}
