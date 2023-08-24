using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ASyncLoader : MonoBehaviour
{
    [Header("Slider")]
    [SerializeField]  Slider loadingSlider;

    private bool hasLoadingCalled = false;

    private void Update()
    {
        if (!hasLoadingCalled)
        {
            StartCoroutine(LoadLevelASync(1));
            hasLoadingCalled = true;
        }
    }

    IEnumerator LoadLevelASync(int levelToLoad)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);

        while(!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingSlider.value = progressValue;
            yield return null;
        }
    }
}
