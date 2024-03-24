#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TSKT
{
    public readonly struct TimeScaler : System.IDisposable
    {
        static Dictionary<int, float>? scales;
        static Dictionary<int, float> Scales => scales ??= new Dictionary<int, float>();

        static int nextId = 1;
        readonly int id;

        TimeScaler(int id)
        {
            this.id = id;
        }

        public static TimeScaler Create()
        {
            var result = new TimeScaler(nextId);
            ++nextId;

            return result;
        }

        public static TimeScaler Create(float value)
        {
            var result = Create();
            result.SetScale(value);
            return result;
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

        public float Value
        {
            get
            {
                if (scales ==  null)
                {
                    return 1f;
                }
                if (scales.TryGetValue(id, out var value))
                {
                    return value;
                }
                return 1f;
            }
            set => SetScale(value);
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
                foreach (var it in scales)
                {
                    t *= it.Value;
                }
                return t;
            }
        }
    }
}