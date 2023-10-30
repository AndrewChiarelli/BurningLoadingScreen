using AC.Fade;
using UnityEngine;

public class FadeByTime : FadeController
{
    [SerializeField] private LeanTweenType easeType;
    private int id = -1;
    
    public override void FadeIn(float speed)
    {
        StartTween(FADE_IN_VALUE, speed, 0);
    }

    public override void FadeOut(float speed)
    {
        StartTween(FADE_OUT_VALUE, 0, speed);
    }
    
    private void StartTween(float targetValue, float minTime, float maxTime)
    {
        if(LeanTween.isTweening(id))
            LeanTween.cancel(id);
        
        OnStartEvent?.Invoke();
        
        LeanTween.value(gameObject, GetCurrentValue(), targetValue, Mathf.Lerp(minTime, maxTime, GetCurrentValue()))
            .setEase(easeType)
            .setOnUpdate(UpdateValue)
            .setOnComplete(OnCompleteEvent.Invoke);
    }
}
