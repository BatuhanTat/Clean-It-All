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

    private void Start()
    {
        StartCoroutine(LoadLevelASync(PlayerPrefs.GetInt("LastPlayedLevel", 1)));
    }
    //private void Update()
    //{
    //    if (!hasLoadingCalled)
    //    {
    //        StartCoroutine(LoadLevelASync(PlayerPrefs.GetInt("LastPlayedLevel",1)));
    //        hasLoadingCalled = true;
    //    }
    //}

    IEnumerator LoadLevelASync(int levelToLoad)
    {
        yield return new WaitForSeconds(0.02f);
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);

        while(!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingSlider.value = progressValue;
            yield return null;
        }
    }
}
