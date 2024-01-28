using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicrophoneSlider : MonoBehaviour
{
    private Slider slider;
    [SerializeField]
    private Image _fill; 

    private float _limit = 50f;
    private float _currentFramesOverLimit = 0;
    private float _maxFramesOverLimit = 30;
    private bool _isLimited;

    private bool _shaking;

    public IEnumerator Shake()
    {
        Vector3 _startPosition = transform.position;
        float _elapsedTime = 0f;

        while (_shaking)
        {
            _elapsedTime += Time.deltaTime;
            float _currentIntensity = 0.5f;
            transform.position = _startPosition + (Random.insideUnitSphere * _currentIntensity);
            yield return null;
        }

        transform.position = _startPosition;
    }


    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        float _loudness = MicrophoneManager.Loudness;

        if (slider != null)
        {
            slider.value = _loudness;
            if (_loudness < _limit)
            {
                if (_shaking)
                {
                    _shaking = false;
                }

                _fill.color = Color.Lerp(Color.green, Color.magenta, _loudness / _limit);
            }

            else
            {
                if (!_shaking)
                {
                    _shaking = true;
                    StartCoroutine(Shake());
                }

                _fill.color = Color.red;
            }
        }
    }
}
