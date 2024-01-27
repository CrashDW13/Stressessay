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
        _stress += _passiveIncrease;

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
