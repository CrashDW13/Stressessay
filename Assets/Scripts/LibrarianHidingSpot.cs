using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LibrarianHidingSpot : MonoBehaviour
{
    private float _direction;
    private bool _shaking;
    private Image _librarian;

    private float _librarianAppearanceX = 630;
    private float _librarianDisappearanceX = 1350;
    private float _librarianDuration = 3000f;

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
}
