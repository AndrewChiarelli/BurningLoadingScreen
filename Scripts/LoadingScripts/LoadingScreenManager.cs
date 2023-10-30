using AC.Fade;
using UnityEngine;

namespace AC.Loading
{
    public class LoadingScreenManager : MonoBehaviour
    {
        [Header("Loading Canvas Group")]
        [SerializeField] public FadeController loadingScreenFade;
        [SerializeField] private float loadingScreenFadeInTime = 1;
        [Header("Load Scene Async")]
        [SerializeField] private LoadSceneAsync loadSceneAsync;
        [Header("Load Progress Visual")]
        [SerializeField] private FadeControllerWithTarget fadeControllerTarget;
        [SerializeField] private float targetFadeSpeedMultiplier = 0.75f;
        [SerializeField] private Vector2 asyncTargetLerp = new Vector2(1, 0);
        
        private bool asyncComplete;
        private bool targetComplete;
        private bool isLoading;
        private int sceneToLoad;
        
        public void LoadNewScene(int sceneToLoad)
        {
            if (isLoading)
                return;

            ResetValues();
            AddListeners();
            isLoading = true;
            this.sceneToLoad = sceneToLoad;
            loadingScreenFade.FadeIn(loadingScreenFadeInTime);
        }
        
        private void ResetValues()
        {
            fadeControllerTarget.FadeInImmediate();
            loadingScreenFade.FadeOutImmediate();
            gameObject.SetActive(true);
        }
        
        private void AddListeners()
        {
            loadingScreenFade.OnCompleteEvent.AddListener(OnLoadFadeInComplete);
            loadSceneAsync.OnValueChangeEvent.AddListener(OnAsyncProgressChange);
            fadeControllerTarget.OnCompleteEvent.AddListener(OnFadeTargetComplete);
            loadSceneAsync.OnCompleteEvent.AddListener(OnAsyncLoadComplete);
        }
        
        private void OnLoadFadeInComplete()
        {
            fadeControllerTarget.FadeOut(targetFadeSpeedMultiplier);
            loadSceneAsync.StartSceneLoad(sceneToLoad, false);
        }
        
        private void OnAsyncProgressChange(float newValue)
        {
            fadeControllerTarget.SetTarget(Mathf.Lerp(asyncTargetLerp.x, asyncTargetLerp.y, newValue));
        }
        
        private void OnAsyncLoadComplete()
        {
            asyncComplete = true;
            CheckIsComplete(targetComplete);
        }
        
        private void OnFadeTargetComplete()
        {
            targetComplete = true;
            CheckIsComplete(asyncComplete);
        }
        
        private void CheckIsComplete(bool b)
        {
            if (!b)
                return;
            
            loadSceneAsync.AllowSceneActivation();
        }
    }
}
