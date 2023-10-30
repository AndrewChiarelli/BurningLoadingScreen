using AC.Fade;
using UnityEngine;

public abstract class FadeControllerWithTarget : FadeController
{
    [SerializeField] private bool useUnscaledTime = true;
    protected float currentTarget;
    
    /// <summary>
    /// A target between 0 and 1
    /// </summary>
    public void SetTarget(float target)
    {
        currentTarget = Mathf.Clamp01(target);
    }
    
    protected float GetDeltaTime()
    {
        return useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
    }
}
