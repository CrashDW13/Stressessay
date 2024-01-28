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
    private int _framesSinceLastInput = 3;

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
    public AnimationCurve _typoShakeAnimationCurve;

    [Header("SFX")]
    [SerializeField]
    private AudioClip[] _typingSounds;
    [SerializeField]
    private AudioClip _spaceSound;
    [SerializeField]
    private AudioClip[] _typoSounds;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
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
                if (_inputChar == ' ')
                {
                    _audioSource.PlayOneShot(_spaceSound);
                }
                else
                {
                    int _randomIndex = UnityEngine.Random.Range(0, _typingSounds.Length - 1);
                    _audioSource.PlayOneShot(_typingSounds[_randomIndex]);
                }
                UpdateTextAppearance();
            }

            else
            {
                Debug.Log(_inputChar + " " + _textChars[_currentIndex]);
                StressManager.AddStress(_typoStressPenalty);
                int _randomIndex = UnityEngine.Random.Range(0, _typoSounds.Length - 1);
                _audioSource.PlayOneShot(_typoSounds[_randomIndex]);
                CameraShake.Shake(_typoShakeAnimationCurve, 0.5f);
                return;
            }
        }
    }

    private void UpdateTextAppearance()
    {
        string _highlightedText = _vanillaText;

        string _markdownHighlight = "<color=\"orange\">";
        _highlightedText = _highlightedText.Insert(_currentIndex, _markdownHighlight);
        _highlightedText = _highlightedText.Insert(_currentIndex + 1 + _markdownHighlight.Length, "</color>");

        if (_currentIndex > 0)
        {
            string _finishedLetterHighilight = "<color=\"black\">";
            _highlightedText = _highlightedText.Insert(0, _finishedLetterHighilight);
            _highlightedText = _highlightedText.Insert(_currentIndex + _finishedLetterHighilight.Length, "</color>");
        }

        Debug.Log(_highlightedText);
        _textMeshPro.text = _highlightedText;

        int _totalLineCharacterCount = _textMeshPro.textInfo.lineInfo[_totalLines].characterCount;
        Debug.Log(_lineCharacterCount);
        Debug.Log("Total Line Character Count " + _totalLineCharacterCount);
        if (_lineCharacterCount == _totalLineCharacterCount)
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
                _textMeshPro.rectTransform.anchoredPosition3D = new Vector3(_textMeshPro.rectTransform.anchoredPosition3D.x, _textMeshPro.rectTransform.anchoredPosition3D.y + (_lineSize * 0.5f), 0f);
                _lineCount = 0;
            }
        }

    }




}
