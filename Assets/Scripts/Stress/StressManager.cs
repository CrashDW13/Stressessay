using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressManager : MonoBehaviour
{
    private static float _stress;
    [SerializeField]
    private float _passiveIncrease;


    private void Update()
    {
        if (MicrophoneManager.Loudness < 40)
        {
            _stress += _passiveIncrease;
        }

        else
        {
            _stress -= MicrophoneManager.Loudness * 0.005f;
        }

        _stress = Mathf.Clamp(_stress, 0, 100);

        if (_stress > 100)
        {
            //Game Over
        }
    }

    public static float GetStress()
    {
        return _stress;
    }

    public static void AddStress(float _addedStress)
    {
        _stress += _addedStress;
    }


}
