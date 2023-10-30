using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace AC.Loading
{
    public class LoadSceneAsync : MonoBehaviour
    {
        private AsyncOperation asyncOperation;
        private bool loadOnComplete;
        public UnityEvent OnStartEvent;
        public UnityEvent<float> OnValueChangeEvent;
        public UnityEvent OnCompleteEvent;
    
        public void StartSceneLoad(int scene, bool loadOnComplete)
        {
            this.loadOnComplete = loadOnComplete;
            asyncOperation = SceneManager.LoadSceneAsync(scene);
            asyncOperation.allowSceneActivation = false;
        
            StartCoroutine(AsyncCoroutine());
        }

        private IEnumerator AsyncCoroutine()
        {
            OnStartEvent?.Invoke();
        
            while (GetAsyncComplete())
            {
                OnValueChangeEvent?.Invoke(GetAsyncProgress());
                yield return null;
            }
        
            OnValueChangeEvent?.Invoke(1);
            OnCompleteEvent?.Invoke();
        
            if(loadOnComplete)
                asyncOperation.allowSceneActivation = true;
        }

        private bool GetAsyncComplete()
        {
            return GetAsyncProgress() != 1;
        }

        private float GetAsyncProgress()
        {
            return asyncOperation.progress / 0.9f;
        }
    
        public void AllowSceneActivation()
        {
            asyncOperation.allowSceneActivation = true;
        }
    }
}