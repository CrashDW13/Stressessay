using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
public class StressSlider : MonoBehaviour
{
    Slider _slider;
    [SerializeField]
    Image _fill;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        StartCoroutine(Shake());
    }

    private void Update()
    {
        _slider.value = StressManager.GetStress();
        _fill.color = Color.Lerp(Color.yellow, Color.red, _slider.value / _slider.maxValue);
    }

    public IEnumerator Shake()
    {
        Vector3 _startPosition = transform.position;

        while (true)
        {
            float _currentIntensity = (_slider.value / _slider.maxValue) / 2;
            transform.position = _startPosition + (Random.insideUnitSphere * _currentIntensity);
            yield return null;
        }
    }
}
