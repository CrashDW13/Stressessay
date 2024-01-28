using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private Animator _transition;
    [SerializeField]
    private float _transitionTime = 1f;
    public LevelLoader _instance;


    private void Start()
    {
        _instance = this;
    }

    public void LoadScene(string _sceneName, bool _playTransition)
    {
        if (_playTransition)
        {
            StartCoroutine(TransitionLoadScene(_sceneName));
        }

        else
        {
            SceneManager.LoadScene(_sceneName);
        }
    }

    public void LoadScene(LevelButton _button)
    {
        bool _playTransition = _button.LoadTransition;
        string _sceneName = _button.SceneName;

        if (_playTransition)
        {
            StartCoroutine(TransitionLoadScene(_sceneName));
        }

        else
        {
            SceneManager.LoadScene(_sceneName);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    private IEnumerator TransitionLoadScene(string _sceneName)
    {
        _transition.SetTrigger("StartTransition");

        yield return new WaitForSeconds(_transitionTime);

        SceneManager.LoadScene( _sceneName );

    }
}
