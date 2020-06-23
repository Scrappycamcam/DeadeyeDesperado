using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMan : MonoBehaviour
{

    public GameObject loadScreen;

    private bool isLoading = false;

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void NextScene(int num)
    {
        if (!isLoading)
        {
            isLoading = true;
            StartCoroutine(LoadScreen(num));
        }
    }
    

    private IEnumerator LoadScreen(int sceneNum)
    {
        loadScreen.SetActive(true);
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneNum);

        while (!async.isDone)
        {
            yield return null;
        }
        
    }

}
