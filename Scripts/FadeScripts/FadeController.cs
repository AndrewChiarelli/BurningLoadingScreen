using UnityEngine;
using UnityEngine.Events;

namespace AC.Fade
{
    public abstract class FadeController : MonoBehaviour
    {
        protected float GetCurrentValue() { return currentValue; }

        public const int FADE_OUT_VALUE = 0;
        public const int FADE_IN_VALUE = 1;
    
        public UnityEvent OnStartEvent;
        public UnityEvent<float> OnValueChange01Event;
        public UnityEvent OnCompleteEvent;
        private float currentValue;
    
        protected void UpdateValue(float value)
        {
            currentValue = Mathf.Clamp01(value);
            OnValueChange01Event?.Invoke(value);
        }

        /// <summary>
        /// Fade in from current to 1
        /// </summary>
        public abstract void FadeIn(float speed);
    
        /// <summary>
        /// Fade out from current to 0
        /// </summary>
        public abstract void FadeOut(float speed);
    
        /// <summary>
        /// Set value to 1 immediately
        /// </summary>
        public void FadeInImmediate()
        {
            UpdateValue(FADE_IN_VALUE);
        }

        /// <summary>
        /// Set value to 0 immediately
        /// </summary>
        public void FadeOutImmediate()
        {
            UpdateValue(FADE_OUT_VALUE);
        }
    }
}

