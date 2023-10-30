using UnityEngine;

namespace AC
{
    public class SetCanvasGroupAlpha : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
    
        public void SetValue(float value)
        {
            canvasGroup.alpha = Mathf.Clamp01(value);
        }
    }
}

