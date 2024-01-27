using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypingManager : MonoBehaviour
{
    [Header("Text")]
    [SerializeField]
    private TextAsset _textAsset;
    [SerializeField]
    private string _text;
    [SerializeField]
    private TextMeshProUGUI _textMeshPro;
    private char[] _textChars;
    private string _vanillaText;

    [Header("Randomness")]
    [Range(0f, 100f)]
    [SerializeField] private float _ignoreInputOdds = 0; 

    private int _currentIndex = 0;

    private void Start()
    {
        if (_textAsset != null)
        {
            _textMeshPro.text = _textAsset.text;
        }

        _textChars = _textMeshPro.text.ToCharArray();
        _vanillaText = _textMeshPro.text;
    }

    private void Update()
    {

        CheckForKeyboardInput();

        if (Input.inputString .Length > 0)
        {
            Debug.Log(Input.inputString);
        }

        //Debug.Log(_currentIndex);
    }

    private void CheckForKeyboardInput()
    {
        float _ignore = UnityEngine.Random.Range(0, 100);
        if (_ignore < _ignoreInputOdds)
        {
            //play ignored typing sound effect
            return;
        }

        string _inputString = Input.inputString;
        if (_inputString.Length > 0)
        {
            char _inputChar = _inputString.ToCharArray()[0]; //prevents failure when pressing 2 keys in the same frame
            if (_inputChar == _textChars[_currentIndex])
            {
                _currentIndex++;
                UpdateTextAppearance();
            }

            else
            {
                Debug.Log(_inputChar + " " + _textChars[_currentIndex]);

                return;
            }
        }
    }

    private void UpdateTextAppearance()
    {
        string _highlightedText = _vanillaText;
        string _markdownHighlight = "<mark=#000000>";
        _highlightedText = _highlightedText.Insert(_currentIndex, _markdownHighlight);
        _highlightedText = _highlightedText.Insert(_currentIndex + 1 + _markdownHighlight.Length, "</mark>");
        Debug.Log(_highlightedText);
        _textMeshPro.text = _highlightedText;
    }




}
