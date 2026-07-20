using System;
using System.Collections;
using Input;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderManager : MonoBehaviour
{
    public static SceneLoaderManager Instance;
    
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private TMP_Text loadingText;
    [SerializeField] private float playerEnableActionMapDelay = 1;
    [SerializeField] private ShaderVariantCollection shaderVariantCollection;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }

    public void SceneLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
    public void AddSceneToLoad(string[] sceneName)
    {
        if (loadingScreen) loadingScreen.SetActive(true);
        StartCoroutine(LoadSceneAsyncAdditive(sceneName));
    }

    public void AddScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName,  LoadSceneMode.Additive);
    }
    
    public IEnumerator LoadSceneAsyncAdditive(string[] sceneName)
    {
        if (loadingText)
        {
            StartCoroutine(LoadingAnimated());
        }

        if (shaderVariantCollection != null)
        {
            while (!shaderVariantCollection.isWarmedUp)
            {
                shaderVariantCollection.WarmUp();
                yield return null;
            }
        }


        foreach (string scene in sceneName)
        {
            if (SceneManager.GetSceneByName(scene).isLoaded) continue;
            AsyncOperation async = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

            if (async == null) continue;
            async.allowSceneActivation = false;

            while (async.progress < 0.9f)
            {
                yield return null;
            }

            yield return new WaitForSeconds(0.2f);
            async.allowSceneActivation = true;

            while (!async.isDone)
            {
                yield return null;
            }
        }


        loadingText.text = "Loading Complete.";
        loadingScreen.SetActive(false);
        yield return new WaitForSeconds(playerEnableActionMapDelay);
        InputManager.ToggleActionMap(InputManager.InputAction.Player);
        StopAllCoroutines();
    }

    private IEnumerator LoadingAnimated()
    {
        string baseText = "Loading";
        float typeSpeed = 0.2f;
        float dotSpeed = 0.3f;
        float deleteSpeed = 0.1f;

        while (true)
        {
            for (int i = 1; i <= baseText.Length; i++)
            {
                loadingText.text = baseText.Substring(0, i);
                yield return new WaitForSeconds(typeSpeed);
            }

            for (int i = 1; i <= 3; i++)
            {
                loadingText.text = baseText + new string('.', i);
                yield return new WaitForSeconds(dotSpeed);
            }

            for (int i = 2; i >= 0; i--)
            {
                loadingText.text = baseText + new string('.', i);
                yield return new WaitForSeconds(deleteSpeed);
            }

            for (int i = baseText.Length; i > 0; i--)
            {
                loadingText.text = baseText.Substring(0, i);
                yield return new WaitForSeconds(deleteSpeed);
            }

            loadingText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
