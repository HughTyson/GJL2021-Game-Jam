using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CustomeSceneManager : MonoBehaviour
{

    //The names of all the loadable scenes
    [System.Serializable]
    public enum SceneName { None = 0, MainMenu = 1, MainLevel = 2 }

    //The data stored about each loadable scene
    [System.Serializable]
    public struct SceneData
    {
        public SceneName _sceneName; //name of the scene
        public int _sceneIndex; //build index
    }

    //List of all loadable scene data
    [SerializeField] List<SceneData> _allScenes = new List<SceneData>();

    //Loading screen to be instantiated when loading
    [SerializeField] GameObject _loadingScreenPrefab = default;

    private TMP_Text _loadingTxt = default; //The text displayed on the loading screen
    private string _elipses = ""; //The elipses that will be placed at the end of the loading screen text

    public SceneName GetCurrentSceneName()
    {
        for (int i = 0; i < _allScenes.Count; i++)
        {
            if (_allScenes[i]._sceneIndex == SceneManager.GetActiveScene().buildIndex)
                return _allScenes[i]._sceneName;
        }
        Debug.LogWarning("Current Scene Not Found");
        return SceneName.None;
    }

    public int GetCurrentSceneIndex()
    {
        return _allScenes.Find(x => x._sceneIndex == SceneManager.GetActiveScene().buildIndex)._sceneIndex;
    }

    public void LoadScene(SceneName _scene)
    {
        int targetBuildIndex = -1;
        targetBuildIndex = _allScenes.Find(x => x._sceneName == _scene)._sceneIndex;

        if (targetBuildIndex >= 0)
        {
            StopAllCoroutines();
            StartCoroutine(Loading(targetBuildIndex));
        }
    }

    public void LoadScene(int _scene)
    {
        if (_scene >= 0)
        {
            StopAllCoroutines();
            StartCoroutine(Loading(_scene));
        }
    }

    IEnumerator Loading(int buildIndex)
    {
        GameObject loadingScreen = Instantiate(_loadingScreenPrefab);
        DontDestroyOnLoad(loadingScreen);

        yield return new WaitForSeconds(1.5f);

        PauseManager.instance._isPaused = true;

        AsyncOperation operation = SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Single);

        while (!operation.isDone)
        {
            //Any desired functionality for while the loading is taking place goes here
            //E.g. updating a percentage bar
            if (_elipses.Length == 3)
            {
                _elipses = "";
            }
            else
            {
                _elipses += ".";
            }

            if (_loadingTxt != null)
                _loadingTxt.text = "Loading" + _elipses;

            yield return new WaitForSeconds(0.5f);
        }

        loadingScreen.GetComponent<Animator>().SetTrigger("Close");

        yield return new WaitForSeconds(1.5f);

        Destroy(loadingScreen);
        PauseManager.instance._isPaused = false;
    }
}
