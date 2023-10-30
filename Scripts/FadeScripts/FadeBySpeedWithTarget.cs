using System.Collections;
using UnityEngine;

public class FadeBySpeedWithTarget : FadeControllerWithTarget
{
    private float endValue;
    private float speed;
    private bool isRunning;
    
    public override void FadeIn(float speed)
    {
        StartFade(speed, FADE_IN_VALUE);
    }

    public override void FadeOut(float speed)
    {
        StartFade(speed, FADE_OUT_VALUE);
    }
    
    private void StartFade(float speed, float endValue)
    {
        OnStartEvent?.Invoke();
        this.speed = speed;
        this.endValue = endValue;
        currentTarget = GetCurrentValue();
        
        if (!isRunning)
            StartCoroutine(MoveTowardsTargetCoroutine());
    }

    private IEnumerator MoveTowardsTargetCoroutine()
    {
        isRunning = true;
        
        while (CurrentValueUnComplete())
        {
            if(GetCurrentValue() != currentTarget)
                UpdateValue(Mathf.MoveTowards(GetCurrentValue(), currentTarget, GetDeltaTime() * speed));
            yield return null;
        }
        
        isRunning = false;
        OnCompleteEvent?.Invoke();
    }

    private bool CurrentValueUnComplete()
    {
        return GetCurrentValue() != endValue || GetCurrentValue() != currentTarget;
    }
}
