using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private AsyncOperation _asyncOperation;
    private bool _isLoaded = false;
    [SerializeField] private int _sceneNumber = 1;
    public void ChangeScene()
    {
        if (_isLoaded)
        {
            _asyncOperation.allowSceneActivation = true;
        }
        else
        {
            SceneManager.LoadScene(_sceneNumber);
        }
    }

    public void ChangeScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    private void Start()
    {
        StartCoroutine(LoadScene(_sceneNumber));
    }
    private IEnumerator LoadScene(int sceneID)
    {
        yield return null;

        _asyncOperation = SceneManager.LoadSceneAsync(sceneID);
        _asyncOperation.allowSceneActivation = false;
       /* while (!_asyncOperation.isDone)
        {

            if (_asyncOperation.progress >= 0.9f)
            {
                _isLoaded = true;
            }

            yield return null;
        }*/
    }
}
