using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public void Exit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        StartCoroutine(LoadAsync());

    }

    IEnumerator LoadAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        loadingScreen.SetActive(true);
        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            Debug.Log(progress);
            yield return null;
        }
    }


}
