#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TSKT
{
    public class TimeScaler : System.IDisposable
    {
        static Dictionary<int, float>? scales;
        static Dictionary<int, float> Scales => scales ??= new Dictionary<int, float>();

        static int nextId = 1;
        readonly int id;

        public TimeScaler()
        {
            id = nextId;
            ++nextId;
        }

        public void SetScale(float t)
        {
            if (t == 1f)
            {
                if (!Scales.Remove(id))
                {
                    return;
                }
            }
            else
            {
                Scales[id] = t;
            }

            Time.timeScale = TotalScale;
        }

        public void Dispose()
        {
            SetScale(1f);
        }

        float TotalScale
        {
            get
            {
                if (scales == null)
                {
                    return 1f;
                }
                float t = 1f;
                foreach (var it in Scales)
                {
                    t *= it.Value;
                }
                return t;
            }
        }
    }
}