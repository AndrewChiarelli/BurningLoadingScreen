using UnityEngine;

namespace AC
{
    public class SetShaderFloat : MonoBehaviour
    {
        [SerializeField] private Material material;
        [SerializeField] private string shaderVariableName = "_Progress";
        [SerializeField] private Vector2 minMaxLerpValue = new Vector2(0, 1);
        private int progressID;
    
        private void Awake()
        {
            progressID = Shader.PropertyToID(shaderVariableName);
        }
    
        public void SetValue(float value)
        {
            material.SetFloat(progressID, value);
        }
    
        /// <summary>
        /// Lerp a value between min and max by range of 0 to 1
        /// </summary>
        public void SetValueLerp01(float range01)
        {
            material.SetFloat(progressID, Mathf.Lerp(minMaxLerpValue.x, minMaxLerpValue.y, Mathf.Clamp01(range01)));
        }
    }
}