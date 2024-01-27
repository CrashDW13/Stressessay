using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
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

    private char _previousInput = ' ';

    [Header("Scrolling")]
    private int _totalLines = 0;
    [SerializeField]
    private int _linesBeforeScrolling = 1;
    private int _lineCount = 0;
    private int _lineCharacterCount = 0;
    private int _currentIndex = 0;



    [Header("Randomness")]
    [Range(0f, 100f)]
    [SerializeField] private float _ignoreInputOdds = 0;

    [Header("Penalty")]
    [SerializeField]
    private float _typoStressPenalty;

    private void Start()
    {
        if (_textAsset != null)
        {
            _textMeshPro.text = _textAsset.text;
        }

        _textChars = _textMeshPro.text.ToCharArray();
        _vanillaText = _textMeshPro.text;
        UpdateTextAppearance();
    }

    private void Update()
    {

        CheckForKeyboardInput();

        //Debug.Log(_currentIndex);
    }

    private void CheckForKeyboardInput()
    {
        string _inputString = Input.inputString;
        if (_inputString.Length > 0)
        {
            char _inputChar = _inputString.ToCharArray()[0]; //prevents failure when pressing 2 keys in the same frame
            if (_inputChar == _previousInput)
            {
                return;
            }

            _previousInput = _inputChar;
            Debug.Log(_inputChar);

            float _ignore = UnityEngine.Random.Range(0, 100);
            if (_ignore < _ignoreInputOdds)
            {
                //play ignored typing sound effect
                return;
            }

            if (_inputChar == _textChars[_currentIndex])
            {
                _currentIndex++;
                _lineCharacterCount++;
                UpdateTextAppearance();
            }

            else
            {
                Debug.Log(_inputChar + " " + _textChars[_currentIndex]);
                StressManager.AddStress(_typoStressPenalty);
                return;
            }
        }
    }

    private void UpdateTextAppearance()
    {
        string _highlightedText = _vanillaText;

        string _markdownHighlight = "<color=\"red\">";
        _highlightedText = _highlightedText.Insert(_currentIndex, _markdownHighlight);
        _highlightedText = _highlightedText.Insert(_currentIndex + 1 + _markdownHighlight.Length, "</color>");

        if (_currentIndex > 0)
        {
            string _finishedLetterHighilight = "<color=\"black\">";
            _highlightedText = _highlightedText.Insert(0, _finishedLetterHighilight);
            _highlightedText = _highlightedText.Insert(_currentIndex + _finishedLetterHighilight.Length, "</color>");
        }

        Debug.Log(_highlightedText);

        int _totalLineCharacterCount = _textMeshPro.textInfo.lineInfo[_lineCount].characterCount;
        Debug.Log("Total Line Character Count " + _totalLineCharacterCount);
        if (_lineCharacterCount >= _totalLineCharacterCount)
        {
            Debug.Log("finished line");
            _lineCharacterCount = 0;
            _lineCount++;
            _totalLines++;
            if (_lineCount >= _linesBeforeScrolling)
            {
                float _lineSize = 0;
                for (var i = _totalLines; i < _totalLines + _linesBeforeScrolling; i++)
                {
                    _lineSize += _textMeshPro.textInfo.lineInfo[i].lineHeight; 
                }
                _textMeshPro.rectTransform.position = new Vector2(_textMeshPro.rectTransform.position.x, _textMeshPro.rectTransform.position.y + (_lineSize * 1.3f));
                _lineCount = 0;
            }
        }

        _textMeshPro.text = _highlightedText;
    }




}
