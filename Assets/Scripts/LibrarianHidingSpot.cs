using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class LibrarianHidingSpot : MonoBehaviour
{
    private float _direction;
    private bool _shaking;
    private Image _librarian;
    private bool _scaring = false;

    private float _movementFrame = 0f;
    private float _movementDuration = 10f;
    private float _libraryFrame = 0f;
    private float _librarianAppearanceX = 630;
    private float _librarianDisappearanceX = 1350;
    private float _librarianDuration = 4f;

    public IEnumerator Shake(float _duration, bool _jumpscare)
    {
        Vector3 _startRotation = transform.eulerAngles;
        float _currentTime = 0;
        while (_currentTime < _duration)
        {
            _currentTime += Time.deltaTime;
            float _currentIntensity = 1f;
            transform.eulerAngles = new Vector3(_startRotation.x, _startRotation.y, _startRotation.z + Random.Range(-_currentIntensity, _currentIntensity)); 
            yield return null;
        }

        transform.eulerAngles = _startRotation;
        if (_jumpscare)
        {

        }
    }

    private void Update()
    {
        if (_scaring)
        {
            _libraryFrame += Time.deltaTime;
            if (_movementFrame < _movementDuration)
            {
                _movementFrame += Time.deltaTime;
            }

            _librarian.rectTransform.position = new Vector3(Mathf.Lerp(_librarianDisappearanceX, _librarianAppearanceX, _movementFrame / _movementDuration), _librarian.rectTransform.position.y, _librarian.rectTransform.position.z);

        }
    }
}
