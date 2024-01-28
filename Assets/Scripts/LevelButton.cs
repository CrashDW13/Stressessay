using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public string SceneName;
    public bool LoadTransition;

    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(LoadLevel);
    }

    private void LoadLevel()
    {
        LevelLoader _loader = FindObjectOfType<LevelLoader>();
        if (_loader != null)
        {
            _loader.LoadScene(this);
        }
    }

    
}
