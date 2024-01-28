using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private static float _duration = 0.5f;
    private static CameraShake instance;
    private static AnimationCurve _curve;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        //Debug.Log(transform.position);
    }

    public static void Shake(AnimationCurve curve, float duration = 0.5f)
    {
        _duration = duration;
        _curve = curve;

        instance.StartCoroutine(instance.Shaking());
    }

    public IEnumerator Shaking()
    {
        Vector3 _startPosition = transform.position;
        float _elapsedTime = 0f; 

        while(_elapsedTime < _duration)
        {
            _elapsedTime += Time.deltaTime;
            float _currentIntensity = _curve.Evaluate(_elapsedTime/_duration);
            //float _stressIntensity = _currentIntensity * (StressManager.GetStress());
            Debug.Log(_currentIntensity);
            transform.position = _startPosition + (Random.insideUnitSphere * _currentIntensity);
            yield return null; 
        }

        transform.position = _startPosition;  
    }
}
