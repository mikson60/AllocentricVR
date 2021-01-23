using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Stopwatch : MonoBehaviour
{
    [SerializeField] private Text _text;

    // Runtime
    private List<Action> _stopWatchActions = new List<Action>();
    private int _currentStateIndex = 0;


    private bool _isRunning = false;
    private float _currentTime = 0f;

    private void Awake()
    {
        _stopWatchActions.Add(() =>
        {
            StartTime();
        });
        _stopWatchActions.Add(() =>
        {
            PauseTime();
        });
        _stopWatchActions.Add(() =>
        {
            ResetTime();
        });
    }

    void Start()
    {
        Debug.Log(_stopWatchActions.Count);

        _currentStateIndex = 2;

        _stopWatchActions[_currentStateIndex].Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isRunning)
        {
            _currentTime += Time.deltaTime;
        }

        _text.text = _currentTime.ToString("F2") + " seconds";

        if (Keyboard.current[Key.T].wasPressedThisFrame)
        {
            NextState();
        }
    }

    private void NextState()
    {
        _currentStateIndex = _currentStateIndex == _stopWatchActions.Count - 1 ? 0 : _currentStateIndex + 1;

        _stopWatchActions[_currentStateIndex].Invoke();
    }

    private void StartTime()
    {
        _isRunning = true;
    }
    private void PauseTime()
    {
        _isRunning = false;
    }

    private void ResetTime()
    {
        _currentTime = 0f;
    }
}
