using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeClock : MonoBehaviour
{
    TextMeshProUGUI _textMeshPro;

    private void Start()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _textMeshPro.text = TimeManager.GetClockString();
    }
}
