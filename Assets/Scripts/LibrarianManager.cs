using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibrarianManager : MonoBehaviour
{
    private int _currentWaitFrames = 0;
    private int _totalWaitFrames = 1000;

    private bool _rolledRandomChance = false;
    private float _direction; 

    [SerializeField]
    private AudioClip[] _footsteps;
    [SerializeField]
    private AudioClip _appearance; 
    private AudioSource _audioSource;

    private LibrarianHidingSpot[] _librarianHidingSpots;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _direction = Mathf.Sign(transform.position.x);
        _librarianHidingSpots = FindObjectsOfType<LibrarianHidingSpot>();
    }

    // Update is called once per frame
    void Update()
    {
        int _minutes = (Mathf.FloorToInt(TimeManager.GetMinutes()));

        if (_currentWaitFrames < _totalWaitFrames)
        {
            _currentWaitFrames++;
            return; 
        }

        if (_minutes % 5 == 0)
        {
            if (!_rolledRandomChance)
            {
                LibrarianHidingSpot _spot = _librarianHidingSpots[Random.Range(0, _librarianHidingSpots.Length - 1)];
                int _chance = Random.Range(0, 5);
                if (_chance < 4)
                {
                    _audioSource.panStereo = _direction;
                    _audioSource.PlayOneShot(_footsteps[_chance]);
                    StartCoroutine(_spot.Shake(1f, false));
                    Debug.Log(_chance);
                }

                else
                {
                    Debug.Log("JUMPSCARE");
                    StartCoroutine(_spot.Shake(1f, true));

                }
                _rolledRandomChance = true;
            }
        }

        else
        {
            _rolledRandomChance = false;
        }
    }
}
