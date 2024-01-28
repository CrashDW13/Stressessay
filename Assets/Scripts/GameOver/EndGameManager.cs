using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textMeshPro;
    public TextMeshProUGUI TextMeshPro
    {
        get { return _textMeshPro; }
        set { _textMeshPro = value; }
    }

    private static EndGameManager instance;

    private void Start()
    {
        instance = this;
    }

    public static void SetText(string text)
    {
        instance.TextMeshPro.text = text;
    }
}
