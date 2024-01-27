using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrophoneManager : MonoBehaviour
{
    public static float Loudness = 0;

    private AudioClip _clipRecord;
    private string _device;
    private int _sampleWindow = 128;
    private bool _isRecording;
    
    private void StartMicrophone()
    {
        if (_device is null)
        {
            _device = Microphone.devices[0];
            _clipRecord = Microphone.Start(_device, true, 999, 44100);
        }
    }

    private void StopMicrophone()
    {
        Microphone.End(_device);
    }

    private float LevelMax()
    {
        float levelMax = 0;
        float[] waveData = new float[_sampleWindow];
        int micPosition = Microphone.GetPosition(null) - (_sampleWindow + 1);

        if (micPosition < 0)
        {
            return 0;
        }

        _clipRecord.GetData(waveData, micPosition);
        for (int i = 0; i < _sampleWindow; i++)
        {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak)
            {
                levelMax = wavePeak;
            }
        }

        return Mathf.Round(levelMax * 100000);
    }

    private void Start()
    {
        StartMicrophone();
    }

    private void Update()
    {
        Loudness = LevelMax();
    }


    private void OnEnable()
    {
        _isRecording = true;
    }

    private void OnDisable()
    {
        StopMicrophone();
    }

    private void OnDestroy()
    {
        StopMicrophone();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            if (!_isRecording)
            {
                StartMicrophone();
                _isRecording = true;
            }
        }

        else
        {
            StopMicrophone();
            _isRecording = false;
        }
    }
}
